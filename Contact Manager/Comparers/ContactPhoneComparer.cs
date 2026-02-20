using System;
using System.Collections;
using Contact_Manager.Models;

namespace Contact_Manager.Comparers
{
  
    public class ContactPhoneComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Contact c1 = x as Contact;
            Contact c2 = y as Contact;

            if (c1 == null && c2 == null)
            {
                return 0;
            }
            if (c1 == null)
            {
                return -1;
            }
            if (c2 == null)
            {
                return 1;
            }

            return string.Compare(c1.Phone, c2.Phone, StringComparison.OrdinalIgnoreCase);
        }
    }
}