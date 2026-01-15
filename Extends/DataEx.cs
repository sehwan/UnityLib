using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

public static class DataEx
{
    public static T DeepClone<T>(this T from)
    {
        var temp = JsonConvert.SerializeObject(from);
        return JsonConvert.DeserializeObject<T>(temp);
    }
    public static bool IsSame<T>(this T from, T to)
    {
        return JsonConvert.SerializeObject(from) == JsonConvert.SerializeObject(to);
    }


    // This is not working for something
    public static T DeepCloneBinary<T>(this T from)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, from);
            ms.Position = 0;
            return (T)formatter.Deserialize(ms);
        }
    }

    // A Type -> B Type
    public static V Cast<T, V>(this T obj)
    {
        var tempJson = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        return JsonConvert.DeserializeObject<V>(tempJson);
    }
    // Object -> String
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        });
    }

    public static string ToJsoIgnoreNull(this object obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new IgnoreEmptyObjectsResolver()
        });
    }

    class IgnoreEmptyObjectsResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            // 참조 타입(클래스)만 체크
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                property.ShouldSerialize = instance =>
                {
                    var value = property.ValueProvider.GetValue(instance);
                    return value != null;
                };
            }
            return property;
        }
    }


    // String -> Object
    public static V ToObject<V>(this string str)
    {
        return JsonConvert.DeserializeObject<V>(str, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        });
    }

    public static JObject ToJObject(this string json)
    {
        return JObject.Parse(json);
    }
    public static JArray ToJArray(this string json)
    {
        return JArray.Parse(json);
    }


    public static int ToInt(this bool me)
    {
        return me == true ? 1 : 0;
    }
    public static bool ToBool(this int me)
    {
        return me != 0;
    }

    // Enum
    public static int GetEnumCount<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Length;
    }
    public static int GetLastEnumValue<T>(this T enumValue) where T : Enum
    {
        Type enumType = typeof(T);
        Array enumValues = Enum.GetValues(enumType);

        int lastNumber = (int)enumValues.GetValue(enumValues.Length - 1);
        return lastNumber;
    }


    public static List<MemberInfo> GetFieldsAndProperties<T>(BindingFlags bindingAttr)
    {
        return GetFieldsAndProperties(typeof(T), bindingAttr);
    }

    public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
    {
        List<MemberInfo> targetMembers = new();

        targetMembers.AddRange(type.GetFields(bindingAttr));
        targetMembers.AddRange(type.GetProperties(bindingAttr));

        return targetMembers;
    }

    // Reflection
    public static T GetField<T>(this Object me, string fieldName)
    {
        return (T)me.GetType().GetField(fieldName).GetValue(me);
    }
    public static void SetField<T>(this Object me, string fieldName, T newVal)
    {
        me.GetType().GetField(fieldName).SetValue(me, newVal);
    }
    public static void IncrementField(this Object me, string fieldName, int add)
    {
        var field = me.GetType().GetField(fieldName);
        var now = (int)field.GetValue(me);
        field.SetValue(me, now + add);
    }

    public static T DeserializeUnescape<T>(this string me)
    {
        var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(me);
        foreach (var e in dic)
        {
            dic[e.Key] = Regex.Unescape(dic[e.Key]);
        }
        var json = JsonConvert.SerializeObject(dic);
        return JsonConvert.DeserializeObject<T>(json);
    }

    // public static Dictionary MergeDictionaries<Dictionary>(this Dictionary me, Dictionary other)
    // {
    //     Dictionary<string, string>[] dicts = new Dictionary[] { me, other };
    //     return dicts.SelectMany(dict => dict)
    //         .ToDictionary(pair => pair.Key, pair => pair.Value);
    // }
}
