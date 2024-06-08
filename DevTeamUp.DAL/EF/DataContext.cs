using DevTeamUp.DAL.EF.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int >
    {
        public DataContext(DbContextOptions op)
            :base(op)
        {

        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectApplication> ProjectApplications { get; set; } 
    }
}
