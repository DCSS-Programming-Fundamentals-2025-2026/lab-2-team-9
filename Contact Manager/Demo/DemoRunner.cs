using Contact_Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager.Demo
{
    public class DemoRunner
    {
        private readonly ContactService _service;

        public DemoRunner(ContactService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("    CONTACT MANAGER    ");
                Console.WriteLine("===============================");
                Console.WriteLine($"Контактів у базі: {_service.GetTotalCount()}");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Додати новий контакт");
                Console.WriteLine("2. Переглянути всі контакти");
                Console.WriteLine("3. Пошук");
                Console.WriteLine("4. Видалити контакт");
                Console.WriteLine("5. Сортувати список");
                Console.WriteLine("6. Показати лише ВАЖЛИВІ");
                Console.WriteLine("0. Зберегти та Вийти");
                Console.WriteLine("-------------------------------");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddContactMenu();
                            break;
                        case "2":
                            ShowAllMenu();
                            break;
                        case "3":
                            SearchMenu();
                            break;
                        case "4":
                            DeleteMenu();
                            break;
                        case "5":
                            SortMenu();
                            break;
                        case "6":
                            ShowImportantMenu();
                            break;
                        case "0":
                            _service.SaveAndExit(); 
                            Console.WriteLine("\nДані збережено. Роботу завершено.");
                            return;
                        default:
                            Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nПомилка! {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        private void AddContactMenu()
        {
            Console.Write("\nІм'я: ");
            string name = Console.ReadLine();

            Console.Write("\nТелефон: ");
            string phone = Console.ReadLine();

            Console.Write("\nЧи є контакт важливим? (yes/no): ");
            string important = Console.ReadLine();

            _service.AddContact(name, phone, important);
            Console.WriteLine("\nКонтакт додано!");
            Console.ReadLine();
        }

        private void ShowAllMenu()
        {
            Console.Clear();
            Console.WriteLine("--- КОНТАКТИ ---");

            var contacts = _service.GetAllContacts();

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

        private void SearchMenu()
        {
            Console.Write("\nВведіть пошуковий запит: ");
            string query = Console.ReadLine();

            var results = _service.Search(query);

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

        private void DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("--- ВИДАЛЕННЯ ---");

            var contacts = _service.GetAllContacts();

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
                    _service.DeleteContact(idx);
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

        private void SortMenu()
        {
            _service.SortContacts();
            Console.WriteLine("\nСписок відсортовано");
            Console.ReadLine();
        }

        private void ShowImportantMenu()
        {
            Console.Clear();
            Console.WriteLine("--- ВАЖЛИВІ КОНТАКТИ ---");

            var important = _service.GetImportant();

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
