using System;
using System.Linq;
using System.Reflection;

namespace Bank_Management_System.Extensions
{
    /// <summary>
    /// Convert the dto model to poco model
    /// </summary>
    public static class ConvertExtension
    {
        #region Public Method
        /// <summary>
        /// convert DTo to POCO
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static TDestination Convert<TSource, TDestination>(this TSource source, TDestination destination)
        {
            if (destination == null)
            {
                ///create the run time instance
                destination = Activator.CreateInstance<TDestination>();
            }

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            if (sourceProperties != null && destinationProperties != null)
            {
                foreach (var sourceProp in sourceProperties)
                {
                    var destProp = destinationProperties.FirstOrDefault(x => x.Name == sourceProp.Name);

                    if (destProp != null && destProp.PropertyType == sourceProp.PropertyType)
                    {
                        var value = sourceProp.GetValue(source, null);
                        destProp.SetValue(destination, value);
                    }
                }
            }
            else
            {
                // Handle the case where sourceProperties or destinationProperties is null
                throw new InvalidOperationException("Source or destination properties are null.");
            }

            return destination;
        } 
        #endregion
    }
}