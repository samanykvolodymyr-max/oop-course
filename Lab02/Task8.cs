using System;

public class Task8
{
    public static void Run()
    {
        int d = int.Parse(Console.ReadLine()!);
        int w = int.Parse(Console.ReadLine()!);

        int[,,] arr = new int[d, w, 2];
        int[] totals = new int[d];

        for (int i = 0; i < d; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    arr[i, j, k] = int.Parse(Console.ReadLine()!);
                }
            }
        }

        int maxSum = 0;
        int maxIdx = 0;

        for (int i = 0; i < d; i++)
        {
            Console.WriteLine($"Відділення {i + 1}:");
            int depSum = 0;

            for (int j = 0; j < w; j++)
            {
                int morning = arr[i, j, 0];
                int evening = arr[i, j, 1];
                int weekSum = morning + evening;

                Console.WriteLine($"  Тиждень {j + 1}: ранок {morning}, вечір {evening} -> разом {weekSum}");
                depSum += weekSum;
            }

            Console.WriteLine($"  Разом: {depSum} пацієнтів");
            totals[i] = depSum;

            if (depSum > maxSum)
            {
                maxSum = depSum;
                maxIdx = i;
            }
        }

        Console.WriteLine($"Найзавантаженіше: Відділення {maxIdx + 1} ({maxSum} пацієнтів)");
    }
}