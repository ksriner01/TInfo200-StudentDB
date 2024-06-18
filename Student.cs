//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-01-2022   kriner      intial creation of file
// 03-02-2022   kriner      changed GradePtAvg and EmailAddress auto-implemented properties to instance variables
// 03-02-2022   kriner      documentation of methods
// 03-04-2022   kriner      replaced firstName, lastName, and emailAddress auto-implemented properties with ContactInfo
// 03-05-2022   kriner      documentation of class
//
// The Student class of the StudentDB program constructs an Student object with parameters for a ContactInfo object info,
// grades double, and enrolled DateTime. The ContactInfo object contains the first name, last name, and email address
// of a student. The ToString method is overriden to format the student information in the user interface while the
// virtual method ToStringForOutputFile is inherited by the Undergrad and GradStudent classes and used to format the
// student information in the text file.

using System;

// the namespace for the StudentDB program
namespace StudentDB
{
    // student class represents a student in the StudentDB
    internal class Student
    {
        // first name, last name, email
        public ContactInfo Info { get; set; }
        // student gpa
        private double gradePtAvg;
        // student enrollementD date
        public DateTime EnrollmentDate { get; set; }

        // fully specified constructor. info contains first name, last name, and email
        public Student(ContactInfo info, double grades, DateTime enrolled)
        {
            Info = info;
            GradePtAvg = grades;
            EnrollmentDate = enrolled;
        }

        // default constructor
        public Student()
        {
        }

        // property for the gradePtAvg instance variable. if the value is an appropriate
        // GPA value, the gradePtAvg variable is set to value. otherwise the gradePtAvg
        // variable is set to 0.7 as that is the lowest value reported to the UW
        public double GradePtAvg
        {
            get
            {
                return gradePtAvg;
            }
            set
            {
                if (0 <= value && value <= 4)
                {
                    gradePtAvg = value;
                }
                else
                {
                    // 0.7 is the lowest value to be reported at UW
                    gradePtAvg = 0.7;
                }
            }
        }

        // ToString for printing the output file student record
        public virtual string ToStringForOutputFile()
        {
            // buffer
            string str = string.Empty;

            // output format
            str += $"{Info.FirstName}\n";
            str += $"{Info.LastName}\n";
            str += $"{GradePtAvg}\n";
            str += $"{Info.EmailAddress}\n";
            str += $"{EnrollmentDate}\n";

            // return the string
            return str;
        }

        // override ToString for user interface printing student record
        public override string ToString()
        {
            // buffer
            string str = string.Empty;

            str += "*********** Student Record ***********\n";
            // output format
            str += $"First Name: {Info.FirstName}\n";
            str += $" Last Name: {Info.LastName}\n";
            str += $"       GPA: {GradePtAvg}\n";
            str += $"     Email: {Info.EmailAddress}\n";
            str += $"  Enrolled: {EnrollmentDate}\n";

            // return the string
            return str;
        }
    }
}