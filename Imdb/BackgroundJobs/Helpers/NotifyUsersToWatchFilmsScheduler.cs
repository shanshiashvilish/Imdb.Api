using System;

namespace Imdb.Api.BackgroundJobs.Helpers
{
    public class NotifyUsersToWatchFilmsScheduler
    {
        public static bool IsTimeToStatJob(DateTime dateOfLastExecution)
        {
            bool result = false;

            var currentDate = DateTime.Now;
            var timeToStart = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 19, 30, 0);

            if (currentDate.Day == 1 || currentDate.Day == 15
                && currentDate.Day != dateOfLastExecution.Day
               || dateOfLastExecution == DateTime.MinValue)
            {
                var some = (timeToStart - currentDate).TotalMinutes;

                if (some <= 0)
                    result = true;
            }
            return result;
        }
    }
}
