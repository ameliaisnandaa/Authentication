using System;
using System.Collections.Generic;

namespace Authentication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> data = new List<Data>();

            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("BASIC AUTHENTICATION");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show User");
                Console.WriteLine("3. Delete User");
                Console.WriteLine("4. Search");
                Console.WriteLine("5. Login");
                Console.WriteLine("6. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        data.Add(Program.TambahUser(data));
                        break;
                    case "2":
                        Program.DaftarUser(data);
                        break;
                    case "3":
                        Program.Delete(data);
                        break;
                    case "4":
                        Program.Search(data);
                        break;
                    case "5":
                        Program.Login(data);
                        break;
                    case "6":
                        showMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private static Data TambahUser(List<Data> data)
        {
            //Random rnd = new Random();
            bool showMenu = false;
            string namaDepan;
            string namaBelakang;
            string userName = "";
            string password;
            do
            {
                Console.Clear();
                Console.WriteLine("CREATE USER");
                Console.WriteLine("Firstname : ");
                namaDepan = Console.ReadLine();
                Console.WriteLine("Lastname : ");
                namaBelakang = Console.ReadLine();
                Console.WriteLine("Password : ");
                string pwd = Console.ReadLine();
                password = BCrypt.Net.BCrypt.HashPassword(pwd);
                if (namaDepan.Length >= 2 && namaBelakang.Length >= 2 && pwd.Length >= 4)
                {
                    showMenu = true;
                    string[] userName1 = namaDepan.Split();
                    string[] userName2 = namaBelakang.Split();
                    //case control to upper/lower
                    string usn = userName1[0].Substring(0, 2) + userName2[0].Substring(0, 2).ToLower();
                    if (data.Exists((Predicate<Data>)(item => item.UserName==usn)))
                    {
                        int incNumber = 1;
                        string formattedIncNumber = String.Format("s{0:D2}", incNumber);
                        incNumber++;

                        userName = usn + formattedIncNumber;
                    }
                    else
                    {
                        userName = usn;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR : Input Not Valid");
                    Console.WriteLine("press Enter");
                    Console.ReadLine();
                }
            }
            while (!showMenu);
            return new Data(namaDepan, namaBelakang, userName, password);
        }
        private static void DaftarUser(List<Data> data)
        {
            Console.Clear();
            Console.WriteLine("SHOW USER");
            foreach (Data item in data)
                item.TampilkanData();
            Console.WriteLine("press Enter");
            Console.ReadLine();
        }
        private static void Login(List<Data> data)
        {
            Console.Clear();
            Console.WriteLine("LOGIN");
            Console.WriteLine("USERNAME :");
            string input1 = Console.ReadLine();
            Console.WriteLine("PASSWORD :");
            string input2 = Console.ReadLine();
            foreach (Data item in data)
            {
                if (item.UserName == input1)
                {
                    if (BCrypt.Net.BCrypt.Verify(input2, item.Password))
                        Console.WriteLine("MESSAGE : LOGIN SUCCESSFUL!");
                    else
                        Console.WriteLine("MESSAGE : WRONG PASSWORD!");
                    Console.WriteLine("press Enter");
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("MESSAGE : USERNAME NOT FOUND!");
            Console.WriteLine("press Enter");
            Console.ReadLine();
        }
        private static void Delete(List<Data> data)
        {
            int i;
            Console.Clear();
            Console.WriteLine("INPUT USERNAME DAN PASSWORD");
            Console.WriteLine("USERNAME :");
            string input1 = Console.ReadLine();
            Console.WriteLine("PASSWORD :");
            string input2 = Console.ReadLine();

            foreach (Data item in data)
            {
                if (item.UserName == input1)
                {
                    
                    if (BCrypt.Net.BCrypt.Verify(input2, item.Password))
                    {
                        i = data.IndexOf(item);
                        data.RemoveAt(i);
                        Console.WriteLine("MESSAGE : USER DELETED");
                    }
                    else
                        Console.WriteLine("MESSAGE : WRONG PASSWORD!");
                    Console.WriteLine("press Enter");
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("MESSAGE : USERNAME NOT FOUND!");
            Console.WriteLine("press Enter");
            Console.ReadLine();
        }
        private static void Search(List<Data> data)
        {
            Console.Clear();
            Console.WriteLine("INPUT NAME");
            Console.WriteLine("NAME :");
            string input1 = Console.ReadLine().ToLower();

            foreach (Data item in data)
            {
                if (item.NamaDepan.ToLower().Contains(input1) || item.NamaBelakang.ToLower().Contains(input1))
                {
                    Console.WriteLine();
                    Console.WriteLine($"NAME : {item.NamaDepan} {item.NamaBelakang}");
                    Console.WriteLine($"USERNAME : {item.UserName}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("MESSAGE : SEARCH COMPLETE");
            Console.WriteLine("press Enter");
            Console.ReadLine();
        }
    }
}
