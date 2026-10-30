using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Models;
using System;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 200;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;
    private PatientManager? _patientManager;
    private DoctorManager? _doctorManager;

    public int Count => _count;

    public AppointmentManager()
    {
    }

    public AppointmentManager(PatientManager patientManager, DoctorManager doctorManager)
    {
        _patientManager = patientManager;
        _doctorManager = doctorManager;
    }

    public Appointment this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null!;
            return _appointments[index];
        }
    }

    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        if (items == null) return 0;
        int count = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].Cancel(reason))
            {
                count++;
            }
        }
        return count;
    }

    private bool AddAppointment(Appointment app)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return false;
        }
        _appointments[_count++] = app;
        Console.WriteLine($"Запис [{app.Id}] створено.");
        return true;
    }

    public bool Book(int patientId, int doctorId, DateTime dateTime, int durationMinutes = 30)
    {
        Appointment app = new RegularAppointment(patientId, doctorId, dateTime, durationMinutes);
        return AddAppointment(app);
    }

    public bool BookUrgent(int patientId, int doctorId, DateTime dateTime, string urgencyNote = "", int durationMinutes = 30)
    {
        Appointment app = new UrgentAppointment(patientId, doctorId, dateTime, urgencyNote, durationMinutes);
        return AddAppointment(app);
    }

    public bool BookSpecialist(int patientId, int doctorId, DateTime dateTime, string topic = "", int durationMinutes = 45)
    {
        Appointment app = new SpecialistAppointment(patientId, doctorId, dateTime, topic, durationMinutes);
        return AddAppointment(app);
    }

    public Appointment[] GetAll()
    {
        Appointment[] result = new Appointment[_count];
        for (int i = 0; i < _count; i++)
        {
            result[i] = _appointments[i];
        }
        return result;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId) matchCount++;
        }

        Appointment[] result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    public Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id) return _appointments[i];
        }
        return null;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DateTime.Date == date.Date) matchCount++;
        }

        Appointment[] result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DateTime.Date == date.Date)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day));
    }

    public Appointment[] GetUpcoming()
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DateTime >= DateTime.Now && _appointments[i].Status == AppointmentStatus.Scheduled)
            {
                matchCount++;
            }
        }

        Appointment[] result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DateTime >= DateTime.Now && _appointments[i].Status == AppointmentStatus.Scheduled)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    public bool Cancel(int id, string reason)
    {
        Appointment? app = FindById(id);
        if (app == null) return false;
        return app.Cancel(reason);
    }

    public bool Complete(int id)
    {
        Appointment? app = FindById(id);
        if (app == null) return false;
        return app.Complete();
    }

    public void DisplayList(Appointment[] list)
    {
        if (list == null || list.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            Console.WriteLine(list[i].ToString());
        }
    }
}