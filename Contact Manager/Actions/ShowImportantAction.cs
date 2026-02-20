using System;
using Contact_Manager.Services;

namespace Contact_Manager.Actions
{
    public class ShowImportantAction
    {
        public void Execute(ContactService service)
        {
            Console.Clear();
            Console.WriteLine("--- ВАЖЛИВІ КОНТАКТИ ---");

            var important = service.GetImportant();

            if (important.Count == 0)
            {
                Console.WriteLine("\nВажливих контактів немає...");
            }
            else
            {
                for (int i = 0; i < important.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {important[i].ToString()}");
                }
            }

            Console.ReadLine();
        }
    }
}