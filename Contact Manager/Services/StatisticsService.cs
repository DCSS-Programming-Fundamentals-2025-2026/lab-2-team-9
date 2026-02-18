using Contact_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Contact_Manager.Services
{
    public class ContactStatisticsService
    {
        public int GetTotalCount(List<Contact> contacts)
        {
            return contacts.Count;
        }

        public List<Contact> ImportantContacts(List<Contact> contacts)
        {
            List<Contact> important = new List<Contact>();

            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].Important)
                {
                    important.Add(contacts[i]);
                }
            }

            return important;
        }

        public void GenerateReportFile(List<Contact> contacts)
        {
            string path = "contacts_view.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("===== ВАЖЛИВІ КОНТАКТИ =====");
                    sw.WriteLine();

                    int idx = 1;

                    for (int i = 0; i < contacts.Count; i++)
                    {
                        if (contacts[i].Important)
                        {
                            sw.WriteLine($"{idx}. {contacts[i]}");
                            idx++;
                        }
                    }

                    sw.WriteLine();
                    sw.WriteLine("===== УСІ КОНТАКТИ =====");
                    sw.WriteLine();

                    for (int i = 0; i < contacts.Count; i++)
                    {
                        sw.WriteLine($"{i + 1}. {contacts[i]}");
                    }
                }
            }
            catch (Exception)
            {
                throw new IOException("\nНе вдалося створити файл для звіту...");
            }
        }
    }
}
