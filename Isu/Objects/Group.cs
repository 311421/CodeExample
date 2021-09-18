using System.Collections.Generic;

namespace Isu.Objects
{
    public class Group
    {
        private List<Student> _students = new List<Student>();
        private string _groupName;

        public Group(string name)
        {
            _groupName = name;
        }

        public string Name()
        {
            return _groupName;
        }

        public List<Student> StudentList()
        {
            return _students;
        }

        public Student AddStudent(string name)
        {
            _students.Add(new Student(name, this));
            return _students[^0];
        }
    }
}