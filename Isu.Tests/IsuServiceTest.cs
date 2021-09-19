using Isu.Objects;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group newGroup = _isuService.AddGroup("M3208");
            Student newStudent = _isuService.AddStudent(newGroup, "Vasya Pupkin");
            if (_isuService.GetStudent(newStudent.StudentId) != newStudent)
            {
                Assert.Fail();
            }

            if (newGroup != newStudent.StudentGroup)
            {
                Assert.Fail();
            }
            
            Assert.Pass();
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group newGroup = _isuService.AddGroup("M3101");
                for (int i = 0;; i++)
                {
                    _isuService.AddStudent(newGroup, "" + i);
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("[eqkj ns t,fyjt");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group firstGroup = _isuService.AddGroup("M3208");
            Group secondGroup = _isuService.AddGroup("M3209");
            Student newStudent = _isuService.AddStudent(firstGroup, "Vasya Pupkin");
            _isuService.ChangeStudentGroup(newStudent, secondGroup);
            if (newStudent.StudentGroup == secondGroup)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}