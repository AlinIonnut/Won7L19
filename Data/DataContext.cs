﻿using Microsoft.EntityFrameworkCore;
using Won7E1.Models;

namespace Won7E1.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { } // Databsae.EnsureCreated(); se poate pune intre {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Mark> Marks {  get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
