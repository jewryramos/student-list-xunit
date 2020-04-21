using System;

namespace StudentList.Services
{
    public class StudentManager
    {
        private StudentStorage _storage;

        public StudentManager()
        {
            _storage = new StudentStorage();
        }
        public StudentManager(StudentStorage storage)
        {
            _storage = storage;
        }
        public string[] GetAllStudents()
        {
            var studentList = _storage.LoadStudentList();
            return studentList.Split(',');
            throw new NotImplementedException("Write Tests!");
        }
    }
}
