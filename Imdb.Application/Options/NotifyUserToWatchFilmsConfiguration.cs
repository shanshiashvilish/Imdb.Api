
namespace Imdb.Application.Options
{
    public class NotifyUserToWatchFilmsConfiguration
    {
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int DayOfMonthForFirstRun { get; set; }
        public int DayOfMonthForSecondRun { get; set; }
    }
}
