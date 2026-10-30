using System;

namespace ClinicApp.Models;

public class RegularAppointment : Appointment
{
    public RegularAppointment(int patientId, int doctorId, DateTime dateTime, int durationMinutes = 30)
        : base(patientId, doctorId, dateTime, durationMinutes)
    {
    }

    public override string GetDescription()
    {
        return "Звичайний прийом";
    }
}