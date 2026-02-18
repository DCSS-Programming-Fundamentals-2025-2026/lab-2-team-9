using Contact_Manager.Demo;
using Contact_Manager.Repositories;
using Contact_Manager.Services;
using System;
using System.Text;

namespace Contact_Manager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repository = new ContactRepository();      
            var validator = new ContactValidator();       
            var statsService = new ContactStatisticsService(); 

            var service = new ContactService(repository, validator, statsService);

            var runner = new DemoRunner(service);

            runner.Run();
        }
    }
}
