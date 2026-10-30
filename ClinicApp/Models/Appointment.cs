using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
{
    private static int _nextId = 1;

    private int _durationMinutes = 30;

    public int Id { get; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string CancelReason { get; set; }

    public bool IsPaid { get; private set; }

    public decimal DiscountMultiplier { get; set; } = 1.0m;

    public bool IsCancelled => Status == AppointmentStatus.Cancelled;
    public string CancellationReason => IsCancelled ? CancelReason : "";

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }

    public Appointment(int patientId, int doctorId, DateTime dateTime, int durationMinutes = 30)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
        Status = AppointmentStatus.Scheduled;
        CancelReason = "";
        DurationMinutes = durationMinutes;
        IsPaid = false;

        Id = _nextId++;
    }

    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m * DiscountMultiplier;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public void MarkPaid()
    {
        if (IsCancelled) return;
        IsPaid = true;
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled) return false;
        Status = AppointmentStatus.Cancelled;
        CancelReason = reason;
        return true;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled) return false;
        Status = AppointmentStatus.Completed;
        return true;
    }

    public override string ToString()
    {
        return $"[{Id}] {GetDescription()} | P:{PatientId} до D:{DoctorId} на {DateTime:dd.MM.yyyy HH:mm} | Тривалість: {DurationMinutes} хв | Статус: {Status} | {GetCost():F2} грн";
    }
}