using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Packages.Exceptions.Converters
{
    public class SpecialCharacterRemovingConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() ?? string.Empty;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            // Özel karakterleri boşluk veya istediğiniz başka bir karakterle değiştiriyoruz
            var sanitizedValue = value.Replace("\r", " ").Replace("\n", " ");
            writer.WriteStringValue(sanitizedValue);
        }
    }
}
