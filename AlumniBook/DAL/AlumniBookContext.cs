using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using AlumniBook.Models;

namespace AlumniBook.DAL
{
    public class AlumniBookContext:DbContext
    {
        public AlumniBookContext()
            : base("AlumniBookContext")
        {
        }

        public DbSet<ClassInfo> ClassInfo { get; set; }

        public DbSet<ClassAlbum> ClassAlbum { get; set; }

        public DbSet<ClassNotice> ClassNotice { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<ClassQuestion> ClassQuestion { get; set; }
        public DbSet<ClassLeavingMessage> ClassLeavingMessage { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定单数形式的表名
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<ClassInfo>().HasMany(t => t.adminUser).WithMany(t => t.ClassInfo).Map(m =>
            //    {
            //        m.ToTable("ClassAdminUser");
            //        m.MapLeftKey("ClassId1");
            //        m.MapRightKey("UserId1");
            //    });
            modelBuilder.Entity<ClassInfo>().HasMany(m => m.adminUser).WithMany(t => t.AdminClass)
                .Map(m =>
                {
                    m.ToTable("ClassAdminUser");
                    m.MapLeftKey("ClassId");
                    m.MapRightKey("UserId");
                });
            modelBuilder.Entity<ClassInfo>().HasMany(t => t.User).WithMany(t => t.UserClass).Map(m =>
            {
                m.ToTable("ClassUser");
                m.MapLeftKey("ClassId");
                m.MapRightKey("UserId");
            });
        }

    }
}