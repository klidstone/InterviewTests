using System;
using System.Linq;
using GraduationTracker.Enums;
using GraduationTracker.Models;
using GraduationTracker.Utility;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {   
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;
            var relevantCourses = 0;

            var requirements = diploma.Requirements.Select(x => Repository.GetRequirement(x));

            foreach (var requirement in requirements)
            {
                var studentCourse = student.Courses.FirstOrDefault(x => x.Id == requirement.Course);
                if (studentCourse != null)
                {
                    relevantCourses += 1;
                    average += studentCourse.Mark;
                    if (studentCourse.Mark >= requirement.MinimumMark)
                    {
                        credits += requirement.Credits;
                    }
                }
            }

            average = average / relevantCourses;

            var standing = StandingsHelper.GetStandingFromMark(average);
            
            if (StandingsHelper.IsPassingGrade(average) && credits >= diploma.Credits)
            {
                return new Tuple<bool, STANDING>(true, standing);
            }

            return new Tuple<bool, STANDING>(false, standing);
        }
    }
}
