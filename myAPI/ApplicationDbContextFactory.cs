using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace myAPI
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            return new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySQL("server=localhost;userid=root;pwd=test;port=3306;database=myapi;sslmode=none;")
                .Options);
        }
    }
}
