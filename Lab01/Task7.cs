using System.Linq;

public class Task7
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        decimal[] prices = new decimal[n];

        for (int i = 0; i < n; i++)
        {
            prices[i] = decimal.Parse(Console.ReadLine()!);
        }

        decimal sum = 0;
        decimal min = prices[0];
        decimal max = prices[0];

        foreach (decimal price in prices)
        {
            sum += price;
            if (price < min) min = price;
            if (price > max) max = price;
        }

        decimal avg = sum / n;

        int countAboveAvg = 0;
        for (int i = 0; i < n; i++)
        {
            if (prices[i] > avg)
            {
                countAboveAvg++;
            }
        }

        int firstExpensiveIndex = -1;
        int j = 0;
        while (j < n)
        {
            if (prices[j] > 1000)
            {
                firstExpensiveIndex = j;
                break;
            }
            j++;
        }

        string firstExpensiveStr = (firstExpensiveIndex != -1)
            ? $"#{firstExpensiveIndex + 1} — {prices[firstExpensiveIndex]:F2} грн"
            : "немає";

        Console.WriteLine("=== Звіт по прийомах ===");
        Console.WriteLine($"Кількість:        {n}");
        Console.WriteLine($"Загальна сума:    {sum:F2} грн");
        Console.WriteLine($"Середня:          {avg:F2} грн");
        Console.WriteLine($"Мін / Макс:       {min:F2} / {max:F2} грн");
        Console.WriteLine($"Вище середнього:  {countAboveAvg} з {n}");
        Console.WriteLine($"Перший > 1000:    {firstExpensiveStr}");
        Console.WriteLine("========================");
    }
}