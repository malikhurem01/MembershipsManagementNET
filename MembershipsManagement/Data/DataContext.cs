using MembershipsManagement.Entities;
using MembershipsManagement.Models;
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

            _modelBuilder.Entity<Project>().HasMany(p => p.Incomes).WithOne(p => p.Project).HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Income>().HasOne(p => p.Supervisor).WithMany(p => p.RecordedIncomes).HasForeignKey(p => p.SupervisorId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Member>().HasOne(c => c.Dzemat).WithMany(d => d.Members).HasForeignKey(c => c.DzematId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Dzemat>().HasOne(d => d.Medzlis).WithMany(m => m.Dzemati).HasForeignKey(d => d.MedzlisId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Dzemat>().HasMany(d => d.Spreadsheets).WithOne(b => b.Dzemat).HasForeignKey(b => b.DzematId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Payment>().HasOne(u => u.Member).WithMany(c => c.Payments).HasForeignKey(u => u.MemberId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Spreadsheet>().HasMany(s => s.Members).WithMany(m => m.Spreadsheets).UsingEntity<MemberInfo>();
            _modelBuilder.Entity<Spreadsheet>().HasMany(b => b.MemberInfo).WithOne(c => c.Spreadsheet).HasForeignKey(b => b.SpreadsheetId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Project>().HasMany(p => p.Expenses).WithOne(e => e.Project).HasForeignKey(e => e.ProjectId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<Member>().HasMany(m => m.MemberInfo).WithOne(m => m.Member).HasForeignKey(m => m.MemberId).OnDelete(DeleteBehavior.ClientSetNull);
            _modelBuilder.Entity<MemberInfo>().Ignore(mi => mi.MembershipFee);
            _modelBuilder.Entity<MemberInfo>().Ignore(mi => mi.TotalAmountPayed);
            // Authentication models |INHERITANCE|
            _modelBuilder.Entity<User>().ToTable("Users");
            _modelBuilder.Entity<Supervisor>().ToTable("Supervisors");
            _modelBuilder.Entity<Admin>().ToTable("Admins");
            _modelBuilder.Entity<Dzemat>().ToTable("Dzemati");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Dzemat> Dzemati { get; set; }
        public DbSet<Spreadsheet> Spreadsheets { get; set; }
        public DbSet<Debt> Debt { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberInfo> MembersInfo { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Donator> Donators { get; set; }
        public DbSet<Medzlis> Medzlisi { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
