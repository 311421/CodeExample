using Isu.Tools;

namespace Isu.Objects
{
    public class Student
    {
        private static int _uniqueStudentId = 0;
        private Group _studentGroup;
        public Student(string name, Group group)
        {
            if (group == null)
            {
                throw new IsuException("Incorrect group");
            }

            Name = name;
            _studentGroup = group;
            StudentId = _uniqueStudentId + 100000;
            _uniqueStudentId = (_uniqueStudentId + 1) % 900000;
        }

        public string Name { get; }
        public int StudentId { get; }

        public Group StudentGroup
        {
            get => _studentGroup;
            private set => _studentGroup = value;
        }

        public void ChangeGroup(Group newGroup)
        {
            if (newGroup == null)
            {
                throw new IsuException("Incorrect group name");
            }

            StudentGroup.Students.Remove(this);
            newGroup.Students.Add(this);
            StudentGroup = newGroup;
        }
    }
}