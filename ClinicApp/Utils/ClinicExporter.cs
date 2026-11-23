using ClinicApp.Interfaces;
using ClinicApp.Models;
using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    private string GetReportFilePath(string fileName)
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string directoryPath = Path.Combine(_baseDir, dateFolder);
        Directory.CreateDirectory(directoryPath);
        return Path.Combine(directoryPath, fileName);
    }

    public string ExportPatients()
    {
        string filePath = GetReportFilePath("patients.txt");
        Patient[] patients = _clinic.Patients.GetAll();

        using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
        writer.WriteLine("=== Пацієнти — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < patients.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {patients[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {patients.Length}");

        return filePath;
    }

    public string ExportAppointments()
    {
        string filePath = GetReportFilePath("appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
        writer.WriteLine("=== Записи на прийом — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < appointments.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {appointments[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {appointments.Length}");

        return filePath;
    }

    public string ExportBilling()
    {
        string filePath = GetReportFilePath("billing.txt");
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();
        decimal totalDebt = _clinic.Billing.GetTotalDebt();

        using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
        writer.WriteLine("=== Рахунки та борги — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < unpaid.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {unpaid[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього неоплачених: {unpaid.Length} | Загальний борг: {totalDebt:F2} грн");

        return filePath;
    }

    public string ExportTreatmentPlans()
    {
        string filePath = GetReportFilePath("treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
        writer.WriteLine("=== Плани лікування — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine("----------------------------------------");

        for (int i = 0; i < plans.Length; i++)
        {
            writer.WriteLine($"{i + 1,3}. {plans[i]}");
        }

        writer.WriteLine("----------------------------------------");
        writer.WriteLine($"Всього: {plans.Length}");

        return filePath;
    }

    public string[] ExportAll()
    {
        return new string[]
        {
            ExportPatients(),
            ExportAppointments(),
            ExportBilling(),
            ExportTreatmentPlans()
        };
    }
}