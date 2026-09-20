using System;
using System.Globalization;

System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Console.Write("Введіть номер завдання: ");
string choice = Console.ReadLine()!;

if (choice == "1") Task1.Run();
else if (choice == "2") Task2.Run();
else if (choice == "3") Task3.Run();