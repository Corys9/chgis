using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SwissVoting.DAL.DB
{
    public partial class SwissContext : DbContext
    {
        public static string DefaultConnectionString { get; set; }

        public string ConnectionString { get; set; }

        public SwissContext()
        {
        }

        public SwissContext(DbContextOptions<SwissContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChCantons> ChCantons { get; set; }
        public virtual DbSet<Laws> Laws { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(ConnectionString ?? DefaultConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("address_standardizer")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("ogr_fdw")
                .HasPostgresExtension("pgrouting")
                .HasPostgresExtension("pointcloud")
                .HasPostgresExtension("pointcloud_postgis")
                .HasPostgresExtension("postgis")
                .HasPostgresExtension("postgis_raster")
                .HasPostgresExtension("postgis_sfcgal")
                .HasPostgresExtension("postgis_tiger_geocoder")
                .HasPostgresExtension("postgis_topology");

            modelBuilder.Entity<ChCantons>(entity =>
            {
                entity.HasKey(e => e.Gid)
                    .HasName("ch-cantons_pkey");

                entity.ToTable("ch-cantons");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Engtype1)
                    .HasColumnName("engtype_1")
                    .HasMaxLength(50);

                entity.Property(e => e.Id0).HasColumnName("id_0");

                entity.Property(e => e.Id1).HasColumnName("id_1");

                entity.Property(e => e.Iso)
                    .HasColumnName("iso")
                    .HasMaxLength(3);

                entity.Property(e => e.Name0)
                    .HasColumnName("name_0")
                    .HasMaxLength(75);

                entity.Property(e => e.Name1)
                    .HasColumnName("name_1")
                    .HasMaxLength(75);

                entity.Property(e => e.NlName1)
                    .HasColumnName("nl_name_1")
                    .HasMaxLength(50);

                entity.Property(e => e.Type1)
                    .HasColumnName("type_1")
                    .HasMaxLength(50);

                entity.Property(e => e.Varname1)
                    .HasColumnName("varname_1")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Laws>(entity =>
            {
                entity.ToTable("laws");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasMaxLength(20);

                entity.Property(e => e.Proposal)
                    .IsRequired()
                    .HasColumnName("proposal")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Places>(entity =>
            {
                entity.HasKey(e => e.Gid)
                    .HasName("places_pkey");

                entity.ToTable("places");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(48);

                entity.Property(e => e.OsmId).HasColumnName("osm_id");

                entity.Property(e => e.Population).HasColumnName("population");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<Votes>(entity =>
            {
                entity.HasKey(e => new { e.PlaceId, e.LawId })
                    .HasName("votes_pkey");

                entity.ToTable("votes");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.LawId).HasColumnName("law_id");

                entity.Property(e => e.Against).HasColumnName("against");

                entity.Property(e => e.For).HasColumnName("for");

                entity.HasOne(d => d.Law)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.LawId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVote_Law");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVote_Place");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
