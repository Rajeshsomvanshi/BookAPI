using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.Service
{
    interface IAuthorService
    {
        Task IsAuthorExists(string name);
    }
}
