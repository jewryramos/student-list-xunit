using System;
using System.IO;
using System.Linq;

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

            var studentList = LoadStudentList(Constants.StudentList);

            if (args[0] == Constants.ShowAll) 
            {
                var students = studentList.Split(',');
                foreach(var student in students) 
                {
                    Console.WriteLine(student);
                }
            }
            else if (args[0]== Constants.ShowRandom)
            {

                // We are loading data
                var students = studentList.Split(Constants.StudentEntryDelimiter);
                var rand = new Random();
                var randomIndex = rand.Next(0,students.Length);
                Console.WriteLine(students[randomIndex]);
            }
            else if (args[0].StartsWith(Constants.AddEntry))
            {
                // read
                var newEntry = args[0].Substring(1);

                // Write
                // But we're in trouble if there are ever duplicates entered
                UpdateStudentList(studentList + Constants.StudentEntryDelimiter + newEntry, "students.txt");
            }
            else if (args[0].StartsWith(Constants.FindEntry))
            {
                var students = studentList.Split(Constants.StudentEntryDelimiter);
                var searchTerm = args[0].Substring(1);
                
                //Using the 'Any' LINQ method to return wether or not
                //any item matches the given predicate
                if(students.Any(s => s.Trim() == searchTerm))
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
                var students = studentList.Split(Constants.StudentEntryDelimiter);
                Console.WriteLine(String.Format(" {0} words found", students.Length));
            }
            else
            {
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
