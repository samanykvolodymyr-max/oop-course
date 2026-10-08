using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set => _licenseNumber = value;
    }

    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    public Speciality Speciality { get; set; }
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