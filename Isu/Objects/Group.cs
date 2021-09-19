using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Objects
{
    public class Group
    {
        public Group(string name)
        {
            if (name.Length != 5)
            {
                throw new IsuException("Incorrect group name");
            }

            if ((name[0] <= 'A') || (name[0] >= 'z'))
            {
                throw new IsuException("Incorrect group name");
            }

            for (int i = 1; i < 5; i++)
            {
                if (name[i] < '0' || name[i] > '9')
                {
                    throw new IsuException("Incorrect group name");
                }
            }

            GroupName = name;
        }

        public List<Student> Students { get; } = new List<Student>();
        public string GroupName { get; }
        public Student AddStudent(string name)
        {
            if (Students.Count >= 25)
            {
                throw new IsuException("Maximum group size reached");
            }

            Students.Add(new Student(name, this));
            return Students[^1];
        }
    }
}