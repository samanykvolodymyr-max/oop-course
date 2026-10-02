using System;

namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Speciality Speciality { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "DOC-000", "", new WorkSchedule(8, 17))
    {
    }

    public Doctor()
        : this("Лікар", "Черговий", Speciality.General, "DOC-000", "", new WorkSchedule(8, 17))
    {
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public override string ToString()
    {
        return $"[{Id}] Dr. {FullName} | {ClinicFormatter.FormatSpeciality(Speciality)} | Ліцензія: {LicenseNumber} | Графік: {Schedule}";
    }
}