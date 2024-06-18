//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 03-04-2022   kriner      creation of file, initialization of auto-implemented properties, creation of ctor
// 03-04-2022   kriner      created property for emailAddress instance variable
// 03-04-2022   kriner      overrided ToString method, created ToStringLegal method
// 03-05-2022   kriner      documentation of program
//
// The ContactInfo class is a broad class that holds a first name string, last name string, and email address string
// and overrides the ToString method to output in cleanly with the other student information. The ToStringLegal method
// allows another format for ContactInfo to be output in. The ContactInfo class has the potential to be used be 
// different programs given its widespread use as a person identifier.

// the namespace for the StudentDB program
namespace StudentDB
{
    // declaration of ContactInfo class that Student and the classes that inherit it use
    // to hold a student's first name, last name, and email address
    public class ContactInfo
    {
        // first name of student
        public string FirstName { get; set; }
        // last name of student
        public string LastName { get; set; }
        // email address of student (unique key)
        private string emailAddress;

        // ctor for ContactInfo
        public ContactInfo(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
        }

        // property for emailAddress instance variable. if the value contains the @ symbol and
        // is longer than 3 characters the emailAddress variable is set to that value. otherwise
        // the emailAddress variable is set to a string signaling an error
        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                // passed in email must have more than 3 chars and one of them must be '@'
                if (value.Contains("@") && value.Length > 3)
                {
                    emailAddress = value;
                }
                else
                {
                    emailAddress = "ERROR: Invalid Email";
                }
            }
        }

        // expression bodied override method for ToString that formats ContactInfo information
        public override string ToString() => $"{FirstName} {LastName}\n{EmailAddress}";
        // expression bodied method that outputs ContactInfo in a legal format
        public string ToStringLegal() => $"{LastName}, {FirstName}\n{EmailAddress}";
    }
}