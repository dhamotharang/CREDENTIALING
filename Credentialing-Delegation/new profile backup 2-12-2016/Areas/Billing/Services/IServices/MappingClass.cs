using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public static class MappingClass
    {
        public static IList<T> ToList<T>(dynamic OutPut) where T : new()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();
            foreach (var row in OutPut)
            {
                var item = CreateItemFromRow<T>((dynamic)row, properties);
                result.Add(item);
            }
            return result;
        }
        private static T CreateItemFromRow<T>(dynamic row, IList<PropertyInfo> properties) where T : new()
        {
            T Property = new T();
            foreach (var property in properties)
            {
                var a = row.GetType().GetProperty(property.Name).GetValue(row, null);
                property.SetValue(Property, row.GetType().GetProperty(property.Name).GetValue(row, null), null);
            }
            return Property;
        }
    }
}