using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using Npgsql;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.DbContextVoiceAPI
{
    public class VoiceAPIDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobInvitation> JobInvitations { get; set; }
        public DbSet<FavouriteJob> FavouriteJobs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Ward> Wards { get; set; }

        public VoiceAPIDbContext(DbContextOptions<VoiceAPIDbContext> options) : base(options)
        {
        }

        static VoiceAPIDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AccentEnum>(pgName: "Accent");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AccountStatusEnum>(pgName: "AccountStatus");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GenderEnum>(pgName: "Gender");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<JobInvitationStatusEnum>(pgName: "JobInvitationStatus");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<JobStatusEnum>(pgName: "JobStatus");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<OrderStatusEnum>(pgName: "OrderStatus");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<RoleEnum>(pgName: "Role");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionTypeEnum>(pgName: "TransactionType");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<WorkingStatusEnum>(pgName: "WorkingStatus");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(a => a.Status).HasDefaultValue(AccountStatusEnum.INACTIVE);
            modelBuilder.Entity<Account>().Property(a => a.UpdatedTime).IsRequired(false);
            modelBuilder.Entity<Account>().Property(a => a.DeletedTime).IsRequired(false);
            modelBuilder.Entity<Account>().Property(a => a.Password).IsRequired(false);

            modelBuilder.Entity<Candidate>().Property(c => c.Gender).HasDefaultValue(GenderEnum.OTHER);
            modelBuilder.Entity<Candidate>().Property(c => c.Status).HasDefaultValue(WorkingStatusEnum.AVAILABLE);
            modelBuilder.Entity<Candidate>().Property(c => c.DOB).IsRequired(false);
            modelBuilder.Entity<Candidate>().Property(c => c.Accent).HasDefaultValue(AccentEnum.OTHER);

            modelBuilder.Entity<Order>().Property(o => o.Status).HasDefaultValue(OrderStatusEnum.PENDING);
            modelBuilder.Entity<Order>().Property(o => o.UpdatedTime).IsRequired(false);

            modelBuilder.Entity<Review>().Property(r => r.UpdatedTime).IsRequired(false);
            modelBuilder.Entity<Review>().Property(r => r.DeletedTime).IsRequired(false);

            modelBuilder.Entity<Job>().Property(j => j.IsPublic).HasDefaultValue(true);
            modelBuilder.Entity<Job>().Property(j => j.JobStatus).HasDefaultValue(JobStatusEnum.PENDING);

            modelBuilder.Entity<TransactionHistory>().Property(ts => ts.JobId).IsRequired(false);
            modelBuilder.Entity<TransactionHistory>().Property(ts => ts.AdminId).IsRequired(false);

            modelBuilder.Entity<Wallet>().Property(w => w.AvailableBalance).HasDefaultValue(0M);
            modelBuilder.Entity<Wallet>().Property(w => w.LockedBalance).HasDefaultValue(0M);

            modelBuilder.HasPostgresEnum<AccentEnum>(name: "Accent");
            modelBuilder.HasPostgresEnum<AccountStatusEnum>(name: "AccountStatus");
            modelBuilder.HasPostgresEnum<GenderEnum>(name: "Gender");
            modelBuilder.HasPostgresEnum<JobInvitationStatusEnum>(name: "JobInvitationStatus");
            modelBuilder.HasPostgresEnum<JobStatusEnum>(name: "JobStatus");
            modelBuilder.HasPostgresEnum<OrderStatusEnum>(name: "OrderStatus");
            modelBuilder.HasPostgresEnum<RoleEnum>(name: "Role");
            modelBuilder.HasPostgresEnum<TransactionTypeEnum>(name: "TransactionType");
            modelBuilder.HasPostgresEnum<WorkingStatusEnum>(name: "WorkingStatus");
        }
    }
}
