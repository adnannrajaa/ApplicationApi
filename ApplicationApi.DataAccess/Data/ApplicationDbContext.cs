using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApplicationApi.Models.DataModels;


namespace ApplicationApi.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CompanyDetails> CompanyDetail { get; set; }
        public DbSet<CustomerDetails> CustomerDetail { get; set; }
        public DbSet<DailyExpenses> DailyExpense { get; set; }
        public DbSet<MonthlyExpenses> MonthlyExpense { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<ProductUnits> ProductUnit { get; set; }
        public DbSet<SaleDetails> SaleDetail { get; set; }
        public DbSet<Sales> Sale { get; set; }
        public DbSet<ShopsDetails> ShopsDetail { get; set; }
        public DbSet<Wages> Wages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ActionLinks> ActionLinks { get; set; }
        public DbSet<AllowedLinks> AllowedLinks { get; set; }

        public DbSet<EmployeePerformance> Performances { get; set; }



    }
}
