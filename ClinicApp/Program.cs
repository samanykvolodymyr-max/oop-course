using System;
using System.Globalization;
using System.Text;
using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
Console.OutputEncoding = Encoding.UTF8;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient("Дмитро", "Коваленко", new DateTime(1995, 8, 24), BloodType.ABPositive, "0631112233"));
clinic.Patients.Add(new Patient("Анна", "Шевченко", new DateTime(2015, 1, 5), BloodType.ANegative, "0974445566"));
clinic.Patients.Add(new Patient("Сергій", "Григоренко"));
clinic.Patients.Add(new Patient());
clinic.Patients.Add(new Patient("Вікторія", "Ткачук", new DateTime(1980, 12, 10), BloodType.OPositive, "0502223344"));

Doctor d1 = new Doctor("Василь", "Мельник", Speciality.Orthopedics, "DOC-777", "0509998877", new WorkSchedule(10, 19));
d1.Schedule = new WorkSchedule(8, 16);
Doctor d2 = new Doctor("Ірина", "Лисенко", Speciality.Dermatology) { Schedule = new WorkSchedule(8, 14) };
Doctor d3 = new Doctor();
Doctor d4 = new Doctor("Олексій", "Бойко", Speciality.Surgery, "DOC-101", "0670001122", new WorkSchedule(9, 18));

clinic.Doctors.Add(d1);
clinic.Doctors.Add(d2);
clinic.Doctors.Add(d3);
clinic.Doctors.Add(d4);

clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).AddHours(2));
clinic.Appointments.BookUrgent(2, 2, DateTime.Now.AddDays(1).AddHours(3), "біль у грудях");
clinic.Appointments.BookSpecialist(3, 4, DateTime.Now.AddDays(1).AddHours(4), "кардіологія", 60);

clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", isChronic: true));
clinic.MedicalRecords.Add(new Diagnosis(1, 2, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт", isChronic: false));
clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today.AddDays(-10), "Гемоглобін", 145, "г/л", "120–160", isNormal: true));
clinic.MedicalRecords.Add(new LabResult(1, 2, DateTime.Today.AddDays(-2), "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));
clinic.MedicalRecords.Add(new Prescription(1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(2, 2, DateTime.Today.AddDays(-40), "Амоксицилін", "500 мг", 10, "3 рази на добу"));
clinic.MedicalRecords.Add(new Diagnosis(2, 3, DateTime.Today.AddDays(-15), "L30.9", "Дерматит неуточнений"));
clinic.MedicalRecords.Add(new LabResult(3, 4, DateTime.Today.AddDays(-1), "Глюкоза", 4.8, "ммоль/л", "3.3–5.5", isNormal: true));

Console.WriteLine("\n== new vs override ==");
UrgentAppointment urgentObj = new UrgentAppointment(1, 1, DateTime.Now.AddHours(2), "тест");
Appointment appRef = urgentObj;
UrgentAppointment urgentRef = urgentObj;

Console.WriteLine($"Через Appointment:       {appRef.GetDescription()} | пріоритет {appRef.GetPriority()}");
Console.WriteLine($"Через UrgentAppointment: {urgentRef.GetDescription()} | пріоритет {urgentRef.GetPriority()}");

while (true)
{
    Console.WriteLine("\n╔══════════════════════════════════════════════╗");
    Console.WriteLine("║           МЕДИЧНА КЛІНІКА                    ║");
    Console.WriteLine("╠══════════════════════════════════════════════╣");
    Console.WriteLine("║  1. Пацієнти       — реєстрація, пошук       ║");
    Console.WriteLine("║  2. Лікарі         — персонал, розклад       ║");
    Console.WriteLine("║  3. Записи         — прийоми, скасування     ║");
    Console.WriteLine("║  4. Медична картка — діагнози, рецепти       ║");
    Console.WriteLine("║  5. Рахунки        — оплата, борги           ║");
    Console.WriteLine("║  6. Звіт           — загальна статистика     ║");
    Console.WriteLine("║  0. Вийти                                    ║");
    Console.WriteLine("╚══════════════════════════════════════════════╝");
    Console.Write("Оберіть розділ: ");

    string choice = Console.ReadLine()!;
    if (choice == "1") PatientMenu(clinic);
    else if (choice == "2") DoctorMenu(clinic);
    else if (choice == "3") AppointmentMenu(clinic);
    else if (choice == "4") MedicalRecordMenu(clinic);
    else if (choice == "5") BillingMenu(clinic);
    else if (choice == "6") clinic.GenerateReport();
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
            try
            {
                Console.Write("Ім'я: ");
                string fName = Console.ReadLine()!;
                Console.Write("Прізвище: ");
                string lName = Console.ReadLine()!;
                c.Patients.Add(new Patient(fName, lName));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
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
        Console.WriteLine("5. Вільні години лікаря");
        Console.WriteLine("0. Назад");
        Console.Write("Опція: ");

        string choice = Console.ReadLine()!;
        if (choice == "1") c.Doctors.DisplayAll();
        else if (choice == "2")
        {
            try
            {
                Console.Write("Ім'я: ");
                string fName = Console.ReadLine()!;
                Console.Write("Прізвище: ");
                string lName = Console.ReadLine()!;
                Console.Write("Початок роботи (година 0-23): ");
                int start = int.Parse(Console.ReadLine()!);
                Console.Write("Кінець роботи (година 1-24): ");
                int end = int.Parse(Console.ReadLine()!);

                WorkSchedule schedule = new WorkSchedule(start, end);
                c.Doctors.Add(new Doctor(fName, lName, Speciality.General, "DOC-000", "0000000000", schedule));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
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
        else if (choice == "5")
        {
            Console.Write("ID лікаря: ");
            if (!int.TryParse(Console.ReadLine()!, out int dId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Doctor? doc = c.Doctors.FindById(dId);
            if (doc == null)
            {
                Console.WriteLine("Лікаря не знайдено");
                continue;
            }

            Console.Write("Дата (dd.MM.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine()!, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Помилка: Введено некоректну дату.");
                continue;
            }

            Console.Write("Скільки слотів: ");
            if (!int.TryParse(Console.ReadLine()!, out int slotCount))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            try
            {
                ISchedulable schedulable = doc;
                DateTime[] slots = schedulable.GetAvailableSlots(date, slotCount);
                for (int i = 0; i < slots.Length; i++)
                {
                    Console.WriteLine(slots[i].ToString("HH:mm"));
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
        }
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
        Console.WriteLine("8. Скасувати всі записи пацієнта");
        Console.WriteLine("9. За типом прийому");
        Console.WriteLine("0. Назад");
        Console.Write("Опція: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            c.Appointments.DisplayList(c.Appointments.GetUpcoming());
        }
        else if (choice == "2")
        {
            try
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
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
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
        else if (choice == "8")
        {
            Console.Write("ID пацієнта: ");
            if (!int.TryParse(Console.ReadLine()!, out int pId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("Причина (Enter — без причини): ");
            string reason = Console.ReadLine()!;

            Appointment[] patientApps = c.Appointments.GetByPatient(pId);
            int cancelledCount = AppointmentManager.CancelAll(patientApps, reason);
            Console.WriteLine($"Скасовано записів: {cancelledCount}");
        }
        else if (choice == "9")
        {
            Console.WriteLine("1 — термінові, 2 — консультації, 3 — звичайні");
            Console.Write("Оберіть: ");
            string typeChoice = Console.ReadLine()!;
            if (typeChoice == "1")
            {
                c.Appointments.DisplayList(c.Appointments.GetUrgent());
            }
            else if (typeChoice == "2")
            {
                c.Appointments.DisplayList(c.Appointments.GetSpecialist());
            }
            else if (typeChoice == "3")
            {
                c.Appointments.DisplayList(c.Appointments.GetRegular());
            }
        }
        else if (choice == "0") break;
    }
}

void MedicalRecordMenu(Clinic c)
{
    while (true)
    {
        Console.WriteLine("\n── МЕДИЧНА КАРТКА ────────────");
        Console.WriteLine("1 — Картка пацієнта");
        Console.WriteLine("2 — Усі записи пацієнта");
        Console.WriteLine("3 — Додати діагноз");
        Console.WriteLine("4 — Додати аналіз");
        Console.WriteLine("5 — Додати рецепт");
        Console.WriteLine("6 — Записи лікаря");
        Console.WriteLine("0 — Назад");
        Console.Write("Оберіть: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            Console.Write("ID пацієнта: ");
            if (int.TryParse(Console.ReadLine()!, out int pId))
            {
                c.MedicalRecords.DisplayPatientSummary(pId);
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }
        else if (choice == "2")
        {
            Console.Write("ID пацієнта: ");
            if (int.TryParse(Console.ReadLine()!, out int pId))
            {
                MedicalRecord[] records = c.MedicalRecords.GetByPatient(pId);
                for (int i = 0; i < records.Length; i++)
                {
                    Console.WriteLine($"{records[i].GetRecordType()}: {records[i].GetSummary()}");
                }
                if (records.Length == 0)
                {
                    Console.WriteLine("Записи не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }
        else if (choice == "3")
        {
            Console.WriteLine("\n--- Список пацієнтів ---");
            c.Patients.DisplayAll();
            Console.WriteLine("--- Список лікарів ---");
            c.Doctors.DisplayAll();

            Console.Write("ID пацієнта: ");
            if (!int.TryParse(Console.ReadLine()!, out int pId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("ID лікаря: ");
            if (!int.TryParse(Console.ReadLine()!, out int dId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            if (c.Patients.FindById(pId) == null || c.Doctors.FindById(dId) == null)
            {
                Console.WriteLine("Пацієнта або лікаря з таким ID не знайдено.");
                continue;
            }

            Console.Write("Код діагнозу (напр. J06.9): ");
            string code = Console.ReadLine()!;
            Console.Write("Опис: ");
            string desc = Console.ReadLine()!;
            Console.Write("Хронічне? (1=так, 0=ні): ");
            string chronicStr = Console.ReadLine()!;
            bool isChronic = chronicStr == "1";

            try
            {
                c.MedicalRecords.Add(new Diagnosis(pId, dId, DateTime.Today, code, desc, isChronic));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
        }
        else if (choice == "4")
        {
            Console.WriteLine("\n--- Список пацієнтів ---");
            c.Patients.DisplayAll();
            Console.WriteLine("--- Список лікарів ---");
            c.Doctors.DisplayAll();

            Console.Write("ID пацієнта: ");
            if (!int.TryParse(Console.ReadLine()!, out int pId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("ID лікаря: ");
            if (!int.TryParse(Console.ReadLine()!, out int dId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            if (c.Patients.FindById(pId) == null || c.Doctors.FindById(dId) == null)
            {
                Console.WriteLine("Пацієнта або лікаря з таким ID не знайдено.");
                continue;
            }

            Console.Write("Назва аналізу: ");
            string testName = Console.ReadLine()!;
            Console.Write("Значення (число): ");
            if (!double.TryParse(Console.ReadLine()!, NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("Одиниці виміру: ");
            string unit = Console.ReadLine()!;
            Console.Write("Норма (напр. 4.0–9.0): ");
            string range = Console.ReadLine()!;
            Console.Write("В нормі? (1=так, 0=ні): ");
            string normStr = Console.ReadLine()!;
            bool isNormal = normStr == "1";

            try
            {
                c.MedicalRecords.Add(new LabResult(pId, dId, DateTime.Today, testName, val, unit, range, isNormal));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
        }
        else if (choice == "5")
        {
            Console.WriteLine("\n--- Список пацієнтів ---");
            c.Patients.DisplayAll();
            Console.WriteLine("--- Список лікарів ---");
            c.Doctors.DisplayAll();

            Console.Write("ID пацієнта: ");
            if (!int.TryParse(Console.ReadLine()!, out int pId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("ID лікаря: ");
            if (!int.TryParse(Console.ReadLine()!, out int dId))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            if (c.Patients.FindById(pId) == null || c.Doctors.FindById(dId) == null)
            {
                Console.WriteLine("Пацієнта або лікаря з таким ID не знайдено.");
                continue;
            }

            Console.Write("Препарат: ");
            string med = Console.ReadLine()!;
            Console.Write("Дозування (10 мг): ");
            string dosage = Console.ReadLine()!;
            Console.Write("Кількість днів: ");
            if (!int.TryParse(Console.ReadLine()!, out int days))
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
                continue;
            }

            Console.Write("Інструкція (Enter — пропустити): ");
            string instr = Console.ReadLine()!;

            try
            {
                c.MedicalRecords.Add(new Prescription(pId, dId, DateTime.Today, med, dosage, days, instr));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
        }
        else if (choice == "6")
        {
            Console.Write("ID лікаря: ");
            if (int.TryParse(Console.ReadLine()!, out int dId))
            {
                MedicalRecord[] records = c.MedicalRecords.GetByDoctor(dId);
                c.MedicalRecords.DisplayList(records);
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }
        else if (choice == "0") break;
    }
}

void BillingMenu(Clinic c)
{
    while (true)
    {
        Console.WriteLine("\n── Рахунки ───────────────────");
        Console.WriteLine("1. Борги пацієнта");
        Console.WriteLine("2. Всі неоплачені записи");
        Console.WriteLine("3. Оплатити запис");
        Console.WriteLine("4. Загальний борг клініки");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть: ");

        string choice = Console.ReadLine()!;
        if (choice == "1")
        {
            Console.Write("ID пацієнта: ");
            if (int.TryParse(Console.ReadLine()!, out int pId))
            {
                IPayable[] unpaid = c.Billing.GetUnpaidByPatient(pId);
                if (unpaid.Length > 0)
                {
                    Console.WriteLine($"Неоплачені записи пацієнта #{pId}:");
                }
                c.Billing.DisplayUnpaid(unpaid);
                Console.WriteLine($"Борг: {c.Billing.GetPatientDebt(pId):F2} грн");
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }
        else if (choice == "2")
        {
            IPayable[] unpaid = c.Billing.GetAllUnpaid();
            c.Billing.DisplayUnpaid(unpaid);
        }
        else if (choice == "3")
        {
            Console.Write("ID запису для оплати: ");
            if (int.TryParse(Console.ReadLine()!, out int id))
            {
                if (c.Billing.PayAppointment(id))
                {
                    Console.WriteLine($"Запис [{id}] оплачено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
                }
            }
            else
            {
                Console.WriteLine("Помилка: Введено некоректне число.");
            }
        }
        else if (choice == "4")
        {
            Console.WriteLine($"Загальний борг по клініці: {c.Billing.GetTotalDebt():F2} грн");
        }
        else if (choice == "0") break;
    }
}