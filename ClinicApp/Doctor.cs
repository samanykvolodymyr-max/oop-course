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

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }

    public int WorkingHoursPerDay
    {
        get { return Schedule.TotalHours; }
    }

    public bool IsAvailableNow
    {
        get { return CanAcceptAt(DateTime.Now.Hour); }
    }

    public Doctor()
        : this("Невідомий", "Лікар", Speciality.General, "LIC-000", "0000000000", new WorkSchedule(8, 17))
    {
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "LIC-000", "0000000000", new WorkSchedule(8, 17))
    {
    }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone)
        : this(firstName, lastName, speciality, licenseNumber, phone, new WorkSchedule(8, 17))
    {
    }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        Id = _nextId;
        _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.IsWorkingAt(hour);
    }

    public override string ToString()
    {
        string status = IsAvailableNow ? "доступний зараз" : "не в робочий час";
        return $"[{Id}] {FullName} | {Speciality} | {LicenseNumber} | Тел: {Phone} | {Schedule} ({WorkingHoursPerDay} год) | {status}";
    }
}