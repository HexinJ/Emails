using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the CSV file name:");
        string fileName = Console.ReadLine();

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        if (File.Exists(filePath))
        {
            List<string> validEmails = new List<string>();
            List<string> invalidEmails = new List<string>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length > 2) // Assuming the email is in the third column
                    {
                        string email = fields[2].Trim();
                        if (IsValidEmail(email))
                        {
                            validEmails.Add(email);
                        }
                        else
                        {
                            invalidEmails.Add(email);
                        }
                    }
                }
            }

            Console.WriteLine("Valid Email Addresses:");
            foreach (string validEmail in validEmails)
            {
                Console.WriteLine(validEmail);
            }

            Console.WriteLine("\nInvalid Email Addresses:");
            foreach (string invalidEmail in invalidEmails)
            {
                Console.WriteLine(invalidEmail);
            }
        }
        else
        {
            Console.WriteLine($"Error: File '{fileName}' not found.");
        }
    }

    static bool IsValidEmail(string email)
    { 
        //string regex = @"^[^@\s]+@[^@\s]+\.[a-zA-Z]*$";
        string regex = @"^[\w]+@[\w]+\.[\w]+$";
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }
}
