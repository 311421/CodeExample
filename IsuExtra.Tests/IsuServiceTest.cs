using System.Collections.Generic;
using Isu.Objects;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private ExIsuService _isuService;
        private OGNP _ognp;
        private Group _group;
        private Student _student;
        private Faculty _faculty1;
        private Faculty _faculty2;
        private Stream _stream1;


        [SetUp]
        public void Setup()
        {
            _isuService = new ExIsuService();

            _faculty1 = _isuService.CreateFaculty("ITIP");
            _faculty2 = _isuService.CreateFaculty("FTMI");
            _stream1 = _faculty1.CreateStream("Stream");
            _group = _isuService.AddGroup("M3208", _stream1);
            _student = _isuService.AddStudent(_group, "Fedor");
            // Creating 7-pair schedule on Monday 
            for (uint i = 0; i < 8; i++)
            {
                Pair newPair = _isuService.CreatePair("Albert Einstein",
                    _group, "666", Weekday.Mon, i);
                _isuService.AssignPair(newPair, _stream1);
            }

            _isuService.UnassignPair(_stream1, Weekday.Mon, 5);
            //
            _ognp = _isuService.CreateOgnp(_faculty2, "something");
        }

        [Test]
        public void AddingStudentOgnp()
        {
            Pair newPair = _isuService.CreatePair("Zhak Fresko", _group, "666", Weekday.Mon, 5);
            _isuService.AssignPair(newPair, _ognp);
            _isuService.AssignOgnp(_student, _ognp);
            Assert.NotNull(_isuService.FindStudentOgnp(_student.Name));
        }

        [Test]
        public void AssigningStudentToOwnFaculty()
        {
            _ognp = _isuService.CreateOgnp(_faculty1, "something");
            Pair newPair = _isuService.CreatePair("Zhak Fresko", _group, "666", Weekday.Mon, 5);
            _isuService.AssignPair(newPair, _ognp);
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AssignOgnp(_student, _ognp);
            });
        }

        [Test]
        public void UnassignOgnp()
        {
            Pair newPair = _isuService.CreatePair("Zhak Fresko", _group, "666", Weekday.Mon, 5);
            _isuService.AssignPair(newPair, _ognp);
            _isuService.AssignOgnp(_student, _ognp);
            _isuService.UnassignOgnp(_student);
            Assert.IsNull(_isuService.FindStudentOgnp(_student.Name));
        }

        [Test]
        public void GetStudentsOfOgnp()
        {
            _isuService.AssignOgnp(_student, _ognp);
            List<Student> students = _isuService.GetStudents(_ognp);
            Assert.Contains(_student, students);
        }

        [Test]
        public void GetUnassignedStudents()
        {
            Student newStudent1 = _isuService.AddStudent(_group, "name1");
            Student newStudent2 = _isuService.AddStudent(_group, "name2");
            List<Student> unassignedStudents = _isuService.GetUnassignedStudents(_group);
            foreach (Student student in _group.Students)
            {
                Assert.Contains(student, unassignedStudents);
            }
        }
    }
}