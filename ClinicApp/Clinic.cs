using System;

namespace ClinicApp;

public class Clinic
{
    public string Name { get; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");
        Appointment[] dailyAppointments = Appointments.GetByDate(date);
        Appointments.DisplayList(dailyAppointments);
    }

    public void GenerateReport()
    {
        Appointment[] upcoming = Appointments.GetUpcoming();
        Doctor[] allDoctors = Doctors.GetAll();

        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine($"║  Звіт — {Name,-36} ║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine($"║  Пацієнтів:          {Patients.Count,-24} ║");
        Console.WriteLine($"║  Лікарів:            {Doctors.Count,-24} ║");
        Console.WriteLine($"║  Майбутніх записів:  {upcoming.Length,-24} ║");
        Console.WriteLine("╠══════════════════════════════════════════════╣");
        Console.WriteLine("║  Навантаження лікарів (майбутні записи):     ║");

        for (int i = 0; i < allDoctors.Length; i++)
        {
            Doctor doc = allDoctors[i];
            int docLoad = 0;

            for (int j = 0; j < upcoming.Length; j++)
            {
                if (upcoming[j].DoctorId == doc.Id)
                {
                    docLoad++;
                }
            }

            string docInfo = $"  {doc.FullName} ({doc.Speciality}): {docLoad} записів";
            Console.WriteLine($"║  {docInfo,-44} ║");
        }

        Console.WriteLine("╚══════════════════════════════════════════════╝");
    }
}