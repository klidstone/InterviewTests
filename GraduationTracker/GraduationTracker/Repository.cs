using System.Linq;
using GraduationTracker.Models;

namespace GraduationTracker
{
    public class Repository
    {
        #region Fetch By Id

        public static Student GetStudent(int id)
        {
            var students = GetStudents();
            return students.FirstOrDefault(x => x.Id == id);
        }

        public static Diploma GetDiploma(int id)
        {
            var diplomas = GetDiplomas();
            return diplomas.FirstOrDefault(x => x.Id == id);
        }

        public static Requirement GetRequirement(int id)
        {
            var requirements = GetRequirements();
            return requirements.FirstOrDefault(x => x.Id == id);
        }

        public static Course GetCourse(int id, int? mark = null)
        {
            var courses = GetCourses();
            var course = courses.FirstOrDefault(x => x.Id == id);

            if (mark != null)
            {
                course.Mark = (int)mark;
            }

            return course;
        }

        #endregion

        #region Fetch All

        private static Diploma[] GetDiplomas()
        {
            return new[]
            {
                new Diploma
                {
                    Id = 1,
                    Credits = 4,
                    Requirements = new int[]{100,102,103,104}
                }
            };
        }

        public static Requirement[] GetRequirements()
        {
            return new[]
            {
                new Requirement{Id = 100, MinimumMark=50, Course = 1, Credits=1 },
                new Requirement{Id = 102, MinimumMark=50, Course = 2, Credits=1 },
                new Requirement{Id = 103, MinimumMark=50, Course = 3, Credits=1 },
                new Requirement{Id = 104, MinimumMark=50, Course = 4, Credits=1 }
            };
        }

        public static Course[] GetCourses()
        {
            return new[]
            {
                new Course{Id = 1, Name = "Math"},
                new Course{Id = 2, Name = "Science"},
                new Course{Id = 3, Name = "Literature"},
                new Course{Id = 4, Name = "Physical Education"}
            };
        }

        private static Student[] GetStudents()
        {
            return new[]
            {
                new Student
                {
                    Id = 1,
                    Courses = new Course[]
                    {
                        GetCourse(1, 95),
                        GetCourse(2, 95),
                        GetCourse(3, 95),
                        GetCourse(4, 95)
                    }
                },
                new Student
                {
                    Id = 2,
                    Courses = new Course[]
                    {
                        GetCourse(1, 80),
                        GetCourse(2, 80),
                        GetCourse(3, 80),
                        GetCourse(4, 80)
                    }
                },
                new Student
                {
                    Id = 3,
                    Courses = new Course[]
                    {
                        GetCourse(1, 50),
                        GetCourse(2, 50),
                        GetCourse(3, 50),
                        GetCourse(4, 50)
                    }
                },
                new Student
                {
                    Id = 4,
                    Courses = new Course[]
                    {
                        GetCourse(1, 40),
                        GetCourse(2, 40),
                        GetCourse(3, 40),
                        GetCourse(4, 40)
                    }
                }

            };
        }

        #endregion
    }
}
