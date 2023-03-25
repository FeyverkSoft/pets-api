namespace Pets.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class JsonHelper
    {
        public static String ToJson(this Object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
            });
        }

        /// <summary>
        ///     Преобразовать строку сожержащую JSON в объект
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Object? ParseJson(this String json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
            });
        }

        /// <summary>
        ///     Преобразовать строку сожержащую JSON в объект
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T? ParseJson<T>(this String json)
        {
            if (String.IsNullOrEmpty(json))
                return default;
            return JsonSerializer.Deserialize<T?>(json, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
            });
        }

        public static T? TryParseJson<T>(this String json)
        {
            try
            {
                return json.ParseJson<T>();
            }
            catch
            {
                return default;
            }
        }

        public static Object? TryParseJson(this String json, Type type)
        {
            try
            {
                return json.ParseJson(type);
            }
            catch
            {
                return null;
            }
        }
    }
}