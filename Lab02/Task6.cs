using System;

public class Task6
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        int[][] arr = new int[n][];

        int maxSum = 0;
        int maxIdx = 0;

        for (int i = 0; i < n; i++)
        {
            int k = int.Parse(Console.ReadLine()!);
            arr[i] = new int[k];

            int sum = 0;

            for (int j = 0; j < k; j++)
            {
                arr[i][j] = int.Parse(Console.ReadLine()!);
                sum += arr[i][j];
            }

            double avg = (double)sum / k;
            Console.WriteLine($"Лікар {i + 1}: {k} прийоми, сума={sum} грн, середня={avg:F2} грн");

            if (sum > maxSum)
            {
                maxSum = sum;
                maxIdx = i;
            }
        }

        Console.WriteLine($"Найбільший дохід: Лікар {maxIdx + 1} ({maxSum} грн)");
    }
}