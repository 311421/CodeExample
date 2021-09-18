using System.Collections.Generic;
using System.Linq;
using Isu.Services;

namespace Isu.Objects
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups = new List<Group>();
        public Group AddGroup(string name)
        {
            _groups.Add(new Group(name));
            return _groups[^0];
        }

        public Student AddStudent(Group @group, string name)
        {
            return group.AddStudent(name);
        }

        public Student GetStudent(int id)
        {
            return _groups.SelectMany(currentGroup =>
                currentGroup.StudentList()).FirstOrDefault(currentStudent => currentStudent.Id() == id);
        }

        public Student FindStudent(string name)
        {
            return _groups.SelectMany(currentGroup =>
                currentGroup.StudentList()).FirstOrDefault(currentStudent => currentStudent.Name() == name);
        }

        public List<Student> FindStudents(Group groupName)
        {
            return groupName.StudentList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var output = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.Name()[2] == (int)courseNumber)
                {
                    output.AddRange(group.StudentList());
                }
            }

            return output;
        }
    }
}