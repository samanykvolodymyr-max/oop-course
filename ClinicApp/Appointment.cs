using System;

namespace ClinicApp;

public class Appointment
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; private set; }
    public string CancellationReason { get; private set; }

    public DateTime EndsAt
    {
        get { return ScheduledAt.AddMinutes(DurationMinutes); }
    }

    public bool IsUpcoming
    {
        get { return Status == AppointmentStatus.Scheduled && ScheduledAt > DateTime.Now; }
    }

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        CancellationReason = "";
    }

    public bool Cancel(string reason = "")
    {
        if (Status == AppointmentStatus.Cancelled) return false;
        Status = AppointmentStatus.Cancelled;
        CancellationReason = reason;
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
        return "[" + Id + "] Пацієнт #" + PatientId + " → Лікар #" + DoctorId + " | " + ScheduledAt.ToString("dd.MM.yyyy HH:mm") + " | " + Status;
    }
}