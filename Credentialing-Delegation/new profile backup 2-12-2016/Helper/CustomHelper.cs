using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PortalTemplate.Helper
{
    static public class CustomHelper
    {
        #region ExportOptions
        public static MvcHtmlString ExportOptions(this HtmlHelper htmlHelper, string par1 = "false", string par2 = "false", string par3 = "false", string par4 = "false", string par5 = "false")
        {
            var path = "/Helper/Resources/Images/";
            TagBuilder aTag;
            TagBuilder imgTag;
            var element = "";
            if (par1 != "false" || par2 != "false" || par3 != "false" || par4 != "false" || par5 != "false")
            {
                if (par1 != "false")
                {
                    element += buildExportTag(out aTag, out imgTag, par1.ToLower(), path + par1.ToLower() + ".png").ToString();
                }
                if (par2 != "false")
                {
                    element += buildExportTag(out aTag, out imgTag, par2.ToLower(), path + par2.ToLower() + ".png").ToString();
                }
                if (par3 != "false")
                {
                    element += buildExportTag(out aTag, out imgTag, par3.ToLower(), path + par3.ToLower() + ".png").ToString();
                }
                if (par4 != "false")
                {
                    element += buildExportTag(out aTag, out imgTag, par4.ToLower(), path + par4.ToLower() + ".png").ToString();
                }
                if (par5 != "false")
                {
                    element += buildExportTag(out aTag, out imgTag, par5.ToLower(), path + par5.ToLower() + ".png").ToString();
                }
            }
            else
            {
                element = buildExportTag(out aTag, out imgTag, "Print", path + "print.png").ToString() +
                   buildExportTag(out aTag, out imgTag, "Email", path + "email.png").ToString() +
                    buildExportTag(out aTag, out imgTag, "Save as PDF", path + "pdf.png").ToString() +
                    buildExportTag(out aTag, out imgTag, "Save as Excel", path + "excel.png").ToString();
            }
            return MvcHtmlString.Create(element);
        }

        public static TagBuilder buildExportTag(out TagBuilder aTag, out TagBuilder imgTag, string title, string imgPath)
        {
            switch (title)
            {
                case "pdf": title = "Save as PDF"; break;
                case "excel": title = "Save as Excel"; break;
                default: break;
            }
            title = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);//this will capitalize the first letter
            aTag = new TagBuilder("a");
            imgTag = new TagBuilder("img");
            aTag.Attributes["href"] = "javascript:void(0)";
            aTag.Attributes["class"] = "export-button pull-right";
            aTag.Attributes["data-toggle"] = "tooltip";
            aTag.Attributes[" data-placement"] = "bottom";
            aTag.Attributes["title"] = title;
            imgTag.Attributes["src"] = imgPath;
            aTag.InnerHtml += imgTag;
            return aTag;
        }
        #endregion

        #region RadioButton
        public static MvcHtmlString PratianRadio(this HtmlHelper htmlHelper, string value = "2")
        {
            var divTag = new TagBuilder("div");
            var inputTag = new TagBuilder("input");
            var labelTag = new TagBuilder("label");
            var spanTag = new TagBuilder("span");
            inputTag.Attributes["type"] = "radio";
            inputTag.Attributes["value"] = value;
            labelTag.InnerHtml += spanTag;
            labelTag.InnerHtml += "Option 1";
            divTag.InnerHtml += inputTag;
            divTag.InnerHtml += labelTag;


            return MvcHtmlString.Create(divTag.ToString());

        }
        #endregion


        public static MvcHtmlString ShortNameFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var name = metadata.ShortDisplayName ?? metadata.DisplayName ?? metadata.PropertyName;

            return MvcHtmlString.Create(string.Format(@"<span>{0}</span>", name));
        }

        public static string RenderPartialToString(ControllerContext controllerContext, String viewName, Object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}