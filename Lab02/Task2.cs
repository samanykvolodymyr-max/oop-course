using System;

public class Task2
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        int[] arr = new int[n];

        for (int i = 0; i < n; i++)
        {
            arr[i] = int.Parse(Console.ReadLine()!);
        }

        string before = string.Join(" ", arr);

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        string after = string.Join(" ", arr);

        Console.WriteLine($"Черга (до):    {before}");
        Console.WriteLine($"Черга (після): {after}");
        Console.WriteLine($"Найдешевший:   {arr[0]} грн");
        Console.WriteLine($"Найдорожчий:   {arr[n - 1]} грн");
    }
}