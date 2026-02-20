using System;
using Contact_Manager.Services;

namespace Contact_Manager.Actions
{
    public class ShowAllAction
    {
        public void Execute(ContactService service)
        {
            Console.Clear();
            Console.WriteLine("--- КОНТАКТИ ---");

            var contacts = service.GetAllContacts();

            if (contacts.Count == 0)
            {
                Console.WriteLine("\nСписок порожній...");
            }
            else
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {contacts[i].ToString()}");
                }
            }

            Console.ReadLine();
        }
    }
} 