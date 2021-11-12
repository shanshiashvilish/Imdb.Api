using Imdb.Application.Options;
using System;

namespace Imdb.Api.BackgroundJobs.Helpers
{
    public class NotifyUsersToWatchFilmsScheduler
    {
        public static bool IsTimeToStatJob(DateTime dateOfLastExecution, NotifyUserToWatchFilmsConfiguration configuration)
        {
            bool result = false;

            var currentDate = DateTime.Now;

            if((currentDate.Day == configuration.DayOfMonthForFirstRun || currentDate.Day == configuration.DayOfMonthForSecondRun) 
                && (currentDate.Day != dateOfLastExecution.Day || dateOfLastExecution == DateTime.MinValue))
            {
                var timeToStart = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, configuration.StartHour, configuration.StartMinute, 0);
                var minutes = (timeToStart - currentDate).TotalMinutes;

                if (minutes <= 0)
                    result = true;
            }

            return result;
        }
    }
}
