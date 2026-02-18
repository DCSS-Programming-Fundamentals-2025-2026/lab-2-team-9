using Contact_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Contact_Manager.Repositories
{
    public class ContactRepository
    {
        public List<Contact> contacts;
        private readonly string path = "contacts.txt";

        public ContactRepository()
        {
            contacts = new List<Contact>();
            LoadFromFile();
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public void RemoveAt(int idx)
        {
            if (idx < 0 || idx >= contacts.Count)
            {
                throw new ArgumentOutOfRangeException("\nКонтакту з таким номером не існує");
            }

            contacts.RemoveAt(idx);
        }

        public List<Contact> GetAll()
        {
            return new List<Contact>(contacts);
        }

        public void UpdateList(List<Contact> sortedContacts)
        {
            contacts = sortedContacts;
        }

        public int Count()
        {
            return contacts.Count;
        }

        public void SaveChanges()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < contacts.Count; i++)
                    {
                        sw.WriteLine($"{contacts[i].Name},{contacts[i].Phone},{contacts[i].Important}");
                    }
                }
            }
            catch (Exception)
            {
                throw new IOException("Не вдалося зберегти файл контактів.");
            }
        }

        private void LoadFromFile()
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                using (StreamReader rdr = new StreamReader(path))
                {
                    string line;

                    while ((line = rdr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 3)
                        {
                            string name = parts[0];
                            string phone = parts[1];
                            bool important = false;

                            if (parts[2].ToLower() == "yes")
                            {
                                important = true;
                            }

                            contacts.Add(new Contact(name, phone, important));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                contacts = new List<Contact>();
                Console.WriteLine($"\nПомилка! {ex.Message}");
                Console.WriteLine();
            }
        }
    }
}
