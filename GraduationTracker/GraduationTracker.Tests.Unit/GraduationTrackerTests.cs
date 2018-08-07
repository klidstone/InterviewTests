using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraduationTracker.Models;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private static readonly GraduationTracker _tracker = new GraduationTracker();
        private static readonly Diploma _diploma = new Diploma
        {
            Id = 1,
            Credits = 4,
            Requirements = new int[] { 100, 102, 103, 104 }
        };

        [TestMethod]
        public void GraduateIfEnoughCreditsAndHighEnoughAverage()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physical Education", Mark=50 }
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsTrue(hasGraduatedWithStanding.Item1);
        }

        [TestMethod]
        public void DoNotGraduateIfMissingRequiredCourse()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 },
                    new Course{Id = 2, Name = "Science", Mark=100 },
                    new Course{Id = 3, Name = "Literature", Mark=100 }
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsFalse(hasGraduatedWithStanding.Item1);
        }

        [TestMethod]
        public void DoNotGraduateIfAverageMarkIsTooLow()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 },
                    new Course{Id = 4, Name = "Physical Education", Mark=40 }
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsFalse(hasGraduatedWithStanding.Item1);
        }

        [TestMethod]
        public void CannotUseIrrelevantCoursesToGraduate()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 },
                    new Course{Id = 2, Name = "Science", Mark=100 },
                    new Course{Id = 3, Name = "Literature", Mark=100 },
                    new Course{Id = 5, Name = "Basket Weaving", Mark=100}
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsFalse(hasGraduatedWithStanding.Item1);
        }

        [TestMethod]
        public void DoNotGetCreditForCoursesWithPoorMarks()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=100 },
                    new Course{Id = 2, Name = "Science", Mark=100 },
                    new Course{Id = 3, Name = "Literature", Mark=100 },
                    new Course{Id = 4, Name = "Physical Education", Mark=40}
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsFalse(hasGraduatedWithStanding.Item1);
        }

        [TestMethod]
        public void StandingCutoffsWorkAsExpected()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Mark=100 }
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.AreEqual(hasGraduatedWithStanding.Item2, Enums.STANDING.SummaCumLaude);

            student.Courses = new Course[] { new Course{Id = 1, Mark=90 } };
            hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.AreEqual(hasGraduatedWithStanding.Item2, Enums.STANDING.MagnaCumLaude);

            student.Courses = new Course[] { new Course { Id = 1, Mark = 75 } };
            hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.AreEqual(hasGraduatedWithStanding.Item2, Enums.STANDING.Average);

            student.Courses = new Course[] { new Course { Id = 1, Mark = 40 } };
            hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.AreEqual(hasGraduatedWithStanding.Item2, Enums.STANDING.Remedial);

        }

        [TestMethod]
        public void CannotUseRepeatedRequiredCoursesToGraduate()
        {
            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 1, Name = "Math", Mark=50 }
                }
            };

            var hasGraduatedWithStanding = _tracker.HasGraduated(_diploma, student);

            Assert.IsFalse(hasGraduatedWithStanding.Item1);
        }
    }
}
