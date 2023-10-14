using MembershipsManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            base.OnModelCreating(_modelBuilder);

            _modelBuilder.Entity<Projekat>().HasMany(p => p.Prihodi).WithOne(p => p.Projekat).HasForeignKey(p => p.ProjekatId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Prihod>().HasOne(p => p.Korisnik).WithMany(p => p.ProknjizeniPrihodi).HasForeignKey(p => p.KorisnikId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Clan>().HasOne(c => c.Dzemat).WithMany(d => d.Clanovi).HasForeignKey(c => c.DzematId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Dzemat>().HasOne(d => d.Medzlis).WithMany(m => m.Dzemati).HasForeignKey(d => d.MedzlisId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Dzemat>().HasMany(d => d.Baze).WithOne(b => b.Dzemat).HasForeignKey(b => b.DzematId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Uplata>().HasOne(u => u.Clan).WithMany(c => c.Uplate).HasForeignKey(u => u.ClanId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Baza>().HasMany(b => b.ClanInfo).WithOne(c => c.Baza).HasForeignKey(b => b.BazaId).OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Baza> Baze { get; set; }
        public DbSet<Clan> Clanovi { get; set; }
        public DbSet<ClanInfo> ClanInfo { get; set; }
        public DbSet<ClanPorodice> ClanPorodice { get; set; }
        public DbSet<Donator> Donatori { get; set; }
        public DbSet<Dug> Dugovi { get; set; }
        public DbSet<Dzemat> Dzemat { get; set; }
        public DbSet<Medzlis> Medzlis { get; set; }
        public DbSet<Prihod> Prihodi { get; set; }
        public DbSet<Projekat> Projekti { get; set; }
        public DbSet<Trosak> Troskovi { get; set; }
        public DbSet<Uplata> Uplate { get; set; }
    }
}
