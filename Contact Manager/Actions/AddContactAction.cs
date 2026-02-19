using System;
using Contact_Manager.Services;

namespace Contact_Manager.Actions
{
    public class AddContactAction
    {
        public void Execute(ContactService service)
        {
            Console.Write("\nІм'я: ");
            string name = Console.ReadLine();

            Console.Write("\nТелефон: ");
            string phone = Console.ReadLine();

            Console.Write("\nЧи є контакт важливим? (yes/no): ");
            string important = Console.ReadLine();

            service.AddContact(name, phone, important);
            Console.WriteLine("\nКонтакт додано!");
            Console.ReadLine();
        }
    }
}