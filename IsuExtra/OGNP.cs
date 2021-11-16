namespace IsuExtra
{
    public class OGNP
    {
        private Schedule _schedule = new Schedule();
        private string _name;

        public OGNP(Faculty faculty, string name)
        {
            Faculty = faculty;
            _name = name;
        }

        public Faculty Faculty { get; }
        public Schedule Schedule => _schedule;
    }
}