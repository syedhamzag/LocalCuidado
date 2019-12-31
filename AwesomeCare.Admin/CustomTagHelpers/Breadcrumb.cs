using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.CustomTagHelpers
{
    [HtmlTargetElement("breadcrumb")]
    public class Breadcrumb : TagHelper
    {
        protected HttpRequest Request => ViewContext.HttpContext.Request;
        protected HttpResponse Response => ViewContext.HttpContext.Response;


        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string li;
            string path = Request.Path.Value;
            var pages = path.Split('/');
            RouteData routeData = RoutingHttpContextExtensions.GetRouteData(ViewContext.HttpContext);
            string currentAction = routeData.Values["action"]?.ToString();
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "breadcrumb");
            li = @"<li class=""breadcrumb-item""><a href=""/""><i class=""icon-home""></i></a></li>";
            output.Content.AppendHtml(li);
            for (int i = 0; i < pages.Length; i++)
            {
                var page = pages[i];
                if (!string.IsNullOrWhiteSpace(page))
                {
                    if (string.Equals(page, currentAction, StringComparison.InvariantCultureIgnoreCase))
                    {
                        li = $@"<li class=""breadcrumb-item active"">{page}</li>";
                    }
                    else
                    {
                        li = $@"<li class=""breadcrumb-item"">{page}</li>";
                    }
                    
                    output.Content.AppendHtml(li);
                }
            }
            output.PostContent.SetHtmlContent("</ul>");

        }
    }
}
