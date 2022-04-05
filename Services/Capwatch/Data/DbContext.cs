
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UnitPlanner.Services.Capwatch.Models;

namespace UnitPlanner.Services.Capwatch.Data;

public class CapwatchDbContext : DbContext
{
    public DbSet<CadetAchv> CadetAchvs => Set<CadetAchv>();
    public DbSet<CadetAchvAprs> CadetAchvAprs => Set<CadetAchvAprs>();
    public DbSet<CadetActivities> CadetActivities => Set<CadetActivities>();
    public DbSet<CadetDutyPosition> CadetDutyPositions => Set<CadetDutyPosition>();
    public DbSet<CadetHFZInformation> CadetHFZInformation => Set<CadetHFZInformation>();
    public DbSet<CdtAchvEnum> CadetAchvEnum => Set<CdtAchvEnum>();
    public DbSet<DutyPosition> DutyPositions => Set<DutyPosition>();
    public DbSet<MbrContact> MbrContact => Set<MbrContact>();
    public DbSet<OFlight> OFlights => Set<OFlight>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Member> Members => Set<Member>();

    public CapwatchDbContext(DbContextOptions<CapwatchDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>();

        modelBuilder.Entity<CadetAchv>()
            .HasKey(ca => new { ca.CAPID, ca.CadetAchvID });

        modelBuilder.Entity<CadetAchvAprs>()
            .HasKey(ca => new { ca.CAPID, ca.CadetAchvID });

        modelBuilder.Entity<CadetActivities>()
            .HasKey(ca => new { ca.CAPID, ca.Completed });

        modelBuilder.Entity<CadetDutyPosition>()
            .HasKey(dp => new { dp.CAPID, dp.Duty, dp.ORGID, dp.Asst });

        modelBuilder.Entity<DutyPosition>()
            .HasKey(dp => new { dp.CAPID, dp.Duty, dp.ORGID, dp.Asst });

        modelBuilder.Entity<MbrContact>()
            .HasKey(mc => new { mc.CAPID, mc.Type, mc.Priority });

        modelBuilder.Entity<OFlight>()
            .HasKey(of => new { of.CAPID, of.FltDate });
    }
}