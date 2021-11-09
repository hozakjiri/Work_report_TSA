using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF.Functions
{
    public class MemberFunc
    {

        public static List<Member> LoadMembersData()
        {
            using DbSettingsContext context = new();
            List<Member> MembersMail = new();
            if (context != null)
            {
                MembersMail = context.Members.ToList();
                return MembersMail;
            }
            else
            {
                return MembersMail;
            }
        }

        public static void SaveMembers(string mail)
        {

            try
            {
                using DbSettingsContext context = new();
                if (!string.IsNullOrEmpty(mail))
                {
                    Member MemberData = new()
                    {
                        Mail = mail,
                    };
                    context.Members.Add(MemberData);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteMember(int? id)
        {

            try
            {
                using DbSettingsContext context = new();

                if (id != null)
                {
                    var customer = context.Members.Find(id);
                    context.Members.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
