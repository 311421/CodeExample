using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Objects;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class ExIsuService
    {
        private Dictionary<int, OGNP> _studentOgnps = new Dictionary<int, OGNP>();
        private Dictionary<string, Stream> _groupStreams = new Dictionary<string, Stream>();
        private List<OGNP> _ognps = new List<OGNP>();
        private List<Faculty> _faculties = new List<Faculty>();
        private List<Group> _groups = new List<Group>();

        public List<Student> GetUnassignedStudents(Group group)
        {
            return group.Students.Where(student => _studentOgnps[student.StudentId] == null).ToList();
        }

        public List<Student> GetStudents(OGNP ognp)
        {
            return _studentOgnps.Where(pair => pair.Value == ognp).Select(pair => GetStudent(pair.Key)).ToList();
        }

        public List<Stream> GetStreams(Faculty faculty)
        {
            if (faculty == null) throw new IsuException("Invalid faculty");
            return faculty.Streams;
        }

        public Stream CreateStream(Faculty faculty, string name)
        {
            if (faculty == null || name == null) throw new IsuException("Invalid parameters");
            return faculty.CreateStream(name);
        }

        public Faculty CreateFaculty(string name)
        {
            _faculties.Add(new Faculty(name));
            return _faculties[^1];
        }

        public List<Student> StudentsAssignedTo(OGNP ognp)
        {
            return (List<Student>)_studentOgnps.Keys.Where(id => _studentOgnps[id] == ognp)
                .Select(GetStudent);
        }

        public OGNP FindStudentOgnp(string name)
        {
            Student student = FindStudent(name);
            return _studentOgnps[student.StudentId];
        }

        public Lesson CreatePair(string teacher, Schedule schedule, string classroom, DayOfWeek weekday, uint pairNum)
        {
            var pair = new Lesson(teacher, schedule, classroom, weekday, pairNum);
            schedule.AssignPair(pair);
            return pair;
        }

        public void UnassignPair(OGNP ognp, DayOfWeek day, uint pairNum)
        {
            ognp.Schedule.RemovePair(day, pairNum);
        }

        public void UnassignPair(Stream stream, DayOfWeek day, uint pairNum)
        {
            stream.Schedule.RemovePair(day, pairNum);
        }

        public bool OgnpIsAssignable(Student student, OGNP ognp)
        {
            return _groupStreams[student.StudentGroup.GroupName].Schedule.IsValidScheduleMerge(ognp.Schedule) &&
                   _groupStreams[student.StudentGroup.GroupName].Faculty != ognp.Faculty;
        }

        public void UnassignOgnp(Student student)
        {
            if (student == null) throw new IsuException("Invalid student");
            _studentOgnps[student.StudentId] = null;
        }

        public void AssignOgnp(Student student, OGNP ognp)
        {
            if (student == null || ognp == null) throw new IsuException("Invalid parameters");
            if (_groupStreams[student.StudentGroup.GroupName].Faculty == ognp.Faculty)
            {
                throw new IsuException("Student Can't be assigned to own faculty's ognp");
            }

            if (OgnpIsAssignable(student, ognp))
            {
                _studentOgnps[student.StudentId] = ognp;
            }
            else
            {
                throw new IsuException("Schedules overlap");
            }
        }

        public OGNP CreateOgnp(Faculty faculty, string name)
        {
            _ognps.Add(new OGNP(faculty, name));
            return _ognps[^1];
        }

        public Group AddGroup(string name, Stream stream)
        {
            _groups.Add(new Group(name));
            Group newGroup = _groups[^1];
            stream.Faculty.Groups.Add(newGroup);
            _groupStreams.Add(name, stream);
            return newGroup;
        }

        public Student AddStudent(Group @group, string name)
        {
            if (group == null || name == null)
            {
                throw new IsuException("Incorrect parameters");
            }

            Student newStudent = group.AddStudent(name);
            _studentOgnps.Add(newStudent.StudentId, null);
            return newStudent;
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
            return _groups.Where(@group => @group.GroupName[2] == (int)courseNumber).SelectMany(group => group.Students).ToList();
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
            if (newGroup.Students.Count >= newGroup.MaxGroupSize)
            {
                throw new IsuException("Target group has reached its size limit");
            }

            if (student == null)
            {
                throw new IsuException("Incorrect parameters");
            }

            student.ChangeGroup(newGroup);
        }
    }
}