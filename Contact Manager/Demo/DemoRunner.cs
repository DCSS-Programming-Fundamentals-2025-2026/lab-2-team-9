using System;
using Contact_Manager.Services;
using Contact_Manager.Actions;
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
                Console.WriteLine("7. Статистика ");
                Console.WriteLine("0. Зберегти та Вийти");
                Console.WriteLine("-------------------------------");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            new AddContactAction().Execute(_service);
                            break;
                        case "2":
                            new ShowAllAction().Execute(_service);
                            break;
                        case "3":
                            new SearchAction().Execute(_service);
                            break;
                        case "4":
                            new DeleteAction().Execute(_service);
                            break;
                        case "5":
                            new SortAction().Execute(_service);
                            break;
                        case "6":
                            new ShowImportantAction().Execute(_service);
                            break;
                        case "7":
                            new StatsAction().Execute(_service);
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
    }
}
        

    
