using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Objects
{
    public class Group
    {
        public Group(string name)
        {
            if (name == null)
            {
                throw new IsuException("Incorrect group name");
            }

            if (name.Length != 5)
            {
                throw new IsuException("Incorrect group name");
            }

            if ((name[0] <= 'A') || (name[0] >= 'z'))
            {
                throw new IsuException("Incorrect group name");
            }

            if (name[1] < '1' || name[1] > '4')
            {
                throw new IsuException("Incorrect group name");
            }

            for (int i = 2; i < 5; i++)
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
        public int MaxGroupSize { get; set; } = 25;

        public Student AddStudent(string name)
        {
            if (Students.Count >= MaxGroupSize)
            {
                throw new IsuException("Maximum group size reached");
            }

            Students.Add(new Student(name, this));
            return Students[^1];
        }

        public void RemoveStudent(string name)
        {
            Students.Remove(Students.FirstOrDefault(student => student.Name == name));
        }
    }
}