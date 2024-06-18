// Kyle Riner
// L6oop assignment
// TINFO-200A, Winter 2022

//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-01-2022   kriner      intial creation of file, created TestDriverMain1 for preliminary testing, created DbApp object in main
// 03-03-2022   kriner      creation of Undergrad object in TestDriverMain1
// 03-04-2022   kriner      removed TestDriverMain1 test driver from the program
// 03-05-2022   kriner      added user interface, documentation of program and final testing prior to canvas submission
//
// The StudentDB program creates a DbApp object database and runs it. A list of students is read from an 
// input file and a main menu with a command list is displayed for the user. The database application has the
// following basic operations: create a new record, delete a past record, modify an existing record, find
// an existing record, print all records, save all records to an output, and save and exit program. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// the namespace for the StudentDB program
namespace StudentDB
{
    // declaration of the class that holds the StudentDB program
    internal class Program
    {
        // the main method that executes the code for the StudentDB program
        static void Main(string[] args)
        {
            // user interface explaining the program that only outputs upon program start-up
            Console.WriteLine(@"
Welcome to the StudentDB program! In this program you will
be given the ability to view and edit the contents of a
database of students. Every student will have a first name,
last name, gpa, email, enrollement date/time. Undergrads
will also have their year rank (freshman, sophomore, junior,
senior) and their major listed. Grad students will have their tuition
credit and faculty advisor listed. The students email will be used
as their primary identification key when searching the database.
All aspects of a student's information will be modifiable except 
their enrollement data and email. The contents of the student
database will be saved to a text file upon exiting the program.

Thank you for using the StudentDB program!");
            // creation of new DbApp object and call of Run() method from DbApp class
            DbApp database = new DbApp();
            database.Run();
        }
    }
}
