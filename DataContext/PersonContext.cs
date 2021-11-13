using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDni.Models;

namespace WebApiDni.DataContext
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) :base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
    }
}
