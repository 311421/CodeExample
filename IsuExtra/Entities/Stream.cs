namespace IsuExtra
{
    public class Stream
    {
        private string _name;

        public Stream(string name, Faculty faculty)
        {
            _name = name;
            Faculty = faculty;
        }

        public Faculty Faculty { get; }
        public Schedule Schedule { get; } = new Schedule();
    }
}