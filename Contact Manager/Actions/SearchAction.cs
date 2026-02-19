using System;
using Contact_Manager.Services;

namespace Contact_Manager.Actions
{
    public class SearchAction
    {
        public void Execute(ContactService service)
        {
            Console.Write("\nВведіть пошуковий запит: ");
            string query = Console.ReadLine();

            var results = service.Search(query);

            if (results.Count == 0)
            {
                Console.WriteLine("\nНічого не знайдено.");
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {results[i].ToString()}");
                }
            }

            Console.ReadLine();
        }
    }
}