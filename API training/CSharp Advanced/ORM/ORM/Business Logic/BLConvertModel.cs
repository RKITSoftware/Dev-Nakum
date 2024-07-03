using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace ORM.Business_Logic
{
    /// <summary>
    /// Convert the dto model to poco model
    /// </summary>
    public class BLConvertModel
    {
        /// <summary>
        ///  to check is json property is valid or not
        /// </summary>
        /// <param name="dtoPropertyName">property name of dto</param>
        /// <param name="dtoJsonPropertyName">property name of poco</param>
        /// <returns>true if valid or else false</returns>
        public static bool IsValidJsonProperty(string dtoPropertyName, string dtoJsonPropertyName)
        {
            for (int i = 0; i < dtoPropertyName.Length; i++)
            {
                if (dtoPropertyName[i] == 'F')
                {
                    return dtoJsonPropertyName[i] == '1';
                }
            }
            return true;
        }

        /// <summary>
        /// map dto model to poco model
        /// </summary>
        /// <typeparam name="TDto">Dto model</typeparam>
        /// <typeparam name="TPoco">poco model</typeparam>
        /// <param name="dto">object of dto model</param>
        /// <returns>converted poco model</returns>
        public static TPoco MapDtoToPoco<TDto, TPoco>(TDto dto)
        {
            var poco = Activator.CreateInstance<TPoco>();
            var dtoProperties = typeof(TDto).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                //var dtoProperty = typeof(DtoUse01).GetProperty("E01F02");
                var matchingPocoProperty = typeof(TPoco).GetProperty(dtoProperty.Name);
                if (matchingPocoProperty != null)
                {
                    // Verify JSON property of DTO's E01F02
                    var dtoJsonProperty = dtoProperty.GetCustomAttributes<JsonPropertyAttribute>(false).FirstOrDefault();
                    if ((dtoJsonProperty != null && IsValidJsonProperty(dtoProperty.Name, dtoJsonProperty.PropertyName)))
                    {
                        // Matching property and JSON property, copy value
                        matchingPocoProperty.SetValue(poco, dtoProperty.GetValue(dto));
                    }
                }
            }
            return poco;
        }
    }
}