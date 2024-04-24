using Microsoft.EntityFrameworkCore;
using PrioritySearchProgram.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySearchProgram.Database
{
    class DatabaseContext : DbContext
    {
        public DbSet<ConcertTicket> ConcertTickets { get; set; }
        public DbSet<FootballGameTicket> FootballGameTickets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string solutionFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databaseFile = "PrioritySearchProgram.db";
            string databasePath = Path.Combine(solutionFolder, databaseFile);
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConcertTicket>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<FootballGameTicket>().Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}