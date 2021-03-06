﻿namespace ImdbLite.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlExtentions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, object htmlAttributes = null)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");

            return MvcHtmlString.Create(input.ToString());
        }

        public static MvcHtmlString Submit(this HtmlHelper helper, string name, object htmlAttributes = null)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");
            input.Attributes.Add("value", name);

            return MvcHtmlString.Create(input.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string source, object htmlAttributes)
        {

            var builder = new TagBuilder("image");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;

            builder.MergeAttribute("src", source);
            builder.MergeAttributes(attributes);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, byte[] source, string type, string alt = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("image");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            var src = string.Format("data:{0};base64,{1}", type, Convert.ToBase64String(source, Base64FormattingOptions.None));

            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttributes(attributes);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string ImageHref(this HtmlHelper htmlHelper, byte[] source, string type)
        {
            var src = string.Format("data:{0};base64,{1}", type, Convert.ToBase64String(source, Base64FormattingOptions.None));

            return src;
        }

        public static MvcHtmlString UploadFile<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            attributes.Add("type", "file");

            return InputExtensions.TextBoxFor<TModel, TProperty>(htmlHelper, expression, attributes);
        }
    }
}
