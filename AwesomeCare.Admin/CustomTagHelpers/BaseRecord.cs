using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.CustomTagHelpers
{
    [HtmlTargetElement("baserecord", Attributes = ForAttributeName + "," + ItemsAttributeName)]
    public class BaseRecord : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string ItemsAttributeName = "asp-items";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<SelectListItem> Items { get; set; }

        public string Key { get; set; }
        // public string @class { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.ViewContext == null || this.For == null || this.Items == null)
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

            string currentValue = this.GetValue();
            if(item.Value == currentValue)
                tb.MergeAttribute("selected", "selected");

            return tb;
        }

       

    }
}
