using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.Service
{
    public class AuthorService : IAuthorService
    {
        private AppDbContext _context;
        //public BookService(AppDbContext context)
        //{
        //    _context = context;
        //}
        public Task IsAuthorExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}
