using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.TagHelpers
{
    [HtmlTargetElement("AwesomeCareLabel")]
   public class AwesomeCareLabelTagHelper:TagHelper
    {
        public AwesomeCareLabelTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }
        protected IHtmlGenerator Generator { get; }

        public ModelExpression AspFor { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
           
            output.TagName = "label";
          
            
            foreach (var attribute in context.AllAttributes)
            {
                if (attribute.Name.Equals("asp-for", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                output.Attributes.Add(attribute.Name, attribute.Value);
            }

            string displayName = GetLabelName();
            string labelName = string.IsNullOrEmpty(displayName) ? AspFor.Name : displayName;

            output.Content.SetHtmlContent($@"{labelName}");

            var childContent = await output.GetChildContentAsync();

            if (!childContent.IsEmptyOrWhiteSpace)
            {
                var content = childContent.GetContent();
                output.Content.AppendHtml($" {content}");
            }

            if (IsRequired())
            {
                output.Content.AppendHtml($@" <span class=""text-danger"">*</span>");
            }
           
           

            output.TagMode = TagMode.StartTagAndEndTag;
        }

        string GetLabelName()
        {
            var modelObjectType = AspFor.ModelExplorer.Metadata.ContainerType;
            var modelObjectProperties = modelObjectType.GetProperties();
            var property = modelObjectProperties.FirstOrDefault(c => c.Name == AspFor.Name);
            var displayAttribute = property.GetCustomAttributes().FirstOrDefault(c => c.GetType() == typeof(DisplayAttribute)) as DisplayAttribute;

            return displayAttribute?.Name;
        }

        bool IsRequired()
        {
            var modelObjectType = AspFor.ModelExplorer.Metadata.ContainerType;
            var modelObjectProperties = modelObjectType.GetProperties();
            var property = modelObjectProperties.FirstOrDefault(c => c.Name == AspFor.Name);
            bool isRequired = property.GetCustomAttributes().Any(c => c.GetType() == typeof(RequiredAttribute));

            return isRequired;
        }
    }
}
