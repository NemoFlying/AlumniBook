using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.DAL
{
    public static class AlumniBookContextFactory
    {
        private static readonly AlumniBookContext dbContext = new AlumniBookContext();

        public static AlumniBookContext GetdbContext()
        {
            return dbContext;
        }
    }
}