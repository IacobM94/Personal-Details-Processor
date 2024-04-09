using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Personal_Details_Processor
{
    internal class Contact
    {

        private string _name;
        private string _dob;
        private string _email;
        private string? _phoneNumber;
        private DateTime _parsedDOB;
        private int _age;
        private string _firstName;
        private string _lastName;

        //Properties to expose fields for JSON file write
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string Dob => _dob;
        public int Age => _age;
        public string Email => _email;
        public string? PhoneNumber => _phoneNumber;

        //ignore this property for JSON file write
        [JsonIgnore]
        public DateTime Birthday => _parsedDOB;

        //constructor for rows which provide all the info we're looking for
        public Contact(string name, string dOB, string email, string? phoneNumber = null)
        {
            _name = name;
            _dob = dOB;
            _email = email;
            _phoneNumber = phoneNumber;

            //parse date of birth and calculate age
            _parsedDOB = DateTime.Parse(_dob);
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int birthDate = int.Parse(_parsedDOB.ToString("yyyyMMdd"));
            _age = (now - birthDate) / 10000;

            //get First & Last Name from Name;
            string[] firstAndLastName = _name.Split(' ');
            _firstName = firstAndLastName[0];
            _lastName = firstAndLastName[1];

            //adding white space to the mobile number
            if (_phoneNumber != null && _phoneNumber.Length > 5)
            {
                _phoneNumber = _phoneNumber.Insert(5, " ");
                //removing excess characters if phone number too long
                if (_phoneNumber.Length > 12)
                {
                    _phoneNumber = _phoneNumber.Substring(0, 12);
                }
            }
        }

        public void PrintDetails()
        {
            Console.WriteLine("{0, -15} {1, -15} {2, -15} {3, -4} {4, -30} {5, 0}", $"{_firstName}", $"{_lastName}", $"{_dob}", $"{_age}", $"{_email}", $"{_phoneNumber}");
        }


    }
}
