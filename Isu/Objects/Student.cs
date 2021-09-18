using System.Data.Common;

namespace Isu.Objects
{
    public class Student
    {
        private static int _uniqueStudentId = 0;
        private string _name;
        private int _studentId;
        public Student(string name, Group group)
        {
            _name = name;
            _studentId = _uniqueStudentId + 100000;
            _uniqueStudentId = (_uniqueStudentId + 1) % 900000;
        }

        public int Id()
        {
            return _studentId;
        }

        public string Name()
        {
            return _name;
        }
    }
}