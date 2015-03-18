namespace ImdbLite.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static IEnumerable<SelectListItem> ToSelectList<T, TTextProperty, TValueProperty>(this IEnumerable<T> instance, Func<T, TTextProperty> text, Func<T, TValueProperty> value, Func<T, bool> selectedItem = null)
        {
            return instance.Select(t => new SelectListItem
            {
                Text = Convert.ToString(text(t)),
                Value = Convert.ToString(value(t)),
                Selected = selectedItem != null ? selectedItem(t) : false
            });
        }
    }
}
