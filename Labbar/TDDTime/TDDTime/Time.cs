using System;

public struct Time
{
    private const int MaxHours = 23;
    private const int MaxMinutes = 59;
    private const int MaxSeconds = 59;

    private int hours;
    private int minutes;
    private int seconds;

    public Time(int hours, int minutes, int seconds)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public bool IsValid()
    {
        return hours >= 0 && hours <= MaxHours &&
               minutes >= 0 && minutes <= MaxMinutes &&
               seconds >= 0 && seconds <= MaxSeconds;
    }
    public string ToString(bool is24HourFormat)
    {
        if (is24HourFormat)
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        else
        {
            string period = (hours < 12) ? "am" : "pm";
            int hours12 = (hours % 12 == 0) ? 12 : hours % 12;
            return $"{hours12:D2}:{minutes:D2}:{seconds:D2} {period}";
        }
    }

    public bool IsAM()
    {
        return hours < 12;
    }

    public bool IsPM()
    {
        return hours >= 12;
    }

    public static Time operator +(Time time, int secondsToAdd)
    {
        int totalSeconds = time.hours * 3600 + time.minutes * 60 + time.seconds + secondsToAdd;
        return new Time(totalSeconds / 3600 % 24, (totalSeconds / 60) % 60, totalSeconds % 60);
    }

    public static Time operator -(Time time, int secondsToSubtract)
    {
        int totalSeconds = time.hours * 3600 + time.minutes * 60 + time.seconds - secondsToSubtract;
        if (totalSeconds < 0)
            totalSeconds += 24 * 3600; // Wrap around for negative time
        return new Time(totalSeconds / 3600 % 24, (totalSeconds / 60) % 60, totalSeconds % 60);
    }

    // Operatoröverlagring för prefix ökning (++Time)
    public static Time operator ++(Time time)
    {
        return time + 1;
    }

    // Operatoröverlagring för prefix minskning (--Time)
    public static Time operator --(Time time)
    {
        return time - 1;
    }

    // Equals and GetHashCode overrides...

    public static bool operator >(Time time1, Time time2)
    {
        return (time1.hours * 3600 + time1.minutes * 60 + time1.seconds) >
               (time2.hours * 3600 + time2.minutes * 60 + time2.seconds);
    }

    public static bool operator <(Time time1, Time time2)
    {
        return (time1.hours * 3600 + time1.minutes * 60 + time1.seconds) <
               (time2.hours * 3600 + time2.minutes * 60 + time2.seconds);
    }

    public static bool operator >=(Time time1, Time time2)
    {
        return time1 > time2 || time1 == time2;
    }

    public static bool operator <=(Time time1, Time time2)
    {
        return time1 < time2 || time1 == time2;
    }

    public static bool operator ==(Time time1, Time time2)
    {
        return time1.hours == time2.hours &&
               time1.minutes == time2.minutes &&
               time1.seconds == time2.seconds;
    }

    public static bool operator !=(Time time1, Time time2)
    {
        return !(time1 == time2);
    }

    public override int GetHashCode()
    {
        return (hours * 3600 + minutes * 60 + seconds).GetHashCode();
    }
}
