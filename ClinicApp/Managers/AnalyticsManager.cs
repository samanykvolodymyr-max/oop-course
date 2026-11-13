using ClinicApp.Models;
using System;
using System.Collections.Generic;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Doctor[] doctors = _doctors.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < doctors.Length; i++)
        {
            Doctor doc = doctors[i];
            int count = 0;
            decimal revenue = 0m;
            DateTime lastDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].DoctorId == doc.Id)
                {
                    count++;
                    revenue += appointments[j].GetCost();
                    if (appointments[j].DateTime > lastDate)
                    {
                        lastDate = appointments[j].DateTime;
                    }
                }
            }

            yield return new DoctorStats(doc.Id, doc.FullName, count, revenue, lastDate);
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Patient[] patients = _patients.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            Patient patient = patients[i];
            int count = 0;
            decimal spent = 0m;
            DateTime lastDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].PatientId == patient.Id)
                {
                    count++;
                    spent += appointments[j].GetCost();
                    if (appointments[j].DateTime > lastDate)
                    {
                        lastDate = appointments[j].DateTime;
                    }
                }
            }

            yield return new PatientStats(patient.Id, patient.FullName, count, spent, lastDate);
        }
    }
}