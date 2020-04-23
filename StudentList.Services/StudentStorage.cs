using System;
using System.IO;


namespace StudentList.Services
{
    public class StudentStorage
    {
        //Name of file containing list of students
        private const string StudentList = "students.txt";

        ///<summary>
        ///Replaces the content of the student storage with the given string of content
        ///The method will also update the timestamp in the student storage
        ///</summary>

        public virtual string LoadStudentList()
        {
            //The 'using' construct does the heavy lifting of flushing a stream
            //amd releasing system resources the stream was using.
            using (var fileStream = new FileStream(StudentList,FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                //The format of our student list is that it is two lines
                //The first line is a coma-separated list of students
                //The second line is a timestamp.
                //Let us just retrieve the first line, which is a student name
                return reader.ReadLine();
            }
        }
        public virtual void UpdateStudentList(string content)
        {
            var timestamp = String.Format("List last updated on {0}",DateTime.Now);

            //The 'using' construct does the heavy lifting of flushing a stream
            //amd releasing system resources the stream was using.
            using (var fileStream = new FileStream(StudentList,FileMode.Open))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(content);
                writer.WriteLine(timestamp);
            }
        }
    }
}
