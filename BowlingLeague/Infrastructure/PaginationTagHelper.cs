using System;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BowlingLeague.Infrastructure
{
    //can create your own attributes as well by giving "Attributes" a name. Creates attribute called paging info that we will use on html pages
    [HtmlTargetElement("div", Attributes = "page-info")]

    //class extends TagHelper Class
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        public PaginationTagHelper(IUrlHelperFactory urlHelper)
        {
            urlInfo = urlHelper;
        }

        public PageNumberingInfo PageInfo { get; set; }

        public bool SetUpCorrectly { get; set; }

        //these attributes make it so we can do css on the page nums
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //our own dictionary we are creating
        //group things based off a common prefix
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        //Overriding pre-written method
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.TotalPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;

                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);

                //sets the up the css for the paging buttons
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                //adds the newly created tags to the html
                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);

            }



            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
