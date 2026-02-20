using Contact_Manager.Models;
using Contact_Manager.Repositories;
using System;
using System.Collections.Generic;

namespace Contact_Manager.Services
{
    public class ContactService
    {
        private readonly ContactRepository repository;
        private readonly ContactValidator _validator;
        private readonly ContactStatisticsService statisticsService;

        public ContactService(ContactRepository repo, ContactValidator validator, ContactStatisticsService stats)
        {
            repository = repo;
            _validator = validator;
            statisticsService = stats;
        }

        public void AddContact(string name, string phone, string importantSTR)
        {
            if (name != null)
            {
                name = name.Trim();
            }

            if (phone != null)
            {
                phone = phone.Trim();
            }

            _validator.isValid(name, phone, repository.GetAll());

            if (string.IsNullOrWhiteSpace(name))
            {
                name = phone;
            }

            bool isImportant = false;

            if(importantSTR != null && importantSTR.Trim().ToLower() == "yes")
            {
                isImportant = true;
            }

            repository.Add(new Contact(name, phone, isImportant));
        }

        public void DeleteContact(int userIndex)
        {
            int realIndex = userIndex - 1;

            repository.RemoveAt(realIndex);
        }

        public List<Contact> GetAllContacts()
        {
            return repository.GetAll();
        }

        public List<Contact> Search(string query)
        {
            if (query != null)
            {
                query = query.Trim();
            }

            List<Contact> all = repository.GetAll();
            List<Contact> found = new List<Contact>();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].MatchesQuery(query))
                {
                    found.Add(all[i]);
                }
            }
            return found;
        }

        public int GetTotalCount()
        {
            return statisticsService.GetTotalCount(repository.GetAll());
        }

        public List<Contact> GetImportant()
        {
            return statisticsService.ImportantContacts(repository.GetAll());
        }

        public void UpdateAndSave(List<Contact> sortedContacts)
        {
            repository.UpdateList(sortedContacts);
            repository.SaveChanges();
        }

        public void SaveAndExit()
        {
            repository.SaveChanges();
            statisticsService.GenerateReportFile(repository.GetAll());
        }
    }
}
