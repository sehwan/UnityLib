using System;
using System.Collections.Generic;
using UnityEngine;


/// 시간 관련 확장
public static class TimeEx
{
    /// 시간 -> 분 : 초 표시
    public static string ToFormattedString(this TimeSpan me)
    {
        //day
        if (me.Days > 0)
            return $"{me.Days}d {me.Hours}h";
        //hour
        else if (me.Hours > 0)
            return $"{me.Hours}h {me.Minutes}m";
        //min
        else if (me.Minutes > 0)
            return $"{me.Minutes}m {me.Seconds}s";
        //sec.
        else if (me.Seconds > 0)
            return $"{me.Seconds}s";
        //finished
        else return null;
    }
    public static string ToFormattedShortString(this TimeSpan me)
    {
        //day
        if (me.Days > 0)
            return $"{me.Days}d";
        //hour
        else if (me.Hours > 0)
            return $"{me.Hours}h";
        //min
        else if (me.Minutes > 0)
            return $"{me.Minutes}m";
        //sec.
        else if (me.Seconds > 0)
            return $"{me.Seconds}s";
        //finished
        else return null;
    }
    public static string ToTimeString(this int input)
    {
        int days = input / 86400;
        input %= 86400;
        int hours = input / 3600;
        input %= 3600;
        int minutes = input / 60;
        int seconds = input % 60;
        if (days > 0) return $"{days}d {hours}h {minutes}m {seconds}s";
        if (hours > 0) return $"{hours}h {minutes}m {seconds}s";
        if (minutes > 0) return $"{minutes}m {seconds}s";
        else return $"{seconds}s";
    }


    /// 분 -> TimeSpan 시;분;초
    public static string Minutes_to_Text(this int input)
    {
        return (new TimeSpan(0, input, 0)).ToFormattedString();
    }
    public static string Seconds_to_Text(this int input)
    {
        return (new TimeSpan(0, 0, input)).ToFormattedString();
    }

    /// 시각 -> 남은 시간 시;분;초
    public static TimeSpan ToRemain(this DateTime dt)
    {
        return dt - DateTime.UtcNow;
    }

    /// 스트링 -> 남은 시간.
    public static TimeSpan ToTimeSpanFromNow(this string me)
    {
        return Convert.ToDateTime(me) - DateTime.UtcNow;
    }



    /// 자바스크립트 숫자 -> 시각.
    public static DateTime ToDateTime(this string me)
    {
        DateTime dt = new(1970, 1, 1, 0, 0, 0, 0);
        dt = dt.ToLocalTime();
        return dt.AddSeconds(double.Parse(me) / 1000);
    }
    public static DateTime ToDateTime(this long me)
    {
        DateTime dt = new(1970, 1, 1, 0, 0, 0, 0);
        dt = dt.ToLocalTime();
        return dt.AddMilliseconds(me);
    }
    public static DateTime ToDateTime(this ulong me)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(me);
    }

    // DateTime -> Javascript Dos Date Type
    public static ulong ToLong(this DateTime dt)
    {
        return (ulong)dt 
            .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .TotalMilliseconds;
    }

    #region Chance
    public static TimeSpan GetDiff(this DateTime me)
    {
        return DateTime.UtcNow - me;
    }
    public static int GetChance(this TimeSpan me, int cool, int max)
    {
        double chance = me.TotalSeconds / cool;
        if (chance > max) chance = max;
        else if (chance < 0) chance = 0;
        return (int)chance;
    }
    public static int GetChance(this DateTime me, int cool, int max)
    {
        return me.GetDiff().GetChance(cool, max);
    }

    public static DateTime GetTimeUsedChance(this DateTime me, int cool, int max)
    {
        int remainChance = me.GetDiff().GetChance(cool, max);
        if (remainChance == max)
            return DateTime.UtcNow.AddSeconds(-(remainChance - 1) * cool);
        else return me.AddSeconds(cool);
    }
    public static DateTime GetTimeRefillChance(this DateTime me, int cool, int times)
    {
        return me.AddSeconds(-cool * times);
    }
    public static string GetChanceOrRemain(this DateTime me, int cool, int max)
    {
        var chance = me.GetChance(cool, max);
        if (chance > 0) return $"{chance} / {max}";
        else return (me.AddSeconds(cool + 1) - DateTime.UtcNow).ToFormattedString();
    }
    public static string RemainTimeText(this DateTime me, int ex, int cool, int max)
    {
        var chance = me.GetChance(cool, max) + ex;
        if (chance >= max) return "";
        int sec = (int)((DateTime.UtcNow - me).TotalSeconds % cool);
        return (cool - sec).Seconds_to_Text();
    }
    #endregion


    // Date Checking
    public static bool IsDateChangedFromNow(this DateTime last)
    {
        var now = DateTime.UtcNow;
        return (last.Date < now.Date);
    }
    public static bool IsWeekChangedFromNow(this DateTime last)
    {
        var now = DateTime.UtcNow;
        if (now.Date <= last.Date) return false;
        return (now.DayOfWeek <= last.DayOfWeek ||
                last.Date.AddDays(7) < now.Date);
    }
    public static bool IsMonthChangedFromNow(this DateTime last)
    {
        var now = DateTime.UtcNow;
        if (now.Date <= last.Date) return false;
        return (last.Month != now.Month ||
                last.Year != now.Year);
    }


    public static DateTime GetNextWeekday(this DateTime start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }
    public static DateTimeOffset GetNextWeekday(this DateTimeOffset start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }
    public static DateTime ChangeTime(this DateTime original, int h, int m = 0, int s = 0, int ms = 0)
    {
        return new DateTime(
            original.Year,
            original.Month,
            original.Day,
            h,
            m,
            s,
            ms,
            original.Kind);
    }
    public static DateTimeOffset ChangeTime(this DateTimeOffset original, int h, int m = 0, int s = 0, int ms = 0)
    {
        return new DateTimeOffset(
            original.Year,
            original.Month,
            original.Day,
            h,
            m,
            s,
            ms,
            original.Offset);
    }


    public static Dictionary<string, DateTime> passed = new();
    public static bool IsOKCooldown(string key, float sec)
    {
        if (passed.ContainsKey(key))
        {
            if (passed[key].AddSeconds(sec) < DateTime.UtcNow)
            {
                passed[key] = DateTime.UtcNow;
                return true;
            }
            else return false;
        }
        else
        {
            passed.Add(key, DateTime.UtcNow);
            return true;
        }
    }
}
