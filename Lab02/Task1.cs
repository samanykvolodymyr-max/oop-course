using System;

public class Task1
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        double[] arr = new double[n];

        for (int i = 0; i < n; i++)
        {
            arr[i] = double.Parse(Console.ReadLine()!);
        }

        double sum = 0;
        double min = arr[0];
        double max = arr[0];

        for (int i = 0; i < n; i++)
        {
            sum += arr[i];

            if (arr[i] < min)
            {
                min = arr[i];
            }
            if (arr[i] > max)
            {
                max = arr[i];
            }
        }

        double avg = sum / n;
        int count = 0;

        for (int i = 0; i < n; i++)
        {
            if (arr[i] > avg)
            {
                count++;
            }
        }

        Console.WriteLine($"Кількість: {n} / Середня вага: {avg:F1} кг / Мін / Макс: {min:F1} / {max:F1} кг / Вище середнього: {count} з {n}");
    }
}