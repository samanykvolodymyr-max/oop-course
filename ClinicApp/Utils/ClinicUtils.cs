using System;

namespace ClinicApp.Utils;

public static class ClinicUtils
{
    public static bool IsValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return false;

        int digitCount = 0;
        for (int i = 0; i < phone.Length; i++)
        {
            if (char.IsDigit(phone[i])) digitCount++;
        }
        return digitCount >= 10;
    }

    public static string FormatName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "";
        name = name.Trim();
        if (name.Length == 1) return name.ToUpper();
        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }
}