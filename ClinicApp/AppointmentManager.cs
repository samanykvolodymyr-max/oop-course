using System;

namespace ClinicApp;

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

    public void Book(int patientId, int doctorId, DateTime dateTime)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return;
        }

        Appointment app = new Appointment(patientId, doctorId, dateTime);
        _appointments[_count++] = app;
        Console.WriteLine($"Запис [{app.Id}] створено.");
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
        if (list.Length == 0)
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