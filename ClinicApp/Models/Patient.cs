using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private DateTime _birthDate;
    private string _phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Ім'я не може бути порожнім або довшим за 50 символів.", nameof(FirstName));
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Прізвище не може бути порожнім або довшим за 50 символів.", nameof(LastName));
            _lastName = value;
        }
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (value > DateTime.Today || value.Year < 1900)
                throw new ArgumentOutOfRangeException(nameof(BirthDate), "Дата народження не може бути в майбутньому або раніше 1900 року.");
            _birthDate = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
                throw new ArgumentException("Номер телефону має містити рівно 10 символів.", nameof(Phone));

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                    throw new ArgumentException("Номер телефону має містити лише цифри.", nameof(Phone));
            }
            _phone = value;
        }
    }

    public BloodType BloodType { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public bool IsAdult => Age >= 18;

    public Patient(string firstName, string lastName, DateTime birthDate, BloodType bloodType, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        BloodType = bloodType;
        Phone = phone;

        Id = _nextId++;
    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName, DateTime.Today, BloodType.Unknown, "0000000000")
    {
    }

    public Patient()
        : this("Анонім", "Анонімов", DateTime.Today, BloodType.Unknown, "0000000000")
    {
    }

    public void Deconstruct(out string fullName, out int age, out BloodType bloodType)
    {
        fullName = FullName;
        age = Age;
        bloodType = BloodType;
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | {ClinicFormatter.FormatAge(Age)} | Група: {ClinicFormatter.FormatBloodType(BloodType)} | Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}