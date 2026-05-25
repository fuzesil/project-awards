namespace Awards.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Descendant class of <see cref="DbContext"/> fit for the project's purpose.
    /// </summary>
    public partial class AwardsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwardsDbContext"/> class.
        /// </summary>
        public AwardsDbContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Brand"/>.
        /// </summary>
        public virtual DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Product"/>.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="ExpertGroup"/>.
        /// </summary>
        public virtual DbSet<ExpertGroup> ExpertGroups { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Member"/>.
        /// </summary>
        public virtual DbSet<Member> Members { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="DbSet{TEntity}"/> of <see cref="Country"/>.
        /// </summary>
        public virtual DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Configures the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="optionsBuilder"/> is null. </exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), nameof(this.OnConfiguring) + " must not take a null parameter!");
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseInMemoryDatabase(nameof(AwardsDbContext));
            }
        }

        /// <summary>
        /// Further configures the model that was discovered by convention from
        /// the entity types exposed in <see cref="DbSet{TEntity}"/> properties on the derived context.
        /// </summary>
        /// <param name="modelBuilder"> The builder being used to construct the model for this context. </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="modelBuilder"/> is null. </exception>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder), nameof(this.OnModelCreating) + " must not take a null parameter!");
            }

            modelBuilder.Entity<ExpertGroup>(entity =>
            {
                entity.HasKey(expertgroup => expertgroup.ExpertGroupID);
                entity.Property(expertgroup => expertgroup.Name).HasMaxLength(64).IsRequired();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(member => member.MemberID);
                entity.Property(member => member.Name).HasMaxLength(64).IsRequired();
                entity.Property(member => member.Website).HasMaxLength(128).IsRequired();
                entity.Property(member => member.OfficeLocation).HasMaxLength(256).IsRequired();
                entity.Property(member => member.Publisher).HasMaxLength(128).IsRequired();
                entity.Property(member => member.ChiefEditor).HasMaxLength(64).IsRequired();
                entity.Property(member => member.PhoneNumber).HasMaxLength(32).IsRequired();

                entity.HasOne(member => member.ExpertGroup)
                    .WithMany(expertgroup => expertgroup.Members)
                    .HasForeignKey(member => member.ExpertGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(member => member.Country)
                    .WithMany(country => country.Members)
                    .HasForeignKey(member => member.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(country => country.CountryID);
                entity.Property(country => country.Name).HasMaxLength(32).IsRequired();
                entity.Property(country => country.CapitalCity).HasMaxLength(32).IsRequired();
                entity.Property(country => country.CallingCode).IsRequired();
                entity.Property(country => country.PPPperCapita).IsRequired();
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(manufacturer => manufacturer.BrandId);
                entity.Property(manufacturer => manufacturer.Name).HasMaxLength(32).IsRequired();
                entity.Property(manufacturer => manufacturer.Address).HasMaxLength(128);
                entity.Property(manufacturer => manufacturer.Homepage).HasMaxLength(64).IsRequired();

                entity.HasOne(brand => brand.Country)
                    .WithMany(country => country.Brands)
                    .HasForeignKey(manufacturer => manufacturer.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.ProductID);
                entity.Property(product => product.Name).HasMaxLength(64).IsRequired();
                entity.Property(product => product.Price).IsRequired();
                entity.Property(product => product.Category).HasMaxLength(128).IsRequired();
                entity.Property(product => product.LaunchDate).IsRequired();
                entity.Property(product => product.EstimatedLifetime).IsRequired();

                entity.HasOne(product => product.Brand)
                    .WithMany(manufacturer => manufacturer.Products)
                    .HasForeignKey(product => product.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(product => product.ExpertGroup)
                    .WithMany(expertgroup => expertgroup.Products)
                    .HasForeignKey(product => product.ExpertGroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ExpertGroup>().HasData(DataSeeder.GetExpertGroups);
            modelBuilder.Entity<Country>().HasData(DataSeeder.GetCountries);
            modelBuilder.Entity<Member>().HasData(DataSeeder.GetMembers);
            modelBuilder.Entity<Brand>().HasData(DataSeeder.GetBrands);
            modelBuilder.Entity<Product>().HasData(DataSeeder.GetProducts);
        }
    }
}
