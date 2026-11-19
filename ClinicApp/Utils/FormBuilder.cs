using ClinicApp.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];
            if (prop.GetSetMethod() == null)
            {
                continue;
            }

            List<string> hints = new List<string>();

            RequiredAttribute? requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr != null)
            {
                hints.Add("обов'язкове");
            }

            MaxLengthAttribute? maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttr != null)
            {
                hints.Add($"макс. {maxLengthAttr.Length}");
            }

            MinValueAttribute? minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValueAttr != null)
            {
                hints.Add($"мін. {minValueAttr.Min}");
            }

            string prompt = hints.Count > 0
                ? $"  {prop.Name} ({string.Join(", ", hints)}): "
                : $"  {prop.Name}: ";

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                try
                {
                    object converted = prop.PropertyType.IsEnum
                        ? Enum.Parse(prop.PropertyType, input, ignoreCase: true)
                        : Convert.ChangeType(input, prop.PropertyType);

                    prop.SetValue(obj, converted);
                    break;
                }
                catch
                {
                    Console.WriteLine("  Некоректне значення, спробуйте ще раз.");
                }
            }
        }

        return obj;
    }
}