using System;

namespace ClinicApp;

public struct WorkSchedule
{
    public int StartHour { get; set; }
    public int EndHour { get; set; }

    public WorkSchedule(int startHour, int endHour)
    {
        StartHour = startHour;
        EndHour = endHour;
    }

    public readonly int TotalHours
    {
        get { return EndHour - StartHour; }
    }

    public readonly bool IsWorkingAt(int hour)
    {
        return hour >= StartHour && hour < EndHour;
    }

    public override readonly string ToString()
    {
        return $"{StartHour:D2}:00–{EndHour:D2}:00";
    }
}