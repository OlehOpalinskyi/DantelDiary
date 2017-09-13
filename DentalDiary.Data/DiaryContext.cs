namespace DentalDiary.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class DiaryContext : DbContext
    {
        // Your context has been configured to use a 'DiaryContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DentalDiary.Data.DiaryContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DiaryContext' 
        // connection string in the application configuration file.
        public DiaryContext()
            : base("name=DiaryContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<PriceDataModel> PriceList { get; set; }
        public virtual DbSet<PersonDataModel> Persons { get; set; }
        public virtual DbSet<ReceptionDataModel> Receptions { get; set; }
        public virtual DbSet<CityDataModel> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<ReceptionDataModel>().WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<ReceptionDataModel>().HasRequired(x => x.Price).WithMany().WillCascadeOnDelete(false);
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}