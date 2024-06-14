using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialMediaAPI.Other
{
    public class DataTableJsonConverter : JsonConverter<DataTable>
    {
        public override DataTable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization from JSON to DataTable is not supported.");
        }

        public override void Write(Utf8JsonWriter writer, DataTable dataTable, JsonSerializerOptions options)
        {
            // Start the JSON object
            writer.WriteStartObject();

            // Write table name and rows
            writer.WritePropertyName("TableName");
            writer.WriteStringValue(dataTable.TableName);

            writer.WritePropertyName("Rows");
            writer.WriteStartArray();

            // Serialize each DataRow
            foreach (DataRow row in dataTable.Rows)
            {
                writer.WriteStartObject();
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.WritePropertyName(column.ColumnName);
                    writer.WriteStringValue(row[column]?.ToString()); // Use WriteValue for various data types
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray(); // End the rows array
            writer.WriteEndObject(); // End the JSON object
        }
    }

}
