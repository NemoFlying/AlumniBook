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
        }

    }
}