using System;

namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count
    {
        get { return _count; }
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Досягнуто ліміт лікарів.");
            return;
        }
        _doctors[_count] = doctor;
        _count++;
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }
        return null;
    }

    public Doctor[] FindBySpeciality(string specialityFragment)
    {
        string search = specialityFragment.ToLower();
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(search))
            {
                matchCount++;
            }
        }

        Doctor[] result = new Doctor[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(search))
            {
                result[index] = _doctors[i];
                index++;
            }
        }
        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];
        Array.Copy(_doctors, result, _count);
        return result;
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
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
            _doctors[i] = _doctors[i + 1];
        }
        _doctors[_count - 1] = null!;
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
        if (_count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i].ToString());
        }
    }

    public void DisplayStats()
    {
        Console.WriteLine("=== Статистика лікарів ===");
        Console.WriteLine($"Всього:         {_count}");
        if (_count == 0)
        {
            Console.WriteLine("==========================");
            return;
        }

        int availableCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }
        Console.WriteLine($"Доступні зараз: {availableCount}");

        Console.WriteLine("По спеціальностях:");
        for (int i = 0; i < _count; i++)
        {
            bool isNewSpeciality = true;
            for (int j = 0; j < i; j++)
            {
                if (_doctors[i].Speciality == _doctors[j].Speciality)
                {
                    isNewSpeciality = false;
                    break;
                }
            }

            if (isNewSpeciality)
            {
                int specCount = 0;
                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Speciality == _doctors[i].Speciality)
                    {
                        specCount++;
                    }
                }
                Console.WriteLine($"  {_doctors[i].Speciality}: {specCount}");
            }
        }
        Console.WriteLine("==========================");
    }
}