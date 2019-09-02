using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.CustomTagHelpers
{
    [HtmlTargetElement("baserecord", Attributes = ForAttributeName + "," + ItemsAttributeName )]
    public class BaseRecord : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string ItemsAttributeName = "asp-items";
        private const string BaseRecordKeyAttributeName = "asp-key";
        private const string cacheKey = "baserecord_key";
        private readonly IMemoryCache _cache;
        private readonly IBaseRecordService _baseRecordService;
        public BaseRecord(IMemoryCache cache, IBaseRecordService baseRecordService)
        {
            _cache = cache;
            _baseRecordService = baseRecordService;
        }

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<SelectListItem> Items { get; set; }

        [HtmlAttributeName(BaseRecordKeyAttributeName)]
        public string Key { get; set; }
      // public string Key2 { get; set; }
        // public string @class { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.ViewContext == null || this.For == null || (this.Items == null&&string.IsNullOrEmpty(Key)))
                return;

            output.SuppressOutput();
            output.Content.Clear();

            output.Content.AppendHtml(this.GenerateDropdownList());

        }

        private IHtmlContent GenerateDropdownList()
        {
            TagBuilder tb = new TagBuilder("select");
            tb.MergeAttribute("id", this.GetIdentity());
            tb.MergeAttribute("name", this.GetIdentity());
            tb.MergeAttribute("data-value", this.GetValue());
            tb.AddCssClass("custom-select");
            if (Items == null)
            {
                Items =  GetBaseRecordItems(Key);
            }
            if (!Items.Any(i => i.Selected))
            {
                tb.InnerHtml.AppendHtml(this.GenerateDropDownListItem(new SelectListItem("--Select--","",true)));
            }
                foreach (var item in Items)
                    tb.InnerHtml.AppendHtml(this.GenerateDropDownListItem(item));
           



            return tb;
        }

        private string GetIdentity()
        {
            return this.For.Name;
        }

        private string GetValue()
        {
            ModelStateEntry modelState;

            if (ViewContext.ModelState.TryGetValue(this.GetIdentity(), out modelState) && !string.IsNullOrEmpty(modelState.AttemptedValue))
                return modelState.AttemptedValue;

            return this.For.Model == null ? null : this.For.Model.ToString();


        }
        private IHtmlContent GenerateDropDownListItem(SelectListItem item)
        {
            TagBuilder tb = new TagBuilder("option");
            tb.MergeAttribute("value", item.Value);
            tb.InnerHtml.AppendHtml(item.Text);

            if (item.Selected)
                tb.MergeAttribute("selected", "selected");

            //string currentValue = this.GetValue();
            //if (item.Value == currentValue)
            //    tb.MergeAttribute("selected", "selected");

            return tb;
        }

        private IEnumerable<SelectListItem> GetBaseRecordItems(string key)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if (_cache.TryGetValue(cacheKey, out List<GetBaseRecordWithItems> baseRecords))
            {
                string currentValue = this.GetValue();
                selectListItems = (from rec in baseRecords
                                   where rec.KeyName == key
                                   from recItem in rec.BaseRecordItems
                                   select new SelectListItem
                                   {
                                       Selected= recItem.BaseRecordItemId.ToString() == currentValue,
                                       Text = recItem.ValueName,
                                       Value = recItem.BaseRecordItemId.ToString()
                                   }).ToList();
            }
           
            return selectListItems;
        }

    }
}
