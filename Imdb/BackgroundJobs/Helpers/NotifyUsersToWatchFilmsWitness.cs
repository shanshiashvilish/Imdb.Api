using System;

namespace Imdb.Api.BackgroundJobs.Helpers
{
    public class NotifyUsersToWatchFilmsWitness
    {
        public DateTime StartDate { get; set; }
        public DateTime DateOfLastExecution { get; set; }
    }
}
