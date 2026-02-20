using System;
using Contact_Manager.Services;
using Contact_Manager.Collections;
using Contact_Manager.Models;

namespace Contact_Manager.Actions
{
    public class StatsAction
    {
        public void Execute(ContactService service)
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("--- СТАТИСТИКА ПРОЄКТУ ---");

            var allContacts = service.GetAllContacts();

            ContactCollection myCollection = new ContactCollection();

            foreach (var c in allContacts)
            {
                myCollection.Add(c);
            }

            int total = 0;
            int importantCount = 0;
            var it = myCollection.GetEnumerator();
            while (it.MoveNext())
            {
                var contact = (Contact)it.Current;
                total++;

                if (contact.Important)
                {
                    importantCount++;
                }
            }
            Console.WriteLine($"\nЗібрано статистику через кастомний ітератор:");
            Console.WriteLine($"Всього контактів у колекції: {total}");
            Console.WriteLine($"Кількість важливих контактів: {importantCount}");

            Console.ReadLine();
        }
    }
}