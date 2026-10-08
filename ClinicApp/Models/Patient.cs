using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public BloodType BloodType { get; set; }
    public string Phone { get; set; }

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
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        BloodType = bloodType;
        Phone = phone;
    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName, DateTime.Today, BloodType.Unknown, "")
    {
    }

    public Patient()
        : this("Анонім", "Анонімов", DateTime.Today, BloodType.Unknown, "")
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