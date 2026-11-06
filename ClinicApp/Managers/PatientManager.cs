using ClinicApp.Enums;
using ClinicApp.Models;
using System;
using System.Collections.Generic;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new List<Patient>();

    public int Count => _patients.Count;

    public Patient this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count) return null!;
            return _patients[index];
        }
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public bool TryFindById(int id, out Patient patient)
    {
        patient = FindById(id)!;
        return patient != null;
    }

    public (int Total, int Adults, double AvgAge) GetSummaryTuple()
    {
        if (_patients.Count == 0) return (0, 0, 0.0);

        double sumAge = 0;
        int adultCount = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            sumAge += _patients[i].Age;
            if (_patients[i].IsAdult) adultCount++;
        }

        return (_patients.Count, adultCount, sumAge / _patients.Count);
    }

    public void Add(Patient patient)
    {
        _patients.Add(patient);
        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    public Patient[] FindByName(string nameFragment)
    {
        string search = nameFragment.ToLower();
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                result.Add(_patients[i]);
            }
        }
        return result.ToArray();
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result.Add(_patients[i]);
            }
        }
        return result.ToArray();
    }

    public bool Remove(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                _patients.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Пацієнти ({_patients.Count}) ===");
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < _patients.Count; i++)
        {
            Console.WriteLine(_patients[i].ToString());
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_patients.Count}");

        if (_patients.Count == 0)
        {
            Console.WriteLine("============================");
            return;
        }

        double sumAge = 0;
        int adultCount = 0;
        int minAgeIndex = 0;
        int maxAgeIndex = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            int currentAge = _patients[i].Age;
            sumAge += currentAge;

            if (_patients[i].IsAdult) adultCount++;

            if (currentAge < _patients[minAgeIndex].Age) minAgeIndex = i;
            if (currentAge > _patients[maxAgeIndex].Age) maxAgeIndex = i;
        }

        double avgAge = sumAge / _patients.Count;
        Console.WriteLine($"Середній вік: {avgAge:F1} р.");
        Console.WriteLine($"Наймолодший:  {_patients[minAgeIndex].FullName} ({_patients[minAgeIndex].Age} р.)");
        Console.WriteLine($"Найстарший:   {_patients[maxAgeIndex].FullName} ({_patients[maxAgeIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adultCount} з {_patients.Count}");
        Console.WriteLine("============================");
    }
}