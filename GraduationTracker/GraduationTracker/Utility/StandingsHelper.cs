using GraduationTracker.Enums;

namespace GraduationTracker.Utility
{
    public static class StandingsHelper
    {
        public static STANDING GetStandingFromMark(int mark)
        {
            if (mark < 50)
                return STANDING.Remedial;
            else if (mark < 80)
                return STANDING.Average;
            else if (mark < 95)
                return STANDING.MagnaCumLaude;
            
            return STANDING.SummaCumLaude;
        }

        public static bool IsPassingGrade(int mark)
        {
            return mark >= 50;
        }
    }
}
