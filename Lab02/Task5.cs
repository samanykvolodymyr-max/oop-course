using System;

public class Task5
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        int[,] matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine()!.Split(' ');
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(parts[j]);
            }
        }

        int mainDiagSum = 0;
        int sideDiagSum = 0;

        for (int i = 0; i < n; i++)
        {
            mainDiagSum += matrix[i, i];
            sideDiagSum += matrix[i, n - 1 - i];
        }

        Console.WriteLine($"Головна діагональ: {mainDiagSum}");
        Console.WriteLine($"Побічна діагональ: {sideDiagSum}");

        if (mainDiagSum > sideDiagSum)
        {
            Console.WriteLine("Переважає головна діагональ");
        }
        else if (sideDiagSum > mainDiagSum)
        {
            Console.WriteLine("Переважає побічна діагональ");
        }
        else
        {
            Console.WriteLine("Діагоналі рівні");
        }
    }
}