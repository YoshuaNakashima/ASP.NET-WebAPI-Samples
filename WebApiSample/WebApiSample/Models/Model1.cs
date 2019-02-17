namespace WebApiSample.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public partial class Model1 : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public Model1()
            : base("name=Model1")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<categories> categories { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<tree_paths> tree_paths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categories>()
                .HasMany(e => e.tree_paths)
                .WithRequired(e => e.categories)
                .HasForeignKey(e => e.ancestor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.tree_paths1)
                .WithRequired(e => e.categories1)
                .HasForeignKey(e => e.descendant)
                .WillCascadeOnDelete(false);
        }
    }
}
