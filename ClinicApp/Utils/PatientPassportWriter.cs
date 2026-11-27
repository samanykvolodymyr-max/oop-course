using ClinicApp.Events;
using ClinicApp.Models;
using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _directory;

    public PatientPassportWriter(Clinic clinic, string directory = "patients")
    {
        _clinic = clinic;
        _directory = directory;
        Directory.CreateDirectory(_directory);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
        => WritePassport(e.PatientId);

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
        => WritePassport(e.PatientId);

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
        => WritePassport(e.PatientId);

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);
        if (patient == null) return;

        string filePath = Path.Combine(_directory, $"passport_{patientId}.txt");

        using StreamWriter writer = new StreamWriter(filePath, append: false, Encoding.UTF8);

        writer.WriteLine($"=== Паспорт пацієнта #{patient.Id} ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine($"Ім'я: {patient.FullName}");
        writer.WriteLine($"Дата народження: {patient.BirthDate:dd.MM.yyyy} ({patient.Age} років)");
        writer.WriteLine($"Група крові: {ClinicFormatter.FormatBloodType(patient.BloodType)}   Телефон: {ClinicFormatter.FormatPhone(patient.Phone)}");

        MedicalRecord[] records = _clinic.MedicalRecords.GetByPatient(patientId);

        writer.WriteLine("-- Діагнози --");
        bool hasDiagnoses = false;
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is Diagnosis d)
            {
                writer.WriteLine(d.GetSummary());
                hasDiagnoses = true;
            }
        }
        if (!hasDiagnoses) writer.WriteLine("Немає");

        writer.WriteLine("-- Аналізи --");
        bool hasLabs = false;
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is LabResult l)
            {
                writer.WriteLine(l.GetSummary());
                hasLabs = true;
            }
        }
        if (!hasLabs) writer.WriteLine("Немає");

        writer.WriteLine("-- Рецепти --");
        bool hasPrescriptions = false;
        for (int i = 0; i < records.Length; i++)
        {
            if (records[i] is Prescription p)
            {
                writer.WriteLine(p.GetSummary());
                hasPrescriptions = true;
            }
        }
        if (!hasPrescriptions) writer.WriteLine("Немає");

        writer.WriteLine("-- Записи --");
        Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);
        if (appointments.Length == 0)
        {
            writer.WriteLine("Немає");
        }
        else
        {
            for (int i = 0; i < appointments.Length; i++)
            {
                Appointment app = appointments[i];
                writer.WriteLine($"[{app.Id}] {app.GetDescription()} | {app.DateTime:dd.MM.yyyy HH:mm} | {app.Status} | {app.GetCost():F2} грн");
            }
        }

        writer.WriteLine("-- Плани лікування --");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetByPatient(patientId);
        if (plans.Length == 0)
        {
            writer.WriteLine("Немає");
        }
        else
        {
            for (int i = 0; i < plans.Length; i++)
            {
                TreatmentPlan plan = plans[i];
                writer.WriteLine($"[{plan.Id}] {plan.Diagnosis} | {plan.DurationDays} днів | {plan.Status}");
            }
        }

        decimal debt = _clinic.Billing.GetPatientDebt(patientId);
        writer.WriteLine($"Заборгованість: {debt:F2} грн");
    }
}