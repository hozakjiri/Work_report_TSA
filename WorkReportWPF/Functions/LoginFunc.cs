using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Enums;
using WorkReportWPF.Models;

namespace WorkReportWPF.Functions
{
    public class LoginFunc
    {

        public static void Create(string name, string userLogin, int level, string mail)
        {
            using DbSettingsContext context = new();

            Login newLogin = new()
            {
                Name = (name == string.Empty) ? name : null,
                UserLogin = (userLogin == string.Empty) ? userLogin : null,
                Level = (level >= 0 && level <= 5) ? (LevelEnum)level : 0,
                Mail = (mail == string.Empty) ? mail : null
            };

            context.Logins.Add(new Login() { Name = newLogin.Name, UserLogin = newLogin.UserLogin, Level = newLogin.Level, Mail = newLogin.Mail });
            context.SaveChanges();
        }

        public static List<Login> ReadList(List<Login> LoginList)
        {
            if (LoginList is null)
            {
                throw new ArgumentNullException(nameof(LoginList));
            }

            using DbSettingsContext context = new();
            List<Login> TableLogin = context.Logins.ToList();
            return TableLogin;
        }

        public static string LoadAllMails()
        {

            using DbSettingsContext context = new();
            var TableLogin = context.Members.Select(x => x.Mail).ToList();
            var stringmails = "";

            if (TableLogin != null)
            {
                int tablecount = TableLogin.Count();

                for (int i = 0; i <= tablecount; i++)
                {
                    if (tablecount == i)
                    {
                        stringmails = TableLogin[i].ToString();
                    }

                    stringmails = TableLogin[i].ToString() + ",";
                }
            }
            return stringmails;
        }



        public static List<Login> LoadAllUsers()
        {
            using DbSettingsContext context = new();
            List<Login> newLogin = new();
            if (context != null)
            {
                newLogin = context.Logins.ToList();
                return newLogin;
            }
            else
            {
                return newLogin;
            }
        }

        public static List<string> LoadAllUsersList()
        {
            using DbSettingsContext context = new();
            List<string> newLogin = new();
            if (context != null)
            {
                newLogin = context.Logins.Select(x => x.Name).ToList();
                return newLogin;
            }
            else
            {
                return newLogin;
            }
        }

        public static void SaveUser(Login data)
        {
            try
            {
                using DbSettingsContext context = new();
                if (data != null)
                {
                    Login UserData = new()
                    {
                        UserLogin = data.UserLogin,
                        Mail = data.Mail,
                        Level = data.Level,
                        Name = data.Name,
                    };
                    context.Logins.Add(UserData);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteUser(int? id)
        {
            try
            {
                using DbSettingsContext context = new();

                if (id != null)
                {
                    var user = context.Logins.Find(id);
                    context.Logins.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void EditUser(Login data)
        {
            try
            {
                using (var context2 = new DbSettingsContext())
                {
                    context2.Update(data);
                    await context2.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Login LoadUserData(string UserLogin)
        {
            using DbSettingsContext context = new();
            Login newLogin = new();
            if (context != null)
            {
                newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).FirstOrDefault();
                return newLogin;
            }
            else
            {
                return newLogin;
            }
        }

        public static LevelEnum LoadUserLevel(string UserLogin)
        {
            using DbSettingsContext context = new();
            LevelEnum newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).Select(x => x.Level).FirstOrDefault();
            return LevelEnum.Level1;
        }

        public static string LoadUserLevelString(string UserLogin)
        {
            using DbSettingsContext context = new();
            LevelEnum newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).Select(x => x.Level).FirstOrDefault();
            return string.Format(newLogin.ToString());
        }

        public static string LoadUserName(string UserLogin)
        {
            using DbSettingsContext context = new();
            string newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).Select(x => x.Name).FirstOrDefault();
            return newLogin;
        }

        public static bool CheckPremisions(string UserLogin)
        {
            using DbSettingsContext context = new();
            var newLogi = context.Logins.ToList();
            //Login newLogins = context.Logins.Single(x => x.UserLogin == UserLogin);

            var newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).ToList();

            return newLogin.Count > 0;
        }

        public static int IntFromLevel(LevelEnum level)
        {
            switch (level)
            {
                case LevelEnum.Level1:
                    return (int)LevelEnum.Level1;
                case LevelEnum.Level2:
                    return (int)LevelEnum.Level2;
                case LevelEnum.Level3:
                    return (int)LevelEnum.Level3;
                case LevelEnum.Level4:
                    return (int)LevelEnum.Level4;
                case LevelEnum.Level5:
                    return (int)LevelEnum.Level5;
                case LevelEnum.Admin:
                    return (int)LevelEnum.Admin;
                default:
                    return -1;
            }
        }

        public static string StringFromLevel(LevelEnum level)
        {
            switch (level)
            {
                case LevelEnum.Level1:
                    return string.Format(Enums.LevelEnum.Level1.ToString());
                case LevelEnum.Level2:
                    return string.Format(Enums.LevelEnum.Level2.ToString());
                case LevelEnum.Level3:
                    return string.Format(Enums.LevelEnum.Level3.ToString());
                case LevelEnum.Level4:
                    return string.Format(Enums.LevelEnum.Level4.ToString());
                case LevelEnum.Level5:
                    return string.Format(Enums.LevelEnum.Level5.ToString());
                case LevelEnum.Admin:
                    return string.Format(Enums.LevelEnum.Admin.ToString());
                default:
                    return "Invalid";
            }
        }

        public static void Read()
        {
            using DbSettingsContext context = new();
            List<Login> TableLogin = context.Logins.ToList();
            //ItemLIst.ItemsSource = TableLogin;
        }

        public static void Update(string name, string userLogin, int level, string mail, int? id)
        {
            using DbSettingsContext context = new();

            //UserControl selectedUser = ItemList.SelectedItem as Login;

            //Login newLogin = context.Logins.Find(selectedUser.UserID)

            if (id != null)
            {
                Login newLogin = context.Logins.Find(id);

                newLogin.Name = (name == string.Empty) ? name : null;
                newLogin.UserLogin = (userLogin == string.Empty) ? userLogin : null;
                newLogin.Level = (level >= 0 && level <= 5) ? (LevelEnum)level : 0;
                newLogin.Mail = (mail == string.Empty) ? mail : null;

                context.SaveChanges();
            }
        }

        public static void Delete(int? id)
        {
            using DbSettingsContext context = new();

            //UserControl selectedUser = ItemList.SelectedItem as Login;

            //Login newLogin = context.Logins.Find(selectedUser.UserID)             

            if (id != null)
            {
                //Login newLogin = context.Logins.Find(id);
                Login newLogin = context.Logins.Single(x => x.UserID == id);
                context.Remove(newLogin);
                context.SaveChanges();
            }
        }

    }
}