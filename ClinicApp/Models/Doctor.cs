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
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер ліцензії не може бути порожнім.", nameof(LicenseNumber));
            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            ClinicValidator.ValidatePhone(value);
            _phone = value;
        }
    }

    public Speciality Speciality { get; set; }
    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;

        Id = _nextId++;
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "DOC-000", "0000000000", new WorkSchedule(8, 17))
    {
    }

    public Doctor()
        : this("Лікар", "Черговий", Speciality.General, "DOC-000", "0000000000", new WorkSchedule(8, 17))
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