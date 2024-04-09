using System.Text.Json;

namespace Personal_Details_Processor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //read .txt file & remove any blank or null results.
            //Code will find the text file relative to the project directory
            string projectFolder = Directory.GetCurrentDirectory();
            string grandParentDirectory = Directory.GetParent(Directory.GetParent(projectFolder).FullName).FullName;
            string filePath = Path.Combine(grandParentDirectory, @"docs\contacts.txt");
            string[] contactDetails = File.ReadAllLines(filePath);

            //cleanup the data.
            contactDetails = contactDetails.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            //create a list of contacts
            List<Contact> contactsList = new List<Contact>();


            //add data into each Contact instance
            for (int i = 0; i < contactDetails.Length; i++)
            {
                //add results to new array and split by tab spacing
                string[] parsedContact = contactDetails[i].Split('\t', StringSplitOptions.RemoveEmptyEntries);


                //check if phone number exists
                bool PhoneNull = parsedContact.ElementAtOrDefault(3) == null;

                //create Contact objects based on bool check above
                if (PhoneNull)
                {
                    contactsList.Add(new Contact(parsedContact[0], parsedContact[1], parsedContact[2]));
                } else
                {
                    contactsList.Add(new Contact(parsedContact[0], parsedContact[1], parsedContact[2], parsedContact[3]));
                }

            }

            //sort the Contacts by date of birth using LINQ - oldest first
            var result = (from c in contactsList
                          orderby c.Birthday
                          select c);

            //LOOK into String Interpolation to rewrite the 2 lines of code below.

            Console.WriteLine("{0, -15} {1, -15} {2, -15} {3, -4} {4, -30} {5, 0}", $"{"First Name"}", $"{"Last Name"}", $"{"Date of Birth"}", $"{"Age"}", $"{"Email"}", $"{"Phone Number"}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            foreach (var p in result)
            {
                p.PrintDetails();
            }

            //print list contents to JSON file - stored in /bin/docs - contacts.json

            //define file path for JSON file
            string jsonPath = Path.Combine(grandParentDirectory, @"docs\contacts.json");
            //JSON printing options for indentation
            var options = new JsonSerializerOptions { WriteIndented = true };
            //serialize to JSON
            string json = JsonSerializer.Serialize(contactsList, options);
            //write to file
            File.WriteAllText(jsonPath, json);


            Console.WriteLine("\nContacts Parsed!\nJSON file generated!\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
