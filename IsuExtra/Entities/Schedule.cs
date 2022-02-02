using System;
using Isu.Tools;

namespace IsuExtra
{
    public class Schedule
    {
        private Pair[,] _schedule = new Pair[6, 8];

        public Schedule() { }

        public void AssignPair(Pair pair)
        {
            if (pair == null) throw new IsuException("Pair is null");
            if (_schedule[(int)pair.Weekday, pair.PairNum] != null) throw new IsuException("Overlapping pairs");
            _schedule[(int)pair.Weekday, pair.PairNum] = pair;
        }

        public void RemovePair(DayOfWeek day, uint pairNum)
        {
            if (_schedule[(int)day, pairNum] == null) throw new IsuException("No pair at this time");
            _schedule[(int)day, pairNum] = null;
        }

        public bool IsValidScheduleMerge(Schedule schedule)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this._schedule[i, j] != null && schedule._schedule[i, j] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}