using System;
using ClinicApp;

Patient p1 = new Patient("Дмитро", "Коваленко", new DateTime(1995, 8, 24), "AB+", "0631112233");
Patient p2 = new Patient("Анна", "Шевченко", new DateTime(2015, 1, 5), "A-", "0974445566");
Patient p3 = new Patient("Сергій", "Григоренко"); 
Patient p4 = new Patient(); 
Patient p5 = new Patient("Вікторія", "Ткачук", new DateTime(1980, 12, 10), "O+", "0502223344");

Console.WriteLine("--- Пацієнти ---");
Console.WriteLine(p1);
Console.WriteLine(p2);
Console.WriteLine(p3);
Console.WriteLine(p4);
Console.WriteLine(p5);
Console.WriteLine();

Doctor d1 = new Doctor("Василь", "Мельник", "Травматолог", "DOC-777", "0509998877");
d1.WorkStartHour = 10;
d1.WorkEndHour = 19;

Doctor d2 = new Doctor("Ірина", "Лисенко", "Дерматолог");
d2.WorkStartHour = 8;
d2.WorkEndHour = 14;

Doctor d3 = new Doctor(); 

Doctor d4 = new Doctor("Олексій", "Бойко", "Хірург", "DOC-101", "0670001122");
d4.WorkStartHour = 9;
d4.WorkEndHour = 18;

Console.WriteLine("--- Лікарі ---");
Console.WriteLine(d1);
Console.WriteLine(d2);
Console.WriteLine(d3);
Console.WriteLine(d4);