using System;
using Contact_Manager.Services;
using Contact_Manager.Comparers;
using Contact_Manager.Models;

namespace Contact_Manager.Actions
{
    public class SortAction
    {
        public void Execute(ContactService service)
        {
            Console.Clear();
            Console.WriteLine("--- СОРТУВАННЯ ---");
            Console.WriteLine("\n1. За замовчуванням (за ім'ям)");
            Console.WriteLine("2. Альтернативне (за телефоном)");
            Console.Write("\nОберіть тип сортування: ");

            string sortChoice = Console.ReadLine();
            var contacts = service.GetAllContacts();

            if (sortChoice == "1")
            {
                contacts.Sort(); 
                Console.WriteLine("\nВідсортовано за ім'ям!");
            }
            else if (sortChoice == "2")
            { 
                Contact[] arr = contacts.ToArray();
                Array.Sort(arr, new ContactPhoneComparer());

                contacts.Clear();

                for (int i = 0; i < arr.Length; i++)
                {
                    contacts.Add(arr[i]);
                }

                Console.WriteLine("\nВідсортовано за номером телефону!");
            }
            else
            {
                Console.WriteLine("\nНевірний вибір.");
                Console.ReadLine();
                return;
            }

            service.UpdateAndSave(contacts);

            Console.ReadLine();
        }
    }
}