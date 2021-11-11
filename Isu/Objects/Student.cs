﻿using Isu.Tools;

namespace Isu.Objects
{
    public class Student
    {
        private static int _uniqueStudentId = 0;
        public Student(string name, Group group)
        {
            if (group == null)
            {
                throw new IsuException("Incorrect group");
            }

            Name = name;
            StudentGroup = group;
            StudentId = _uniqueStudentId + 100000;
            _uniqueStudentId = (_uniqueStudentId + 1) % 900000;
        }

        public string Name { get; }
        public int StudentId { get; }
        public Group StudentGroup { get; private set; }
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