using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryServices
{
    public class DataHelpers
    {
        public static List<string> TransformHours(IEnumerable<BranchHours> branchHours)
        {
            var hours = new List<string>();

            foreach(var time in branchHours)
            {
                var day = TransformDay(time.DayOfWeek);
                var openTime = TransformTime(time.OpenTime);
                var closeTime = TransformTime(time.CloseTime);

                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        public static string TransformDay(int day)
        {
            return Enum.GetName(typeof(DayOfWeek), day - 1);
        }

        public static string TransformTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }
    }
}
