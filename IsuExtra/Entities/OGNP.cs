namespace IsuExtra
{
    public class OGNP
    {
        private string _name;

        public OGNP(Faculty faculty, string name)
        {
            Faculty = faculty;
            _name = name;
        }

        public Faculty Faculty { get; }
        public Schedule Schedule { get; } = new Schedule();
    }
}