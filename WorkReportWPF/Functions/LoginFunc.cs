using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkReportWPF.Models;
using WorkReportWPF.Enums;

namespace WorkReportWPF.Functions
{
    public class LoginFunc
    {

        public static void Create(string name, string userLogin, int level, string mail)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {

                Login newLogin = new Login()
                {
                    Name = (name == string.Empty) ? name : null,
                    UserLogin = (userLogin == string.Empty) ? userLogin : null,
                    Level = (level >= 0 && level <= 5) ? (LevelEnum)level : 0,
                    Mail = (mail == string.Empty) ? mail : null
                };

                context.Logins.Add(new Login() { Name = newLogin.Name, UserLogin = newLogin.UserLogin, Level = newLogin.Level, Mail = newLogin.Mail });
                context.SaveChanges();
            }
        }

        public static List<Login> ReadList(List<Login> LoginList)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                List<Login> TableLogin = context.Logins.ToList();
                return TableLogin;
            }
        }

        public static Login LoadUserData(string UserLogin)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                Login newLogin = new Login();
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
        }

        public static LevelEnum LoadUserLevel(string UserLogin)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                LevelEnum newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).Select(x => x.Level).FirstOrDefault();
                return LevelEnum.Level1;
            }
        }

        public static string LoadUserLevelString(string UserLogin)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                LevelEnum newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).Select(x => x.Level).FirstOrDefault();
                return string.Format(newLogin.ToString());
            }
        }

        public static bool CheckPremisions(string UserLogin)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                var newLogi = context.Logins.ToList();
                //Login newLogins = context.Logins.Single(x => x.UserLogin == UserLogin);

                var newLogin = context.Logins.Where(x => x.UserLogin == UserLogin).ToList();

                return newLogin.Count > 0;
            }
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
            using (DbSettingsContext context = new DbSettingsContext())
            {
                List<Login> TableLogin = context.Logins.ToList();
                //ItemLIst.ItemsSource = TableLogin;
            }
        }

        public static void Update(string name, string userLogin, int level, string mail, int? id)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {

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
        }

        public static void Delete(int? id)
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {

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
}