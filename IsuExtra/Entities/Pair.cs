using System;
using Isu.Objects;
using Isu.Tools;

namespace IsuExtra
{
    public class Pair
    {
        private string _teacher;
        private Group _group;
        private string _classroom;

        public Pair(string teacher, Group @group, string classroom, Weekday weekday, uint pairNum)
        {
            _teacher = teacher ?? throw new IsuException("Invalid teacher name");
            _group = @group;
            _classroom = classroom ?? throw new IsuException("Invalid classroom name");
            PairNum = pairNum;
            Weekday = weekday;
        }

        public Weekday Weekday { get; }
        public uint PairNum { get; }

        public static Pair CreateInstance(string teacher, Group @group, string classroom, Weekday weekday, uint pairNum)
        {
            return new Pair(teacher, @group, classroom, weekday, pairNum);
        }
    }
}