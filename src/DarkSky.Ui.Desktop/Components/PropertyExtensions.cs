using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DarkSky.Ui.Desktop.Components
{
    public static class PropertyExtensions
    {
        /// <summary>
        /// Gets property information for the specified <paramref name="property"/> expression.
        /// </summary>
        /// <typeparam name="TSource">
        /// Type of the parameter in the <paramref name="property"/> expression.
        /// </typeparam>
        /// <typeparam name="TValue">Type of the property's value.</typeparam>
        /// <param name="property">The expression from which to retrieve the property information.</param>
        /// <returns>Property information for the specified expression.</returns>
        /// <exception cref="ArgumentException">The expression is not understood.</exception>
        public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            if (property.Body is MemberExpression body && body.Member is PropertyInfo propertyInfo)
            {
                return propertyInfo;
            }

            throw new ArgumentException("Expression is not a property", "property");
        }
    }
}