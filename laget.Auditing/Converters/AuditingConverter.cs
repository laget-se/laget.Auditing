using System;
using System.Linq;
using laget.Auditing.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace laget.Auditing.Converters
{
    public class AuditingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Only use this converter for classes that contain properties with an JsonDynamicNameAttribute.
            return objectType.IsClass && objectType.GetProperties().Any(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(AuditingAttribute)));
        }

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // We do not support deserialization.
            throw new NotImplementedException();
        }

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resolver = serializer.ContractResolver as DefaultContractResolver;

            var token = JToken.FromObject(value);
            if (token.Type != JTokenType.Object)
            {
                // We should never reach this point because CanConvert() only allows objects with JsonPropertyDynamicNameAttribute to pass through.
                throw new Exception("Token to be serialized was unexpectedly not an object.");
            }

            token = (JObject)token;
            var convertible = value.GetType().GetProperties().Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(AuditingAttribute)));
            var nonconvertible = value.GetType().GetProperties().Where(prop => !prop.CustomAttributes.Any());

            foreach (var property in convertible)
            {
                var dynamicAttributeData = property.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(AuditingAttribute));

                // Determine what we should rename the property from and to.
                var currentName = property.Name;
                var newName = resolver?.GetResolvedPropertyName((string) dynamicAttributeData.ConstructorArguments[0].Value) ?? currentName;

                // Perform the renaming in the JSON object.
                var currentJsonPropertyValue = token[currentName];
                var newJsonProperty = new JProperty(newName, currentJsonPropertyValue);
                currentJsonPropertyValue.Parent.Replace(newJsonProperty);
            }

            foreach (var property in nonconvertible)
            {
                var currentName = property.Name;
                var newName = resolver?.GetResolvedPropertyName(currentName) ?? currentName;

                // Perform the renaming in the JSON object.
                var currentJsonPropertyValue = token[currentName];
                var newJsonProperty = new JProperty(newName, currentJsonPropertyValue);
                currentJsonPropertyValue.Parent.Replace(newJsonProperty);
            }

            token.WriteTo(writer);
        }
    }
}
