﻿namespace Pets.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    public static class JsonHelper
    {
        public static String ToJson(this Object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>()
            });
        }

        public static String ToJson(this Object obj, NullValueHandling nullValueHandling)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>(),
                NullValueHandling = nullValueHandling
            });
        }

        /// <summary>
        ///     Преобразовать строку сожержащую JSON в объект
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Object ParseJson(this String json, Type type)
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return serializer.Deserialize(new StringReader(json), type);
        }

        /// <summary>
        ///     Преобразовать строку сожержащую JSON в объект
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJson<T>(this String json)
        {
            if (String.IsNullOrEmpty(json))
                return default;
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return serializer.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        }

        public static T TryParseJson<T>(this String json)
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

        public static Object TryParseJson(this String json, Type type)
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