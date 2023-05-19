using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewTodoApp
{
    internal class SignUpSignIn
    {
        static List<User> users = new List<User>();
        static ToDoApp todoapp = new ToDoApp();

        public void Menu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Please select an option: ");
            Console.WriteLine(".........................");
            Console.ResetColor();
            Console.WriteLine();

            bool exit = true;
            while (exit)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("..................................................ToDo Application.....................................................");
                Console.ResetColor();
                Console.WriteLine();
      
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("................");
                Console.WriteLine("Enter your choice: ");
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                string Choice = Console.ReadLine();
                Console.ResetColor();

                Console.Clear();

                switch (Choice)
                {
                    case "1":
                        Register();
                        break;

                    case "2":
                        Login(users);
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        void Register()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Guid.NewGuid();
            Console.WriteLine("Registration");
            Console.WriteLine("............");
            Console.ResetColor();
            Console.WriteLine();

            bool name = true;
            string fullName;

            do
            {
                Console.WriteLine("Enter your full name: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                fullName = Console.ReadLine();
                Console.ResetColor();

                bool IsMatch = Regex.IsMatch(fullName, "^[A-Za-z]{1,50}$");
                if (!IsMatch)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error, Only accept fullName, e.g Latifat Adewuyi");
                    Console.ResetColor();
                }
                else
                {
                    name = false;
                }
            } while (name);

            bool user = true;
            string username;

            do
            {
                Console.WriteLine("Enter your username: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                username = Console.ReadLine();
                Console.ResetColor();

                bool IsMatch = Regex.IsMatch(username, "^[a-zA-Z]{1,15}$");
                if (!IsMatch)
                {
                    Console.WriteLine("Error, Must be alphabet only, e.g latifat");
                }
                if ((users.Exists(u => u.UserName == username)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A user with that username address already exists.");
                    Console.ResetColor();
                }
                else
                {
                    user = false;
                }
            } while (user);

            bool emailAddress = true;
            string email;
            do
            {
                Console.WriteLine("Enter your email address: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                email = Console.ReadLine();
                Console.ResetColor();

                bool IsMatch = Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
                if (!IsMatch)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error, Enter a valid email address e.g latifatadewuyi@gmail.com");
                    Console.ResetColor();
                }
                else if ((users.Exists(u => u.Email == email)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A user with that email address already exists.");
                    Console.ResetColor();
                }
                else
                {
                    emailAddress = false;
                }
            } while (emailAddress);

            while (UserExists(users, email))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Email already exists");
                Console.ResetColor();
                Console.Write("Enter another email address: ");
                email = Console.ReadLine();
            }

            bool passkey = true;
            string password = "";

            while (passkey)
            {
                Regex regexps = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$");
                Console.Write("Enter your password: \n");
                ConsoleKeyInfo keyPassword;

                do
                {
                    keyPassword = Console.ReadKey(true);

                    if (char.IsLetterOrDigit(keyPassword.KeyChar) || keyPassword.Key == ConsoleKey.Backspace)
                    {
                        if (keyPassword.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Remove(password.Length - 1);
                            Console.Write("\b \b");
                        }
                        else
                        {
                            password += keyPassword.KeyChar;
                            Console.Write("*");
                        }
                    }
                } while (keyPassword.Key != ConsoleKey.Enter);

                if (!regexps.IsMatch(password))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPassword Requiremnet not met, Your Password must contain \nat least 1 Uppercase, Lowercase and Number ");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            bool confirmpasskey = true;
            string confirmPassword = "";

            while (confirmpasskey)
            {
                Regex regexps = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$");
                Console.WriteLine("\nConfirm password: \n");
                ConsoleKeyInfo keyPassword;

                do
                {
                    keyPassword = Console.ReadKey(true);

                    if (char.IsLetterOrDigit(keyPassword.KeyChar) || keyPassword.Key == ConsoleKey.Backspace)
                    {
                        if (keyPassword.Key == ConsoleKey.Backspace && confirmPassword.Length > 0)
                        {
                            confirmPassword = confirmPassword.Remove(confirmPassword.Length - 1);
                            Console.Write("\b \b");
                        }
                        else
                        {
                            confirmPassword += keyPassword.KeyChar;
                            Console.Write("*");
                        }
                    }
                } while (keyPassword.Key != ConsoleKey.Enter);

                if (!regexps.IsMatch(confirmPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nPassword Requiremnet not met, Your Password must contain \nat least 1 Uppercase, Lowercase and Number ");
                    Console.ResetColor();
                }

                if (confirmPassword == password)
                {
                    Guid guid = Guid.NewGuid();

                    User newUser = new User
                    {
                        FullName = fullName,
                        UserName = username,
                        Email = email,
                        Password = password,
                        Id = guid
                    };
                    users.Add(newUser);
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Registration Successful");
                    Console.ResetColor();
                    todoapp.ToDoMenu(newUser);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nPassword entered is not the same as the previous one");
                    Console.ResetColor();
                }
            }
        }

        static bool UserExists(List<User> users, string email)
        {
            foreach (User user in users)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        static void Login(List<User> users)
        {
            Regex regexps = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Login");
            Console.WriteLine(".......");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = "";
            ConsoleKeyInfo keyPassword;

            do
            {
                keyPassword = Console.ReadKey(true);

                if (char.IsLetterOrDigit(keyPassword.KeyChar) || keyPassword.Key == ConsoleKey.Backspace)
                {
                    if (keyPassword.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        password += keyPassword.KeyChar;
                        Console.Write("*");
                    }
                }
            } while (keyPassword.Key != ConsoleKey.Enter);

            while (!regexps.IsMatch(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Password Requirement not met. Your password must contain at least 1 uppercase, 1 lowercase, and 1 number.");
                Console.ResetColor();
                Console.Write("Enter a strong Password: ");
                password = Console.ReadLine();
            }

            User user = users.Find(u => u.UserName == username && u.Password == password);
            Console.Clear();

            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Log in successful!");
                Console.ResetColor();

                todoapp.ToDoMenu(user);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid username or password. Please try again.");
                Console.ResetColor();
            }
        }
    }
}
