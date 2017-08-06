namespace Newsletter.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsletterContext : DbContext
    {
        public NewsletterContext()
            : base("name=NewsletterContext1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Newsletter.Models.NewsletterUser> NewsletterUsers { get; set; }
    }
}
