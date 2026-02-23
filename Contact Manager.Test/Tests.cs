using Contact_Manager.Comparers;
using Contact_Manager.Models;
using Contact_Manager.Repositories;
using Contact_Manager.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contact_Manager.Tests
{
    public class ContactServiceTests
    {
        private ContactService _service;
        private ContactRepository _repo;
        private ContactValidator _validator;
        private ContactStatisticsService _stats;

        [SetUp]
        public void Setup()
        {
            _repo = new ContactRepository();

            _repo.contacts.Clear();

            _validator = new ContactValidator();
            _stats = new ContactStatisticsService();

            _service = new ContactService(_repo, _validator, _stats);
        }

        [Test]
        public void AddContact_ValidData()
        {
            // Arrange
            string name = "max";
            string phone = "526227";

            // Act
            _service.AddContact(name, phone, "no");

            // Assert
            Assert.That(_service.GetTotalCount(), Is.EqualTo(1));
        }

        [Test]
        public void AddContact_WithSpaces()
        {
            // Arrange
            string name = "  Petro  ";
            string phone = " 0670004554  ";

            // Act
            _service.AddContact(name, phone, "no");

            // Assert
            var contact = _service.GetAllContacts().First();
            Assert.That(contact.Name, Is.EqualTo("Petro"));
            Assert.That(contact.Phone, Is.EqualTo("0670004554"));
        }

        [Test]
        public void AddContact_EmptyName()
        {
            // Arrange
            string name = "        ";
            string phone = "0501289";

            // Act
            _service.AddContact(name, phone, "no");

            // Assert
            var contact = _service.GetAllContacts().First();
            Assert.That(contact.Name, Is.EqualTo("0501289"));
        }

        [Test]
        public void AddContact_ImportantYes()
        {
            // Arrange
            string isImportant = "YES";

            // Act
            _service.AddContact("cat", "123", isImportant);

            // Assert
            var contact = _service.GetAllContacts().First();
            Assert.That(contact.Important, Is.True);
        }

        [Test]
        public void AddContact_InvalidPhone()
        {
            // Arrange
            string invalidPhone = "2244f";

            // Act & Assert
            try
            {
                _service.AddContact("smth", invalidPhone, "no");

                Assert.Fail("Тест провалено, бо повинен був кечнутися ArgumentException");
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Кечнувся - {ex.GetType()}, а не ArgumentException");
            }
        }

        [Test]
        public void AddContact_DuplicatePhone()
        {
            // Arrange
            _service.AddContact("User1", "3802234", "no");

            // Act & Assert
            try
            {
                _service.AddContact("User2", "3802234", "Yes");

                Assert.Fail("Тест провалено, бо повинно було викинути InvalidOperationException");
            }
            catch (InvalidOperationException)
            {
                Assert.Pass();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Очікувалась InvalidOperationException, але зловило: {ex.GetType().Name}");
            }
        }

        [Test]
        public void DeleteContact_ValidIndex()
        {
            // Arrange
            _service.AddContact("a", "123", "no");
            _service.AddContact("b", "234", "no");
            _service.AddContact("c", "456", "no");

            // Act
            _service.DeleteContact(2); 

            // Assert
            var list = _service.GetAllContacts();
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[1].Name, Is.EqualTo("c")); 
        }

        [Test]
        public void DeleteContact_InvalidIndex()
        {
            // Arrange
            _service.AddContact("Solo", "111", "no");

            // Act
            bool exception = false;

            try
            {
                _service.DeleteContact(99); 
            }
            catch (ArgumentOutOfRangeException)
            {
                exception = true; 
            }

            // Assert
            if (exception == false)
            {
                Assert.Fail("Очікувалась помилка ArgumentOutOfRangeException");
            }

            Assert.That(_service.GetTotalCount(), Is.EqualTo(1));
        }

        [Test]
        public void GetImportant_ReturnCorrectNum()
        {
            // Arrange
            _service.AddContact("opp", "111", "yes");
            _service.AddContact("smth", "222", "no");
            _service.AddContact("Nt", "333", "yes");

            // Act
            var importantList = _service.GetImportant();

            // Assert
            Assert.That(importantList.Count, Is.EqualTo(2));

            foreach (var contact in importantList)
            {
                if (contact.Important == false)
                {
                    Assert.Fail($"{contact.Name} не є важливим");
                }
            }
        }

        [Test]
        public void Search_ShouldFind_IgnoringCase()
        {
            // Arrange
            _service.AddContact("Alexandr", "9921", "no");

            // Act
            var results = _service.Search("alex");

            // Assert
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("Alexandr"));
        }

        [Test]
        public void Integration_AddDeleteAdd()
        {
            // Arrange
            _service.AddContact("First", "111", "no");
            _service.AddContact("Second", "222", "yes");

            // Act
            _service.DeleteContact(1);             
            _service.AddContact("Third", "333", "yes"); 

            // Assert
            var all = _service.GetAllContacts();
            Assert.That(all.Count, Is.EqualTo(2));
            Assert.That(all[0].Name, Is.EqualTo("Second"));
            Assert.That(all[1].Name, Is.EqualTo("Third"));
        }

        [Test]
        public void Integration_Swap_WhenSortByPhone()
        {
            // Arrange
            _service.AddContact("Ivan", "333", "no");
            _service.AddContact("ann", "111", "yes");
            _service.AddContact("Oleh", "222", "no");

            // Act
            var contacts = _service.GetAllContacts();

            Contact[] arr = contacts.ToArray();
            Array.Sort(arr, new ContactPhoneComparer());

            contacts.Clear();

            for (int i = 0; i < arr.Length; i++)
            {
                contacts.Add(arr[i]);
            }

            _service.UpdateAndSave(contacts);
            // Assert
            var savedContacts = _service.GetAllContacts();

            Assert.That(savedContacts.Count == 3);
            Assert.That(savedContacts[0].Phone == "111"); 
            Assert.That(savedContacts[1].Phone == "222"); 
            Assert.That(savedContacts[2].Phone == "333");
        }

        [Test]
        public void Integration_SomeActions()
        {
            // Arrange
            _service.AddContact("Mom", "289302010", "yes");
            _service.AddContact("Office", "338199", "yes");
            _service.AddContact("smth", "637724", "no");

            // Act 
            _service.DeleteContact(3);

            var contacts = _service.GetAllContacts();
            contacts.Sort();
            _service.UpdateAndSave(contacts);

            // Assert 
            int total = _service.GetTotalCount();
            var important = _service.GetImportant();
            var savedContacts = _service.GetAllContacts();

            Assert.That(total, Is.EqualTo(2));
            Assert.That(important.Count, Is.EqualTo(2)); 
            Assert.That(savedContacts[0].Name, Is.EqualTo("Mom")); 
            Assert.That(savedContacts[1].Name, Is.EqualTo("Office"));
        }

        [Test]
        public void Integration_SortContactsByName()
        {
            // Arrange 
            _service.AddContact("zaHa", "378221", "yes");
            _service.AddContact("ann", "2222321", "no");

            // Act 
            var contacts = _service.GetAllContacts();
            contacts.Sort(); 
            _service.UpdateAndSave(contacts);

            // Assert 
            var all = _service.GetAllContacts();

            Assert.That(all[0].Name, Is.EqualTo("ann")); 
            Assert.That(all[1].Name, Is.EqualTo("zaHa")); 
        }

        [Test]
        public void Search_ByPhoneNumber_ReturnsContact()
        {
            // Arrange
            _service.AddContact("Jo", "17728882", "no");
            _service.AddContact("jane", "3902939", "yes");

            // Act
            var results = _service.Search("3902"); 

            // Assert
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Name, Is.EqualTo("jane"));
        }

        [Test]
        public void Search_DontMatch()
        {
            // Arrange
            _service.AddContact("max", "378111", "yes");

            // Act
            var results = _service.Search("smth");

            // Assert
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddContactWithEmtyPhone()
        {
            // Arrange
            string phone = " "; 

            // Act & Assert
            try
            {
                _service.AddContact("smn", phone, "no");
                Assert.Fail("Очікувалося ArgumentException, але додало контак");
            }
            catch (ArgumentException)
            {
                Assert.Pass(); 
            }
            catch (Exception ex)
            {
                Assert.Fail($"Зловило {ex.GetType().Name}, а повинно було ArgumentException");
            }
        }
    }
}
