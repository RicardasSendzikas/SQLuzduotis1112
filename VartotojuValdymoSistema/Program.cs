using System;
using System.Collections.Generic;
using VartotojuValdymoSistema.Core.Models;
using VartotojuValdymoSistema.Core.Services;

namespace VartotojuValdymoSistema
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=SQLuzduotis1112;Trusted_Connection=True;TrustServerCertificate=true;";
            UserService userService = new UserService(connectionString);

            while (true)
            {
                Console.WriteLine("Pasirinkite arba įveskite veiksmą:");
                Console.WriteLine("1: Pridėti vartotoją");
                Console.WriteLine("2: Gauti vartotoją pagal ID");
                Console.WriteLine("3: Gauti visus vartotojus");
                Console.WriteLine("4: Atlikti vartotojo atnaujinimą");
                Console.WriteLine("5: Ištrinti vartotoją");
                Console.WriteLine("6: Pakeisti vartotojo slaptažodį");
                Console.WriteLine("7: Nustatyti vartotojo aktyvumą");
                Console.WriteLine("0: Išeiti");
                string choice = Console.ReadLine();

                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Vartotojo vardas:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Slaptažodis:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Ar vartotojas aktyvus? (true/false):");
                        bool isActive = bool.Parse(Console.ReadLine());
                        Console.WriteLine("Pasirinkite rolę (Admin/StandardUser):");
                        string roleInput = Console.ReadLine();
                        UserRole role = Enum.Parse<UserRole>(roleInput, true);

                        User newUser = new User { Username = username, Password = password, IsActive = isActive, Role = role };
                        userService.RegisterUser(newUser);
                        Console.WriteLine("Vartotojas pridėtas.");
                        break;

                    case "2":
                        Console.WriteLine("Įveskite vartotojo ID:");
                        int id = int.Parse(Console.ReadLine());
                        User user = userService.GetUser(id);
                        if (user != null)
                        {
                            Console.WriteLine($"ID: {user.Id}, Vardas: {user.Username}, Aktyvus: {user.IsActive}, Rolė: {user.Role}");
                        }
                        else
                        {
                            Console.WriteLine("Vartotojas nerastas.");
                        }
                        break;

                    case "3":
                        var users = userService.GetAllUsers();
                        foreach (var u in users)
                        {
                            Console.WriteLine($"ID: {u.Id}, Vardas: {u.Username}, Aktyvus: {u.IsActive}, Rolė: {u.Role}");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Įveskite atnaujinamo vartotojo ID:");
                        int updateId = int.Parse(Console.ReadLine());
                        User userToUpdate = userService.GetUser(updateId);
                        if (userToUpdate != null)
                        {
                            Console.WriteLine("Naujas vartotojo vardas:");
                            userToUpdate.Username = Console.ReadLine();
                            Console.WriteLine("Naujas slaptažodis:");
                            userToUpdate.Password = Console.ReadLine();
                            Console.WriteLine("Ar vartotojas aktyvus? (true/false):");
                            userToUpdate.IsActive = bool.Parse(Console.ReadLine());
                            Console.WriteLine("Pasirinkite rolę (Admin/StandardUser):");
                            userToUpdate.Role = Enum.Parse<UserRole>(Console.ReadLine(), true);
                            userService.UpdateUser(userToUpdate);
                            Console.WriteLine("Vartotojas atnaujintas.");
                        }
                        else
                        {
                            Console.WriteLine("Vartotojas nerastas.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Įveskite vartotojo ID:");
                        int deleteId = int.Parse(Console.ReadLine());
                        userService.RemoveUser(deleteId);
                        Console.WriteLine("Vartotojas ištrintas.");
                        break;

                    case "6":
                        Console.WriteLine("Įveskite vartotojo ID:");
                        int pwdChangeId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Įveskite naują slaptažodį:");
                        string newPassword = Console.ReadLine();
                        userService.UpdatePassword(pwdChangeId, newPassword);
                        Console.WriteLine("Slaptažodis pakeistas.");
                        break;

                    case "7":
                        Console.WriteLine("Įveskite vartotojo ID:");
                        int statusId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ar vartotojas aktyvus? (true/false):");
                        bool isActiveStatus = bool.Parse(Console.ReadLine());
                        if (isActiveStatus)
                        {
                            userService.ActivateUser(statusId);
                            Console.WriteLine("Vartotojas aktyvuotas.");
                        }
                        else
                        {
                            userService.DeactivateUser(statusId);
                            Console.WriteLine("Vartotojas deaktyvuotas.");
                        }
                        break;

                    default:
                        Console.WriteLine("Netinkamas pasirinkimas.");
                        break;
                }
            }
        }
    }
}