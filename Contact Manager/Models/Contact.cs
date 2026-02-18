using Contact_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager.Models
{
    public class Contact : PersonRecord, IMatchable
    {
        public string Phone {  get; set; }

        public bool Important { get; set; }

        public Contact(string name, string phone, bool important) : base(name)
        {
            Phone = phone;
            Important = important;
        }

        public bool MatchesQuery(string query)
        {
            if(query != null)
            {
                if(Name.ToLower().Contains(query.ToLower()))
                {
                    return true;
                }

                if (Phone.Contains(query))
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("\nПеревірте коректність вашого запиту...");
                Console.WriteLine();
            }

            return false;
        }

        public override string ToString()
        {
            string markOfImportant = "";

            if (Important)
            {
                markOfImportant = " *ВАЖЛИВИЙ*";
            }

            return $"{Name} - {Phone} {markOfImportant}";
        }
    }
}
