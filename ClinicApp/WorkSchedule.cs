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

    public int TotalHours
    {
        get { return EndHour - StartHour; }
    }

    public bool IsWorkingAt(int hour)
    {
        return hour >= StartHour && hour < EndHour;
    }

    public override string ToString()
    {
        return $"{StartHour:D2}:00–{EndHour:D2}:00 ({TotalHours} год)";
    }


    public static WorkSchedule operator +(WorkSchedule schedule, int extraHours)
    {
        int newEnd = Math.Min(24, schedule.EndHour + extraHours);
        return new WorkSchedule(schedule.StartHour, newEnd);
    }

    public static bool operator ==(WorkSchedule left, WorkSchedule right)
    {
        return left.StartHour == right.StartHour && left.EndHour == right.EndHour;
    }

    public static bool operator !=(WorkSchedule left, WorkSchedule right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is WorkSchedule other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StartHour, EndHour);
    }
}