using System;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxCapacity = 1000;
    private readonly MedicalRecord[] _records = new MedicalRecord[MaxCapacity];
    private int _count = 0;

    public int Count => _count;

    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _records[index];
        }
    }

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxCapacity)
        {
            Console.WriteLine("Неможливо додати запис: досягнуто ліміту.");
            return;
        }

        _records[_count] = record;
        _count++;
        Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
    }

    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
            {
                return _records[i];
            }
        }
        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                matchCount++;
            }
        }

        MedicalRecord[] result = new MedicalRecord[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                result[index++] = _records[i];
            }
        }
        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                matchCount++;
            }
        }

        MedicalRecord[] result = new MedicalRecord[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                result[index++] = _records[i];
            }
        }
        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Медичні записи відсутні.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i].ToString());
        }
    }

    public void DisplayList(MedicalRecord[] list)
    {
        if (list.Length == 0)
        {
            Console.WriteLine("Записи не знайдено.");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            Console.WriteLine(list[i].ToString());
        }
    }

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
            {
                matchCount++;
            }
        }

        Diagnosis[] result = new Diagnosis[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d)
            {
                result[index++] = d;
            }
        }
        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is LabResult)
            {
                matchCount++;
            }
        }

        LabResult[] result = new LabResult[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is LabResult lr)
            {
                result[index++] = lr;
            }
        }
        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription)
            {
                matchCount++;
            }
        }

        Prescription[] result = new Prescription[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription p)
            {
                result[index++] = p;
            }
        }
        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
            {
                matchCount++;
            }
        }

        Diagnosis[] result = new Diagnosis[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
            {
                result[index++] = d;
            }
        }
        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
            {
                matchCount++;
            }
        }

        Prescription[] result = new Prescription[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
            {
                result[index++] = p;
            }
        }
        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        int total = 0;
        int diagnosesCount = 0;
        int labResultsCount = 0;
        int prescriptionsCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                total++;
                if (_records[i] is Diagnosis) diagnosesCount++;
                else if (_records[i] is LabResult) labResultsCount++;
                else if (_records[i] is Prescription) prescriptionsCount++;
            }
        }

        if (total == 0)
        {
            Console.WriteLine($"Медичні записи для пацієнта #{patientId} відсутні.");
            return;
        }

        Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
        Console.WriteLine($"Всього записів: {total} (діагнозів: {diagnosesCount}, аналізів: {labResultsCount}, рецептів: {prescriptionsCount})");

        Diagnosis[] chronic = GetChronicDiagnoses(patientId);
        if (chronic.Length > 0)
        {
            Console.WriteLine($"Хронічні діагнози ({chronic.Length}):");
            for (int i = 0; i < chronic.Length; i++)
            {
                Console.WriteLine($"  {chronic[i]}");
            }
        }

        Prescription[] activeRx = GetActivePrescriptions(patientId);
        if (activeRx.Length > 0)
        {
            Console.WriteLine($"Активні рецепти ({activeRx.Length}):");
            for (int i = 0; i < activeRx.Length; i++)
            {
                Console.WriteLine($"  {activeRx[i]} | до {activeRx[i].ExpiresAt:dd.MM.yyyy}");
            }
        }
    }
}