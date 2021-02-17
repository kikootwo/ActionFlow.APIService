using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionFlow.APIService.Models;
using Microsoft.EntityFrameworkCore;

namespace ActionFlow.APIService
{
    public class AfDbContext : DbContext
    {
        public AfDbContext(DbContextOptions<AfDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Models.Action> Actions { get; set; }
    }
}
