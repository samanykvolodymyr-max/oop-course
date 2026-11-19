using ClinicApp.Attributes;
using System;
using System.Reflection;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();
        if (obj == null)
        {
            result.AddError("Object cannot be null.");
            return result;
        }

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];
            object? value = prop.GetValue(obj);

            RequiredAttribute? requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr != null)
            {
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    result.AddError($"{prop.Name}: {requiredAttr.ErrorMessage}");
                }
            }

            MaxLengthAttribute? maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttr != null && value is string strValue)
            {
                if (strValue.Length > maxLengthAttr.Length)
                {
                    string message = string.IsNullOrEmpty(maxLengthAttr.ErrorMessage)
                        ? $"Must not exceed {maxLengthAttr.Length} characters."
                        : maxLengthAttr.ErrorMessage;
                    result.AddError($"{prop.Name}: {message}");
                }
            }

            MinValueAttribute? minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValueAttr != null && value != null)
            {
                try
                {
                    double numericValue = Convert.ToDouble(value);
                    if (numericValue < minValueAttr.Min)
                    {
                        string message = string.IsNullOrEmpty(minValueAttr.ErrorMessage)
                            ? $"Value must be at least {minValueAttr.Min}."
                            : minValueAttr.ErrorMessage;
                        result.AddError($"{prop.Name}: {message}");
                    }
                }
                catch (FormatException)
                {
                }
                catch (InvalidCastException)
                {
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine(type.Name);
        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];
            string attributesText = "";

            foreach (Attribute attr in prop.GetCustomAttributes())
            {
                Type attrType = attr.GetType();
                string? ns = attrType.Namespace;
                if (ns != null && ns.StartsWith("System.Runtime.CompilerServices"))
                    continue;

                string attrName = attrType.Name;
                if (attrName.EndsWith("Attribute"))
                {
                    attrName = attrName.Substring(0, attrName.Length - "Attribute".Length);
                }
                attributesText += $" [{attrName}]";
            }

            if (attributesText.Length > 0)
            {
                Console.WriteLine($"  {prop.Name,-14}{attributesText}");
            }
            else
            {
                Console.WriteLine($"  {prop.Name}");
            }
        }
    }
}