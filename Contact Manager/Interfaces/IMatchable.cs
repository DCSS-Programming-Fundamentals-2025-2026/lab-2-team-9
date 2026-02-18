using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager.Interfaces
{
    public interface IMatchable
    {
        bool MatchesQuery(string query);
    }
}
