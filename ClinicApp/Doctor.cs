using System;

namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Speciality { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
    public int WorkStartHour { get; set; }
    public int WorkEndHour { get; set; }

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }

    public int WorkingHoursPerDay
    {
        get { return WorkEndHour - WorkStartHour; }
    }

    public string WorkSchedule
    {
        get { return $"{WorkStartHour:D2}:00–{WorkEndHour:D2}:00"; }
    }

    public bool IsAvailableNow
    {
        get { return CanAcceptAt(DateTime.Now.Hour); }
    }

    public Doctor()
        : this("Невідомий", "Лікар", "Загальна практика", "LIC-000", "0000000000")
    {
    }

    public Doctor(string firstName, string lastName, string speciality)
        : this(firstName, lastName, speciality, "LIC-000", "0000000000")
    {
    }

    public Doctor(string firstName, string lastName, string speciality, string licenseNumber, string phone)
    {
        Id = _nextId;
        _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        WorkStartHour = 8;
        WorkEndHour = 17;
    }

    public bool CanAcceptAt(int hour)
    {
        if (hour >= WorkStartHour && hour < WorkEndHour)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        string status = "не в робочий час";
        if (IsAvailableNow)
        {
            status = "доступний зараз";
        }

        return $"[{Id}] {FullName} | {Speciality} | {LicenseNumber} | Тел: {Phone} | {WorkSchedule} ({WorkingHoursPerDay} год) | {status}";
    }
}