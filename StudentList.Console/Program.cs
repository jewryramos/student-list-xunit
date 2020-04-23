using System;
using System.IO;
using System.Linq;
using StudentList.Services;

namespace studentlist1
{
    class Program
    {
        // The Main method 
        static void Main(string[] args)
        {
            /* Check arguments */
            if (args == null || args.Length != 1)
            {
                ShowUsage();
                return; //Exit Early.
            }

            var studentManager = new StudentManager();

            if (args[0] == Constants.ShowAll) 
            {
                foreach(var student in studentManager.Students) 
                {
                    Console.WriteLine(student);
                }
            }
            else if (args[0]== Constants.ShowRandom)
            {
                Console.WriteLine(studentManager.PickRandomStudent());
            }
            else if (args[0].StartsWith(Constants.AddEntry))
            {
                // read
                var newEntry = args[0].Substring(1);

                studentManager.AddStudent(newEntry);
            }
            else if (args[0].StartsWith(Constants.FindEntry))
            {
                var searchTerm = args[0].Substring(1);

                if(studentManager.StudentExists(searchTerm))
                {
                    Console.WriteLine($"Entry '{searchTerm}' found.");
                }
                else
                {
                    Console.WriteLine($"Entry '{searchTerm}' does not exist.");
                }
            }
            else if (args[0].Contains(Constants.ShowCount))
            {
                Console.WriteLine(String.Format(" {0} words found", studentManager.Students.Length));
            }
            else
            {
                //Arguments were supplied, but they were invalid. We'll handle this case
                //gracefully by listing valid arguments to the users.
                ShowUsage();
            }
            
        }

        //Read data from the given file
        static string LoadStudentList(string fileName)
        {

            //The 'using' construct does the heavy lifting of flushing a stream
            //amd releasing system resources the stream was using.
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                //The format of our student list is that it is two lines
                //The first line is a coma-separated list of students
                //The second line is a timestamp.
                //Let us just retrieve the first line, which is a student name
                return reader.ReadLine();
            }   
        }
        //Writes the given string of data to the file with the given filename.
        //This method also adds a timestamp to the end of the file.
        static void UpdateStudentList(string content, string fileName)
        {
            var timestamp = String.Format("List last updated on {0}",DateTime.Now);

            //The 'using' construct does the heavy lifting of flushing a stream
            //amd releasing system resources the stream was using.
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(content);
                writer.WriteLine(timestamp);
            }
        }
        static void ShowUsage()
        {
             Console.WriteLine("Usage: dotnet dev275x.rollcall.dll (a | r | c | +WORD | WORD?)");
        }
    }
}
