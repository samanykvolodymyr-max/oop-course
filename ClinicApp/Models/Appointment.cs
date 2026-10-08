using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Appointment
{
    private static int _nextId = 1;

    private int _durationMinutes = 30;

    public int Id { get; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string CancelReason { get; set; }

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }

    public Appointment(int patientId, int doctorId, DateTime dateTime)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
        Status = AppointmentStatus.Scheduled;
        CancelReason = "";
        DurationMinutes = 30;

        Id = _nextId++;
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
        return $"[{Id}] Запис P:{PatientId} до D:{DoctorId} на {DateTime:dd.MM.yyyy HH:mm} | Тривалість: {DurationMinutes} хв | Статус: {Status}";
    }
}