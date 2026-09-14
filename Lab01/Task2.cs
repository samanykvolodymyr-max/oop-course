using System;

public class Task2
{
    public static void Run()
    {
        double price = double.Parse(Console.ReadLine()!);
        int count = int.Parse(Console.ReadLine()!);
        int discount = int.Parse(Console.ReadLine()!);

        double total = price * count * (1 - discount / 100.0);

        Console.WriteLine($"Сума: {total:F2} грн");
    }
}