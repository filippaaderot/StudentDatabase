using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class MenuApplication
    {
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nVälj ett alternativ:");
                Console.WriteLine("1. Registrera ny student");
                Console.WriteLine("2. Ändra befintlig student");
                Console.WriteLine("3. Lista alla studenter");
                Console.WriteLine("4. Avsluta");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        RegisterStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        ListStudents();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
        private void RegisterStudent()
        {
            using (var db = new StudentDBContext())
            {
                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine();
                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine();
                Console.Write("Stad: ");
                string city = Console.ReadLine();

                var student = new Student { FirstName = firstName, LastName = lastName, City = city };
                db.Students.Add(student);
                db.SaveChanges();
                Console.WriteLine("Ny student registrerad.");
            }
        }
        private void UpdateStudent()
        {
            using (var db = new StudentDBContext())
            {
                Console.Write("Ange StudentId: ");
                int studentid = int.Parse(Console.ReadLine());

                var student = db.Students.Find(studentid);
                if (student != null)
                {
                    Console.Write("Nytt förnamn: ");
                    student.FirstName = Console.ReadLine();
                    Console.Write("Nytt efternamn: ");
                    student.LastName = Console.ReadLine();
                    Console.Write("Ny stad: ");
                    student.City = Console.ReadLine();

                    db.SaveChanges();
                    Console.WriteLine("Student uppdaterad.");
                }
                else
                {
                    Console.WriteLine("Felaktigt StudentId");
                }
            }
        }
        private void ListStudents()
        {
            using (var db = new StudentDBContext())
            {
                var students = db.Students.ToList();
                Console.WriteLine("\nAlla studenter:");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentId}: {student.FirstName}, {student.LastName}, {student.City}");
                }
            }
        }
    }
}
