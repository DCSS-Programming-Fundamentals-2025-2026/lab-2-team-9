using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager.Models
{
    public abstract class PersonRecord
    {
        public string Name {  get; set; }  

        protected PersonRecord(string name)
        {
            Name = name;
        }

    }
}
