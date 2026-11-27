using ClinicApp.Events;
using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    private void WriteLog(string level, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string formattedLine = $"[{timestamp}] [{level.PadRight(5)}] {message}{Environment.NewLine}";
        File.AppendAllText(_logPath, formattedLine, Encoding.UTF8);
    }

    public void LogInfo(string message)
    {
        WriteLog("INFO", message);
    }

    public void LogWarning(string message)
    {
        WriteLog("WARN", message);
    }

    public void LogError(string message)
    {
        WriteLog("ERROR", message);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
        => LogInfo($"Новий пацієнт #{e.PatientId}: {e.FullName}");

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
        => LogInfo($"Запис #{e.AppointmentId} створено: пацієнт #{e.PatientId} → лікар #{e.DoctorId}");

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        string msg = $"ТЕРМІНОВИЙ запис #{e.AppointmentId}: {e.Notes}";
        LogWarning(msg);
        Directory.CreateDirectory("alerts");
        string alertFile = Path.Combine("alerts", $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");
        string alertLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}{Environment.NewLine}";
        File.AppendAllText(alertFile, alertLine, Encoding.UTF8);
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
        => LogInfo($"Запис #{e.AppointmentId} скасовано" + (string.IsNullOrWhiteSpace(e.Notes) ? "" : $" ({e.Notes})"));

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
        => LogInfo($"Запис #{e.AppointmentId} завершено");

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
        => LogInfo($"Оплата запису #{e.AppointmentId}: {e.Amount:F2} грн");

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
        => LogInfo($"План лікування #{e.PlanId} (пацієнт #{e.PatientId}, {e.Diagnosis}) завершено");

    public string[] GetLastLines(int n)
    {
        if (!Exists() || n <= 0)
        {
            return Array.Empty<string>();
        }

        string[] allLines = File.ReadAllLines(_logPath, Encoding.UTF8);
        int skip = allLines.Length > n ? allLines.Length - n : 0;
        int count = allLines.Length - skip;

        string[] result = new string[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = allLines[skip + i];
        }

        return result;
    }

    public void Clear()
    {
        if (File.Exists(_logPath))
        {
            File.Delete(_logPath);
        }
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }
}