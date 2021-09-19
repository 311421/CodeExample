using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu.Objects
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups = new List<Group>();
        public Group AddGroup(string name)
        {
            _groups.Add(new Group(name));
            return _groups[^1];
        }

        public Student AddStudent(Group @group, string name)
        {
            return group.AddStudent(name);
        }

        public Student GetStudent(int id)
        {
            return _groups.SelectMany(currentGroup =>
                currentGroup.Students).FirstOrDefault(currentStudent => currentStudent.StudentId == id);
        }

        public Student FindStudent(string name)
        {
            return _groups.SelectMany(currentGroup =>
                currentGroup.Students).FirstOrDefault(currentStudent => currentStudent.Name == name);
        }

        public List<Student> FindStudents(Group groupName)
        {
            return groupName.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var output = new List<Student>();
            foreach (Group @group in _groups.Where(@group => @group.GroupName[2] == (int)courseNumber))
            {
                output.AddRange(@group.Students);
            }

            return output;
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(@group => @group.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => @group.GroupName[2] == (int)courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.Students.Count >= 25)
            {
                throw new IsuException("Target group has reached its size limit");
            }

            student.ChangeGroup(newGroup);
        }
    }
}