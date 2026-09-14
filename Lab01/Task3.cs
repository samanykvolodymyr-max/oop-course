using System;

public class Task3
{
    public static void Run()
    {
        int birthYear = int.Parse(Console.ReadLine()!);
        int age = 2026 - birthYear;

        string category;
        if (age < 18)
        {
            category = "дитина";
        }
        else if (age <= 59)
        {
            category = "дорослий";
        }
        else
        {
            category = "пенсіонер";
        }

        Console.WriteLine($"Вік: {age} р.");
        Console.WriteLine($"Категорія: {category}");
    }
}