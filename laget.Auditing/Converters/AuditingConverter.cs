using System;
using System.Collections.Generic;
using System.Dynamic;
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
            return objectType.IsClass && objectType.GetProperties().Any(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(AuditableAttribute)));
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

            var properties = value
                .GetType()
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(AuditableAttribute)));

            var reflection = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in properties)
            {
                reflection.Add(property.Name, property.GetValue(value));
            }

            var token = JToken.FromObject(reflection);
            if (token.Type != JTokenType.Object)
            {
                // We should never reach this point because CanConvert() only allows objects with JsonPropertyDynamicNameAttribute to pass through.
                throw new Exception("Token to be serialized was unexpectedly not an object.");
            }

            token = (JObject)token;
            var convertible = value.GetType().GetProperties().Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(AuditableAttribute)));

            foreach (var property in convertible)
            {
                var dynamicAttributeData = property.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(AuditableAttribute));

                // Determine what we should rename the property from and to.
                var currentName = property.Name;
                var constructorArguments = dynamicAttributeData?.ConstructorArguments;

                string newName;
                if (constructorArguments.Count == 0)
                {
                    newName = resolver?.GetResolvedPropertyName(currentName);
                }
                else
                {
                    newName = resolver?.GetResolvedPropertyName((string)constructorArguments[0].Value) ?? currentName;
                }

                // Perform the renaming in the JSON object.
                var currentPropertyValue = token[currentName];
                var newProperty = new JProperty(newName, currentPropertyValue);

                currentPropertyValue?.Parent?.Replace(newProperty);
            }

            token.WriteTo(writer);
        }
    }
}
