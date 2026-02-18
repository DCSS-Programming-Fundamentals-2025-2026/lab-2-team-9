using Contact_Manager.Models;
using System;
using System.Collections.Generic;

namespace Contact_Manager.Services
{
    public class ContactValidator
    {
        public void isValid(string name, string phone, List<Contact> existingContacts)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("\nНомер телефону є обов'язковим!");
            }

            int reviewer;

            if (!int.TryParse(phone, out reviewer))
            {
                throw new ArgumentException("\nНомер телефону повинен містити тільки цифри!");
            }

            for (int i = 0; i < existingContacts.Count; i++)
            {
                if (existingContacts[i].Phone == phone)
                {
                    throw new InvalidOperationException("\nКонтакт з таким номером вже існує!");
                }
            }
        }
    }
}
