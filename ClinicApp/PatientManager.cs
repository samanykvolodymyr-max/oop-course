using System;

namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count
    {
        get { return _count; }
    }

    public void Add(Patient patient)
    {
        if (_count >= MaxPatients)
        {
            Console.WriteLine("Досягнуто ліміт пацієнтів.");
            return;
        }

        _patients[_count] = patient;
        _count++;
        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
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
        int matchCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                matchCount++;
            }
        }

        Patient[] result = new Patient[matchCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                result[index] = _patients[i];
                index++;
            }
        }
        return result;
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1)
        {
            return false;
        }

        for (int i = indexToRemove; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        _patients[_count - 1] = null!;
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i].ToString());
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_count}");

        if (_count == 0)
        {
            Console.WriteLine("============================");
            return;
        }

        double sumAge = 0;
        int adultCount = 0;
        int minAgeIndex = 0;
        int maxAgeIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            int currentAge = _patients[i].Age;
            sumAge += currentAge;

            if (_patients[i].IsAdult)
            {
                adultCount++;
            }

            if (currentAge < _patients[minAgeIndex].Age)
            {
                minAgeIndex = i;
            }
            if (currentAge > _patients[maxAgeIndex].Age)
            {
                maxAgeIndex = i;
            }
        }

        double avgAge = sumAge / _count;
        Console.WriteLine($"Середній вік: {avgAge:F1} р.");
        Console.WriteLine($"Наймолодший:  {_patients[minAgeIndex].FullName} ({_patients[minAgeIndex].Age} р.)");
        Console.WriteLine($"Найстарший:   {_patients[maxAgeIndex].FullName} ({_patients[maxAgeIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adultCount} з {_count}");
        Console.WriteLine("============================");
    }
}