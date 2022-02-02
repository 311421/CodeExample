namespace IsuExtra
{
    public abstract class ScheduleContainer
    {
        private string _name;

        protected ScheduleContainer(string name)
        {
            _name = name;
        }

        public Schedule Schedule { get; } = new Schedule();
    }
}