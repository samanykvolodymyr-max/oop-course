using System;

Console.Write("Введіть номер завдання: ");
string choice = Console.ReadLine()!;

if (choice == "1")
{
    Task1.Run();
}
else if (choice == "2")
{
    Task2.Run();
}
else if (choice == "3")
{
    Task3.Run();
}
else if (choice == "4")
{
    Task4.Run();
}
else if (choice == "5")
{
    Task5.Run();
}