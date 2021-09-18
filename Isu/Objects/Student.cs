namespace Isu.Objects
{
    public class Student
    {
        private string _name;
        private Group _studentGroup;

        public Student(string name, Group group)
        {
            _name = name;
            _studentGroup = group;
        }
    }
}