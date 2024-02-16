namespace LostAnimals.Common.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

public static class ObjectJsonExtension
{
    public static JsonSerializerSettings SetDefaultSettings(this JsonSerializerSettings settings)
    {
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        return settings;
    }

    public static JsonSerializerSettings DefaultJsonSerializerSettings()
    {
        return new JsonSerializerSettings().SetDefaultSettings();
    }

    public static string ToJsonString(this object obj, JsonSerializerSettings settings = null)
    {
        try
        {
            return JsonConvert.SerializeObject(obj, settings ?? DefaultJsonSerializerSettings());
        }
        catch (Exception ex)
        {
            throw new JsonException("Failed to convert to json string", ex);
        }
    }

    public static T FromJsonString<T>(this string obj, JsonSerializerSettings settings = null)
    {
        return JsonConvert.DeserializeObject<T>(obj, settings ?? DefaultJsonSerializerSettings());
    }

    public static object FromJsonString(this string obj, JsonSerializerSettings settings = null)
    {
        return JsonConvert.DeserializeObject(obj, typeof(object), settings ?? DefaultJsonSerializerSettings());
    }

    public static bool TryFromJsonString<T>(this string obj, out T? result)
    {
        try
        {
            result = JsonConvert.DeserializeObject<T>(obj);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}