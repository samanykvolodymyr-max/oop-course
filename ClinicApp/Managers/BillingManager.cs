using ClinicApp.Interfaces;
using ClinicApp.Models;
using System;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointmentManager;

    public BillingManager(AppointmentManager appointmentManager)
    {
        _appointmentManager = appointmentManager;
    }

    private IPayable[] FilterUnpaid(Appointment[] appointments)
    {
        int count = 0;
        for (int i = 0; i < appointments.Length; i++)
        {
            if (appointments[i] != null && !appointments[i].IsPaid && !appointments[i].IsCancelled)
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int idx = 0;
        for (int i = 0; i < appointments.Length; i++)
        {
            if (appointments[i] != null && !appointments[i].IsPaid && !appointments[i].IsCancelled)
            {
                result[idx++] = appointments[i];
            }
        }
        return result;
    }

    public IPayable[] GetAllUnpaid()
    {
        return FilterUnpaid(_appointmentManager.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return FilterUnpaid(_appointmentManager.GetByPatient(patientId));
    }

    public decimal GetTotalDebt()
    {
        IPayable[] unpaid = GetAllUnpaid();
        decimal total = 0m;
        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }
        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        IPayable[] unpaid = GetUnpaidByPatient(patientId);
        decimal total = 0m;
        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }
        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment? app = _appointmentManager.FindById(appointmentId);
        if (app == null || app.IsPaid || app.IsCancelled)
        {
            return false;
        }
        app.MarkPaid();
        return true;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items == null || items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] is Appointment app)
            {
                Console.WriteLine($"{app} | Сума: {items[i].GetCost():F2} грн");
            }
            else
            {
                Console.WriteLine($"[{i + 1}] Запис | Сума: {items[i].GetCost():F2} грн");
            }
        }
    }
}