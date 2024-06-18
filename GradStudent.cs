//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-03-2022   kriner      creation of file, initialization of auto-implemented properties, creation of ctor
// 03-03-2022   kriner      creation of ToString override and ToStringForOutputFile override
// 03-04-2022   kriner      created new ContactInfo object in GradStudent ctor to encapsulate firstName, lastName, and email
// 03-05-2022   kriner      documentation of class
//
// The GradStudent class of the StudentDB program inherits the Student class constructs an GradStudent object with parameters
// for a first name, last name, gpa, email, enroll time, credit, and advisor. The ToString and ToStringForOutputFile methods
// are also overriden from the base class Student to allow for the extra information for a grad student to be outputted
// in the same format as the base class info in both the output in the program window and output in the output file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// the namespace for the StudentDB program
namespace StudentDB
{
    // declaration of GradStudent class that inherits student and represents a grad
    // student in StudentDB
    internal class GradStudent : Student
    {
        // financial tuition credit from teaching
        public decimal TuitionCredit { get; set; }
        // graduate faculty advisor
        public string FacultyAdvisor { get; set; }

        // GradStudent ctor that inherits information from the Student ctor
        public GradStudent(string first, string last, double gpa, string email,
                    DateTime enrolled, decimal credit, string advisor)
            : base(new ContactInfo(first, last, email), gpa, enrolled)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // expression bodied method overriding the student ToString to add grad student info
        public override string ToString() => base.ToString() + $"    Credit: {TuitionCredit}\n   Advisor: {FacultyAdvisor}\n";

        // overrides the student ToStringForOutputFile method to add grad student info to the output file
        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $"{TuitionCredit}\n";
            str += $"{FacultyAdvisor}";

            return str;
        }
    }
}
