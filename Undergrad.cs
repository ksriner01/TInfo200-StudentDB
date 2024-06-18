//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-03-2022   kriner      creation of file, enumeration of YearRank values, initialized auto-implemented properties
// 03-03-2022   kriner      created Undergrad ctor, created ToString override, created ToStringForOutputFile override
// 03-04-2022   kriner      created new ContactInfo object in Undergrad ctor to encapsulate firstName, lastName, and email
// 03-05-2022   kriner      documentation of class
//
// The Undergrad class of the StudentDB program inherits the Student class constructs an Undergrad object with parameters
// for a first name, last name, gpa, email, enroll time, year rank, and major. The ToString and ToStringForOutputFile methods
// are also overriden from the base class Student to allow for the extra information for an undergrad student to be outputted
// in the same format as the base class info in both the output in the program window and output in the output file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// the namespace for the StudentDB program
namespace StudentDB
{
    // enum that assigns int values to each available year rank in school
    public enum YearRank
    {
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }

    // declaration of Undergrad class that inherits student and represents an undergrad
    // student in StudentDB
    internal class Undergrad : Student
    {
        // year in school (Freshman, Sophomore, Junior, Senior)
        public YearRank Rank { get; set; }

        // the undergraduate major
        public string DegreeMajor { get; set; }

        // Undergrad ctor that inherits the base class information and adds year and major parameters
        public Undergrad(string first, string last, double gpa, string email,
                            DateTime enrolled, YearRank year, string major)
            : base(new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = year;
            DegreeMajor = major;
        }

        // expression bodied method overriding the student ToString to add undergrad info
        public override string ToString() => base.ToString() + $"      Year: {Rank}\n     Major: {DegreeMajor}\n";

        // formats the string for the output file by creating a buffer, adding the information to the buffer, and returning the string
        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $"{Rank}\n";
            str += $"{DegreeMajor}";

            return str;
        }
    }
}
