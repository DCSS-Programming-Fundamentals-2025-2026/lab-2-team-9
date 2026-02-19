using System;
using Contact_Manager.Services;

namespace Contact_Manager.Actions
{
    public class DeleteAction
    {
        public void Execute(ContactService service)
        {
            Console.Clear();
            Console.WriteLine("--- ВИДАЛЕННЯ ---");

            var contacts = service.GetAllContacts();

            if (contacts.Count == 0)
            {
                Console.WriteLine("\nСписок порожній!");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"\n{i + 1}. {contacts[i].ToString()}");
            }

            Console.Write("\nВведіть номер для видалення: ");

            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                if (idx > 0 && idx <= contacts.Count)
                {
                    service.DeleteContact(idx);
                    Console.WriteLine("\nКонтакт був видалений");
                }
                else
                {
                    Console.WriteLine("\nКонтакту під таким номером немає!");
                }
            }
            else
            {
                throw new Exception("\nЦе не число!");
            }

            Console.ReadKey();
        }
    }
}