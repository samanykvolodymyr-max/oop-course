using System;

namespace ClinicApp;

public class Appointment
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string CancelReason { get; set; }

    public Appointment(int patientId, int doctorId, DateTime dateTime)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
        Status = AppointmentStatus.Scheduled;
        CancelReason = "";
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
        return $"[{Id}] Запис P:{PatientId} до D:{DoctorId} на {DateTime:dd.MM.yyyy HH:mm} | Статус: {Status}";
    }
}