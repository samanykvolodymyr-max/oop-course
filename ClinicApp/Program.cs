using System;
using ClinicApp;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient("Дмитро", "Коваленко", new DateTime(1995, 8, 24), "AB+", "0631112233"));
clinic.Patients.Add(new Patient("Анна", "Шевченко", new DateTime(2015, 1, 5), "A-", "0974445566"));
clinic.Patients.Add(new Patient("Сергій", "Григоренко"));
clinic.Patients.Add(new Patient());
clinic.Patients.Add(new Patient("Вікторія", "Ткачук", new DateTime(1980, 12, 10), "O+", "0502223344"));

Doctor d1 = new Doctor("Василь", "Мельник", "Травматолог", "DOC-777", "0509998877") { WorkStartHour = 10, WorkEndHour = 19 };
Doctor d2 = new Doctor("Ірина", "Лисенко", "Дерматолог") { WorkStartHour = 8, WorkEndHour = 14 };
Doctor d3 = new Doctor();
Doctor d4 = new Doctor("Олексій", "Бойко", "Хірург", "DOC-101", "0670001122") { WorkStartHour = 9, WorkEndHour = 18 };

clinic.Doctors.Add(d1);
clinic.Doctors.Add(d2);
clinic.Doctors.Add(d3);
clinic.Doctors.Add(d4);

clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).AddHours(2));
clinic.Appointments.Book(2, 2, DateTime.Now.AddDays(1).AddHours(3));

while (true)
{
    Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ КЛІНІКИ ===");
    Console.WriteLine("1. Пацієнти");
    Console.WriteLine("2. Лікарі");
    Console.WriteLine("3. Записи");
    Console.WriteLine("4. Розклад на сьогодні");
    Console.WriteLine("5. Згенерувати звіт");
    Console.WriteLine("6. Тест GrowablePatientManager");
    Console.WriteLine("0. Вийти");
    Console.Write("Виберіть опцію: ");

    string choice = Console.ReadLine()!;
    if (choice == "1") PatientMenu(clinic);
    else if (choice == "2") DoctorMenu(clinic);
    else if (choice == "3") AppointmentMenu(clinic);
    else if (choice == "4") clinic.DisplaySchedule(DateTime.Now.AddDays(1));
    else if (choice == "5") clinic.GenerateReport();
    else if (choice == "6") TestGrowableManager();
    else if (choice == "0") break;
}


void PatientMenu(Clinic c)
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
        if (choice == "1") c.Patients.DisplayAll();
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string fName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            string lName = Console.ReadLine()!;
            c.Patients.Add(new Patient(fName, lName));
        }
        else if (choice == "3")
        {
            Console.Write("Введіть частину імені: ");
            string search = Console.ReadLine()!;
            Patient[] found = c.Patients.FindByName(search);
            for (int i = 0; i < found.Length; i++) Console.WriteLine(found[i].ToString());
        }
        else if (choice == "4")
        {
            Console.Write("ID для видалення: ");
            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                Console.WriteLine(c.Patients.Remove(id) ? "Видалено." : "Не знайдено.");
            }
        }
        else if (choice == "5") c.Patients.DisplayStats();
        else if (choice == "0") break;
    }
}

void DoctorMenu(Clinic c)
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
        if (choice == "1") c.Doctors.DisplayAll();
        else if (choice == "2")
        {
            Console.Write("Ім'я: ");
            string fName = Console.ReadLine()!;
            Console.Write("Прізвище: ");
            string lName = Console.ReadLine()!;
            Console.Write("Спеціальність: ");
            string spec = Console.ReadLine()!;
            c.Doctors.Add(new Doctor(fName, lName, spec));
        }
        else if (choice == "3")
        {
            Console.Write("Спеціальність: ");
            string search = Console.ReadLine()!;
            Doctor[] found = c.Doctors.FindBySpeciality(search);
            for (int i = 0; i < found.Length; i++) Console.WriteLine(found[i].ToString());
        }
        else if (choice == "4")
        {
            Console.Write("ID для видалення: ");
            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                Console.WriteLine(c.Doctors.Remove(id) ? "Видалено." : "Не знайдено.");
            }
        }
        else if (choice == "5") c.Doctors.DisplayStats();
        else if (choice == "0") break;
    }
}

void AppointmentMenu(Clinic c)
{
    while (true)
    {
        Console.WriteLine("\n--- ЗАПИСИ ---");
        Console.WriteLine("1. Усі майбутні записи");
        Console.WriteLine("2. Забронювати прийом");
        Console.WriteLine("3. Скасувати запис");
        Console.WriteLine("4. Завершити прийом");
        Console.WriteLine("0. Назад");
        Console.Write("Опція: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            c.Appointments.DisplayList(c.Appointments.GetUpcoming());
        }
        else if (choice == "2")
        {
            Console.WriteLine("\n--- Список пацієнтів ---");
            c.Patients.DisplayAll();
            Console.WriteLine("--- Список лікарів ---");
            c.Doctors.DisplayAll();

            Console.Write("Введіть ID пацієнта: ");
            int pId = int.Parse(Console.ReadLine()!);
            Console.Write("Введіть ID лікаря: ");
            int dId = int.Parse(Console.ReadLine()!);

            c.Appointments.Book(pId, dId, DateTime.Now.AddDays(1));
        }
        else if (choice == "3")
        {
            Console.Write("ID запису для скасування: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.Write("Причина: ");
            string reason = Console.ReadLine()!;
            c.Appointments.Cancel(id, reason);
        }
        else if (choice == "4")
        {
            Console.Write("ID запису для завершення: ");
            int id = int.Parse(Console.ReadLine()!);
            c.Appointments.Complete(id);
        }
        else if (choice == "0") break;
    }
}

void TestGrowableManager()
{
    Console.WriteLine("\n=== Тест GrowablePatientManager ===");
    GrowablePatientManager growable = new GrowablePatientManager();
    Console.WriteLine("Додаємо 20 пацієнтів підряд...");

    for (int i = 1; i <= 20; i++)
    {
        growable.Add(new Patient("Пацієнт" + i, "Тестовий" + i));
    }

    Console.WriteLine("\nТест пошуку:");
    Patient? found = growable.FindById(10);
    Console.WriteLine(found != null ? "Знайдено ID 10: " + found.FullName : "ID 10 не знайдено");

    Patient? notFound = growable.FindById(99);
    Console.WriteLine(notFound != null ? "Знайдено ID 99: " + notFound.FullName : "ID 99 не знайдено");
}