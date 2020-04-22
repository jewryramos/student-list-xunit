using System;
using System.Linq;


namespace StudentList.Services
{
    public class StudentManager
    {
        private const char StudentEntryDelimiter = ',';
        private StudentStorage _storage;
        private Random _rand;
        private string _studentList;

        public StudentManager(StudentStorage storage)
        {
            _storage = storage;
            _rand = new Random();
            _studentList = _storage.LoadStudentList();
        }

        public string[] Students
        {
            get
            {
                return _studentList.Split(StudentEntryDelimiter);
            }
        }

        public string PickRandomStudent()
        {
            var randomIndex = _rand.Next(0, Students.Length);
            return Students[randomIndex];
        }

        public bool StudentExists(string student)
        {
            //Using the 'Any' Liqn method to return wether or not
            //any items inside the Students object matches the given predicate
            if(this.Students.Any(s => s.Trim() == student))
            {
                return true;
            }
            return false;
        }
        public void AddStudent(string newStudent)
        {
            _studentList += "," + newStudent;
            _storage.UpdateStudentList(_studentList);
        }
    }
}
