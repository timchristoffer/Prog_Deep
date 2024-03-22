using System;

public struct Time
{
    // Definiera maximala värden för timmar, minuter och sekunder.
    private const int MaxHours = 23;
    private const int MaxMinutes = 59;
    private const int MaxSeconds = 59;
    
    // Instansvariabler för timmar, minuter och sekunder.
    private int hours;
    private int minutes;
    private int seconds;

    // Konstruktor för att skapa en tidsinstans med givna värden för timmar, minuter och sekunder.
    public Time(int hours, int minutes, int seconds)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    // Metod för att kontrollera om tiden är giltig.
    public bool IsValid()
    {
        return hours >= 0 && hours <= MaxHours &&
               minutes >= 0 && minutes <= MaxMinutes &&
               seconds >= 0 && seconds <= MaxSeconds;
    }

    // Metod för att konvertera tiden till strängrepresentation med valfritt 24-timmarsformat.
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

    // Metod för att kontrollera om tiden är före middag.
    public bool IsAM()
    {
        return hours < 12;
    }

    // Metod för att kontrollera om tiden är efter middag.
    public bool IsPM()
    {
        return hours >= 12;
    }

    // Överlagring av operator för att lägga till sekunder till tiden.
    public static Time operator +(Time time, int secondsToAdd)
    {
        int totalSeconds = time.hours * 3600 + time.minutes * 60 + time.seconds + secondsToAdd;
        return new Time(totalSeconds / 3600 % 24, (totalSeconds / 60) % 60, totalSeconds % 60);
    }

    // Överlagring av operator för att subtrahera sekunder från tiden.
    public static Time operator -(Time time, int secondsToSubtract)
    {
        int totalSeconds = time.hours * 3600 + time.minutes * 60 + time.seconds - secondsToSubtract;
        if (totalSeconds < 0)
            totalSeconds += 24 * 3600; // Om tiden blir negativ, gör ett "wrap around" för att justera
        return new Time(totalSeconds / 3600 % 24, (totalSeconds / 60) % 60, totalSeconds % 60);
    }

    // Överlagring av operator för prefix ökning (++Time).
    public static Time operator ++(Time time)
    {
        return time + 1;
    }

    // Överlagring av operator för prefix minskning (--Time).
    public static Time operator --(Time time)
    {
        return time - 1;
    }
   
    // Överlagring av operator för att jämföra om en tid är större än en annan tid.
    public static bool operator >(Time time1, Time time2)
    {
        return (time1.hours * 3600 + time1.minutes * 60 + time1.seconds) >
               (time2.hours * 3600 + time2.minutes * 60 + time2.seconds);
    }

    // Överlagring av operator för att jämföra om en tid är mindre än en annan tid.
    public static bool operator <(Time time1, Time time2)
    {
        return (time1.hours * 3600 + time1.minutes * 60 + time1.seconds) <
               (time2.hours * 3600 + time2.minutes * 60 + time2.seconds);
    }

    // Överlagring av operator för att jämföra om en tid är större än eller lika med en annan tid.
    public static bool operator >=(Time time1, Time time2)
    {
        return time1 > time2 || time1 == time2;
    }

    // Överlagring av operator för att jämföra om en tid är mindre än eller lika med en annan tid.
    public static bool operator <=(Time time1, Time time2)
    {
        return time1 < time2 || time1 == time2;
    }

    // Överlagring av operator för att jämföra om två tider är lika.
    public static bool operator ==(Time time1, Time time2)
    {
        return time1.hours == time2.hours &&
               time1.minutes == time2.minutes &&
               time1.seconds == time2.seconds;
    }

    // Överlagring av operator för att jämföra om två tider är olika.
    public static bool operator !=(Time time1, Time time2)
    {
        return !(time1 == time2);
    }

    // Överlagring av GetHashCode för att hantera hash-funktionen för Time-strukturen.
    public override int GetHashCode()
    {
        return (hours * 3600 + minutes * 60 + seconds).GetHashCode();
    }
}
