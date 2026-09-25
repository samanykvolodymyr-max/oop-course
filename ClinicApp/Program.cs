using System;
using ClinicApp;

PatientManager patientManager = new PatientManager();
DoctorManager doctorManager = new DoctorManager();

// --- 5 Пацієнтів (вимоги Задачі 1) ---
Patient p1 = new Patient("Дмитро", "Коваленко", new DateTime(1995, 8, 24), "AB+", "0631112233");
Patient p2 = new Patient("Анна", "Шевченко", new DateTime(2015, 1, 5), "A-", "0974445566");
Patient p3 = new Patient("Сергій", "Григоренко");
Patient p4 = new Patient();
Patient p5 = new Patient("Вікторія", "Ткачук", new DateTime(1980, 12, 10), "O+", "0502223344");

patientManager.Add(p1);
patientManager.Add(p2);
patientManager.Add(p3);
patientManager.Add(p4);
patientManager.Add(p5);

// --- 4 Лікарі (вимоги Задачі 2) ---
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

doctorManager.Add(d1);
doctorManager.Add(d2);
doctorManager.Add(d3);
doctorManager.Add(d4);

// --- ГОЛОВНИЙ ЦИКЛ ---
while (true)
{
    Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
    Console.WriteLine("1. Пацієнти");
    Console.WriteLine("2. Лікарі");
    Console.WriteLine("3. Тест прийомів (Appointment)");
    Console.WriteLine("0. Вийти");
    Console.Write("Виберіть опцію: ");

    string choice = Console.ReadLine()!;
    if (choice == "1")
    {
        PatientMenu();
    }
    else if (choice == "2")
    {
        DoctorMenu();
    }
    else if (choice == "3")
    {
        TestAppointments();
    }
    else if (choice == "0")
    {
        break;
    }
    else
    {
        Console.WriteLine("Неправильний вибір.");
    }
}

// --- ЛОКАЛЬНІ МЕТОДИ (ПІДМЕНЮ) ---

void PatientMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- ПАЦІЄНТИ ---");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати пацієнта");
        Console.WriteLine("3. Знайти за ім'ям");
        Console.WriteLine("4. Видалити");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Опція: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            patientManager.DisplayAll();
        }
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string fName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            string lName = Console.ReadLine()!;
            patientManager.Add(new Patient(fName, lName));
        }
        else if (choice == "3")
        {
            Console.Write("Введіть частину імені: ");
            string search = Console.ReadLine()!;
            Patient[] found = patientManager.FindByName(search);
            Console.WriteLine($"Знайдено: {found.Length}");
            for (int i = 0; i < found.Length; i++) Console.WriteLine(found[i].ToString());
        }
        else if (choice == "4")
        {
            Console.Write("Введіть ID для видалення: ");
            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                bool success = patientManager.Remove(id);
                Console.WriteLine(success ? "Видалено успішно." : "Не знайдено.");
            }
        }
        else if (choice == "5")
        {
            patientManager.DisplayStats();
        }
        else if (choice == "0")
        {
            break;
        }
    }
}

void DoctorMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- ЛІКАРІ ---");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати лікаря");
        Console.WriteLine("3. Знайти за спеціальністю");
        Console.WriteLine("4. Видалити");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Опція: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            doctorManager.DisplayAll();
        }
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string fName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            string lName = Console.ReadLine()!;
            Console.Write("Спеціальність: ");
            string spec = Console.ReadLine()!;
            doctorManager.Add(new Doctor(fName, lName, spec));
        }
        else if (choice == "3")
        {
            Console.Write("Введіть спеціальність: ");
            string search = Console.ReadLine()!;
            Doctor[] found = doctorManager.FindBySpeciality(search);
            Console.WriteLine($"Знайдено: {found.Length}");
            for (int i = 0; i < found.Length; i++) Console.WriteLine(found[i].ToString());
        }
        else if (choice == "4")
        {
            Console.Write("Введіть ID для видалення: ");
            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                bool success = doctorManager.Remove(id);
                Console.WriteLine(success ? "Видалено успішно." : "Не знайдено.");
            }
        }
        else if (choice == "5")
        {
            doctorManager.DisplayStats();
        }
        else if (choice == "0")
        {
            break;
        }
    }
}

void TestAppointments()
{
    Console.WriteLine("\n--- ТЕСТУВАННЯ ПРИЙОМІВ ---");
    Appointment app1 = new Appointment(1, 1, DateTime.Now.AddDays(1).AddHours(2));
    Appointment app2 = new Appointment(2, 2, DateTime.Now.AddHours(-1), 45);

    Console.WriteLine(app1);
    Console.WriteLine(app2);

    Console.WriteLine("\nСкасовуємо перший прийом...");
    app1.Cancel("Пацієнт захворів");
    Console.WriteLine(app1);

    Console.WriteLine("Завершуємо другий прийом...");
    app2.Complete();
    Console.WriteLine(app2);
}