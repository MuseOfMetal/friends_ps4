using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    class User
    {
        public static int FindUser(List<User> users, int user)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserId == user)
                    return i;
            }
            return -1;
        }
        public static void Load()
        {
            if (System.IO.File.Exists("users.json"))
                using (System.IO.StreamReader reader = new System.IO.StreamReader("users.json"))
                {
                    Program.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(reader.ReadToEnd());
                }
        }
        public static void Save()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("users.json"))
            {
                writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Program.Users));
            }
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }



        public void Update(string firstName, string lastName, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
        }
        public int GetPermissionLevel()
        {
            switch (Permission)
            {
                case "banned":
                    return -1;
                case "user":
                    return 0;
                case "admin":
                    return 1;
                case "superadmin":
                    return 2;
                default:
                    return -2;
            }
        }
        public void SetSuperAdmin()
        {
            Permission = "superadmin";
        }
        public void Ban()
        {
            Permission = "banned";
        }
        public void Unban()
        {
            Permission = "user";
        }
        public User(string firstName, string lastName, string userName, int userId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            UserId = userId;
            Permission = "user";
            Status = null;
        }
    }
}
