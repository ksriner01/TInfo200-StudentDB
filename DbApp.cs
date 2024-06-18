//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-01-2022   kriner      intial creation of file, creation of Run method with switch statements
// 03-01-2022   kriner      creation of DisplayMainMenu, GetUserSelection, and SeedDatabaseList methods
// 03-02-2022   kriner      created DbApp ctor, SaveAllRecordsToOutputFile and PrintAllRecords methods
// 03-02-2022   kriner      created ReadDataFromInputFile method, documentation of methods
// 03-03-2022   kriner      altered ReadDataFromInputFile method to accomodate Undergrad and Grad students
// 03-04-2022   kriner      created FindStudentRecord and CreateStudentRecord methods, removed SeedDatabaseList method
// 03-04-2022   kriner      created DeleteStudentRecord method, created backdoor, created ModifyStudentRecord method
// 03-05-2022   kriner      documentation of program
//
// The DbApp class is used for the main operations of the StudentDB program. A list of students is used for storing 
// student objects that are either undergraduates or grad students that are read in from a text file. The user is
// presented a main menu in which they can perform certain operations within the database. The options they are given
// are [C]reate a new student, [F]ind a student, [M]odify a student, [D]elete a student, [P]rint all records, [S]ave
// records, and [E]xit and save. During runtime objects are stored and manipulated in the students list and are only
// written to the text file when the user enters a [S]ave or [E]xit command. 

using System;
using System.Collections.Generic;
using System.IO;

// the namespace for the StudentDB program
namespace StudentDB
{
    // declaration of the class that holds the DbApp code
    internal class DbApp
    {
        // constant string for text file
        public const string STUDENTDB_DATAFILE = "STUDENTDB_DATAFILE.txt";
        // STUDENT_DATAFILE_OUTPUT was used in testing, input and output now merged into STUDENTDB_DATAFILE
        // public const string STUDENTDB_DATAFILE_OUTPUT = "STUDENTDB_DATAFILE_OUTPUT.txt";

        // constant strings for undergrad and grad students
        public const string Undergrad = "StudentDB.Undergrad";
        public const string Grad = "StudentDB.GradStudent";

        // data store for when database is in operation
        private List<Student> students = new List<Student>();

        // orignally used for the SeedDatabaseList method to test the program, now
        // solely calls the ReadDataFromInputFile method at start of execution
        public DbApp()
        {
            // for testing only until release
            // SeedDatabaseList();

            ReadDataFromInputFile();
        }

        // using a StreamReader, reads student data from the file as long as the student
        // type for each student is not null. the student's data starts being collected
        // and once all relevant pieces are read from the input file, a new student 
        // (either undergrad or grad) is added to the list.
        private void ReadDataFromInputFile()
        {
            // create file for reading in from disk
            StreamReader inFile = new StreamReader(STUDENTDB_DATAFILE);

            // read student record
            string studentType = string.Empty;

            // while the line in the input file that declares a student type is not null,
            // read all the info that makes up a student from the input file. the last two
            // pieces of data are different based on whether the student type is an 
            // undergrad or grad student. the students are then added to the list from the
            // input file. if an appropriate student type was not read, an error is displayed
            while ((studentType = inFile.ReadLine()) != null)
            {
                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());
                string email = inFile.ReadLine();
                DateTime date = DateTime.Parse(inFile.ReadLine());

                if (studentType == Undergrad)
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();

                    students.Add(new Undergrad(firstName, lastName, gpa, email, date, rank, major));
                }
                else if (studentType == Grad)
                {
                    decimal pay = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();
                    students.Add(new GradStudent(firstName, lastName, gpa, email, date, pay, advisor));
                }
                else
                {
                    Console.WriteLine("ERROR reading input file.");
                    break;
                }
            }

            // close file
            inFile.Close();
        }

        // a while loop is entered which is always true and displays a main menu for the program, receives
        // a user's choice of command, and enters a switch statement to find a case that matches the
        // user's command. the loop is exited whenever the user uses the exit command which closes
        // the program
        internal void Run()
        {
            while (true)
            {
                // displays main menu
                DisplayMainMenu();

                // receive user choice and stores in char variable
                char userChoice = GetUserSelection();

                switch (userChoice)
                {
                    case 'b': // backdoor, not supposed to be obvious
                        BackDoorFunctions();
                        break;
                    case 'C': //[C]reate
                    case 'c':
                        CreateStudentRecord();
                        break;
                    case 'D': //[D]elete
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M': //[M]odify
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'F': //[F]ind
                    case 'f':
                        string email = string.Empty;
                        FindStudentRecord(out email);
                        break;
                    case 'P': //[P]rint
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'S': //[S]ave
                    case 's':
                        SaveAllRecordsToOutputFile();
                        break;
                    case 'E': //[E]xit
                    case 'e':
                        SaveAllRecordsToOutputFile();
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("ERROR: That was not a valid choice! Select again.");
                        break;
                }
            }
        }

        // backdoor which (in theory) allows an administrator to perfrom executive actions in the command
        // line such as a cmd prompt, file explorer, opening a website, and opening task manager.
        private void BackDoorFunctions()
        {
            switch (Console.ReadLine())
            {
                case "~":
                    System.Diagnostics.Process.Start("cmd.exe");
                    break;
                case "!":
                    System.Diagnostics.Process.Start(@"C:\Windows\System32");
                    break;
                case "@":
                    System.Diagnostics.Process.Start("https://www.vulnhub.com");
                    break;
                case "#":
                    System.Diagnostics.Process.Start("Taskmgr");
                    break;
                default:
                    break;
            }
        }

        // modifies an existing student's record. begins by calling the FindStudentRecord method to
        // determine if the student exists. if so, then the user is prompted to provide new information
        // for the student except the enrollement date, which is uneditable in this version of
        // the program. if the student's email is not found an error message is outputted to the user.
        private void ModifyStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            Undergrad undergrad = stu as Undergrad;
            GradStudent grad = stu as GradStudent;

            if (stu != null)
            {
                Console.WriteLine($"Modifying student with email: {email}");
                Console.WriteLine($"Please enter the new information excluding enrollement date and email for {email}");

                Console.Write("ENTER the first name: ");
                stu.Info.FirstName = Console.ReadLine();
                Console.Write("ENTER the last name: ");
                stu.Info.LastName = Console.ReadLine();
                Console.Write("ENTER the current grade point average: ");
                stu.GradePtAvg = double.Parse(Console.ReadLine());

                if (stu == undergrad)
                {
                    Console.WriteLine("[1]Freshman [2]Sophomore [3]Junior [4]Senior");
                    Console.Write("ENTER year/rank in school from above choices: ");
                    undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                    Console.Write("ENTER degree major: ");
                    undergrad.DegreeMajor = Console.ReadLine();
                    Console.WriteLine($"\nSuccessfully modified student: {email}");
                }
                else if (stu == grad)
                {
                    Console.Write("ENTER tuition reimbursement: ");
                    grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                    Console.Write("ENTER faculty advisor: ");
                    grad.FacultyAdvisor = Console.ReadLine();
                    Console.WriteLine($"\nSuccessfully modified student: {email}");
                }
                else
                {
                    Console.WriteLine($"ERROR: No student {email} modified.\n");
                }
            }
            else
            {
                Console.WriteLine($"ERROR: A student with email {email} does not exist\n");
            }
        }

        // finds and deletes student from the list. begins by calling the FindStudentRecord method
        // to see if the student exists. if so, the student is removed from the list. if the student
        // does not exist, a message is outputted to the user that the program is unable to delete
        // a student with the email they provided
        private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu != null)
            {
                students.Remove(stu);
                Console.WriteLine($"RECORD FOUND AND REMOVED.\nNo longer in database: {email}\n");

            }
            else
            {
                Console.WriteLine($"RECORD NOT FOUND.\nCan't delete record for user {email}\n");
            }
        }

        // creates new student and adds them to the list. begins by calling the FindStudentRecord method
        // to see if a student with the email already exists. if it does, an error message is outputted 
        // for the user. if it does not, then the method begins prompting the user for data inputs for the 
        // student they would like to create. Once all the general student data has been stored, the user
        // is prompted whether the student is an undergrad or a grad student. the information specific to
        // the student type is recorded and the student is created and added to the students list.
        private void CreateStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu == null)
            {
                Console.WriteLine($"Creating new student with email addess: {email}");

                Console.Write("ENTER the first name: ");
                string first = Console.ReadLine();
                Console.Write("ENTER the last name: ");
                string last = Console.ReadLine();
                Console.Write("ENTER the current grade point average: ");
                double gpa = double.Parse(Console.ReadLine());

                Console.Write("[U]ndergrad or [G]rad student?: ");
                string studentType = Console.ReadLine().ToUpper();
                if (studentType == "U")
                {
                    // YearRank
                    Console.WriteLine("[1]Freshman [2]Sophomore [3]Junior [4]Senior");
                    Console.Write("ENTER year/rank in school from above choices: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());
                    // degree major
                    Console.Write("ENTER degree major: ");
                    string major = Console.ReadLine();

                    Undergrad undergrad = new Undergrad(first, last, gpa, email, DateTime.Now,
                                                        rank, major);
                    students.Add(undergrad);

                    Console.WriteLine($"Adding student to database: \n{undergrad}");
                }
                else if (studentType == "G")
                {
                    // tuition reimbursement
                    Console.Write("ENTER tuition reimbursement: ");
                    decimal tuition = decimal.Parse(Console.ReadLine());
                    // faculty advisor
                    Console.Write("ENTER faculty advisor: ");
                    string advisor = Console.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, DateTime.Now,
                                                        tuition, advisor);
                    students.Add(grad);

                    Console.WriteLine($"Adding student to database: \n{grad}");
                }
                else
                {
                    Console.WriteLine($"ERROR: No student {email} created.\n" +
                        $"{studentType} not a valid kind of student.");
                }
            }
            else
            {
                Console.WriteLine($"{email} RECORD FOUND! Can't add student {email},\n" +
                                            $"RECORD ALREADY EXISTS.");
            }
        }

        // checks to see if an email is in the database with foreach loop. if found, returns
        // the student object and if not, outputs a not found message and returns null.
        private Student FindStudentRecord(out string email)
        {
            Console.Write("\nENTER email address (primary key) to search: ");
            email = Console.ReadLine();

            foreach (var stu in students)
            {
                if (email == stu.Info.EmailAddress)
                {
                    Console.WriteLine($"FOUND email address: {stu.Info.EmailAddress}");
                    return stu;
                }
            }
            Console.WriteLine($"{email} NOT FOUND");
            return null;
        }

        // using a StreamWriter, saves records for each student object in the students list
        private void SaveAllRecordsToOutputFile()
        {
            StreamWriter outFile = new StreamWriter(STUDENTDB_DATAFILE);
            Console.WriteLine("\nNow saving all student records to the output file...");

            foreach (var student in students)
            {
                outFile.WriteLine(student.ToStringForOutputFile());
            }
            outFile.Close();
            Console.WriteLine("Done writing all records - file has been closed");
        }

        // Prints all records for each student object in the students list
        private void PrintAllRecords()
        {
            Console.WriteLine("\n********* All Student Records *********");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("***** END of Student Record Data *****");
        }

        // receives a char input from the user based on what action they want to take in
        // the main menu. uses console key info rather than ReadLine to register the user's
        // key press without the need to press enter
        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        // displays a user interface that prompts them for an input for whatever action
        // they would like to take. the user input is then used in the GetUserSelection
        // method
        private void DisplayMainMenu()
        {
            Console.Write(@"
**************************************
******** Student DB Main Menu ********
**************************************
[F]ind a student record for review
[C]reate new student record
[M]odify an existing student record
[D]elete an existing student record
[P]rint all records
[S]ave all records without exit
[E]xit the application (saves and exits)

User Selection (type a letter in brackets above): ");
        }

        // seeds the database list with initial students for testing
        //public void SeedDatabaseList()
        //{
        //    Student stu1 = new Student("Alice", "Anderson", 3.9, "aanderson@uw.edu", DateTime.Now);
        //    students.Add(stu1);
        //    students.Add(new Student("Bob", "Bradshaw", 3.7, "bbradshaw@uw.edu", DateTime.Now));
        //    students.Add(new Student("Carlos", "Castaneda", 3.5, "ccastaneda@uw.edu", DateTime.Now));
        //    students.Add(new Student("David", "Davidson", 2.5, "ddavidson@uw.edu", DateTime.Now));
        //}
    }
}