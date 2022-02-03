using System;
using Isu.Objects;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private string _teacher;
        private Schedule _schedule;
        private string _classroom;

        public Lesson(string teacher, Schedule schedule, string classroom, DayOfWeek weekday, uint pairNum)
        {
            _teacher = teacher ?? throw new IsuException("Invalid teacher name");
            _schedule = schedule;
            _classroom = classroom ?? throw new IsuException("Invalid classroom name");
            PairNum = pairNum;
            Weekday = weekday;
        }

        public DayOfWeek Weekday { get; }
        public uint PairNum { get; }
    }
}