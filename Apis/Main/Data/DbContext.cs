// DbContext.cs: Provides database configuration
//
// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UnitPlanner.Apis.Main.Models;

namespace UnitPlanner.Apis.Main.Data;

public class UnitPlannerDbContext : DbContext
{
    public DbSet<Models.NHQ.CadetAchv> CadetAchvs => Set<Models.NHQ.CadetAchv>();
    public DbSet<Models.NHQ.CadetAchvAprs> CadetAchvAprs => Set<Models.NHQ.CadetAchvAprs>();
    public DbSet<Models.NHQ.CadetActivities> CadetActivities => Set<Models.NHQ.CadetActivities>();
    public DbSet<Models.NHQ.CadetDutyPosition> CadetDutyPositions => Set<Models.NHQ.CadetDutyPosition>();
    public DbSet<Models.NHQ.CadetHFZInformation> CadetHFZInformation => Set<Models.NHQ.CadetHFZInformation>();
    public DbSet<Models.NHQ.CdtAchvEnum> CadetAchvEnum => Set<Models.NHQ.CdtAchvEnum>();
    public DbSet<Models.NHQ.DutyPosition> DutyPositions => Set<Models.NHQ.DutyPosition>();
    public DbSet<Models.NHQ.MbrContact> MbrContact => Set<Models.NHQ.MbrContact>();
    public DbSet<Models.NHQ.OFlight> OFlights => Set<Models.NHQ.OFlight>();
    public DbSet<Models.NHQ.Organization> Organizations => Set<Models.NHQ.Organization>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Calendar> Calendars => Set<Calendar>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<CalendarEvent> Events => Set<CalendarEvent>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Team> Teams => Set<Team>();

    public UnitPlannerDbContext(DbContextOptions<UnitPlannerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExtraAccountMembership>()
            .HasKey(m => new { m.AccountId, m.MemberId });

        modelBuilder.Entity<AttendanceRecord>(b =>
        {
            b.ToTable("Attendance");

            b.Property(a => a.Status).HasConversion<string>();

            b.HasKey(a => new { a.EventId, a.MemberId });
        });

        modelBuilder.Entity<AccountOrganizationMapping>()
            .HasKey(m => new { m.AccountId, m.ORGID });

        modelBuilder.Entity<CAPActivity>();
        modelBuilder.Entity<CAPSquadron>();
        modelBuilder.Entity<CAPGroup>();
        modelBuilder.Entity<CAPWing>();

        modelBuilder.Entity<CustomAttendanceField>(b =>
        {
            b.HasKey(f => new { f.EventId, f.Title });

            b
                .HasDiscriminator<string>("Type")
                .HasValue<CustomAttendanceFieldCheckbox>("Checkbox")
                .HasValue<CustomAttendanceFieldDate>("Date")
                .HasValue<CustomAttendanceFieldNumber>("Number")
                .HasValue<CustomAttendanceFieldText>("Text")
                .HasValue<CustomAttendanceFieldFiles>("Files");
        });

        modelBuilder.Entity<CustomAttendanceFieldValue>(b =>
        {
            b.HasKey(v => new { v.AttendanceRecordEventId, v.AttendanceRecordMemberId, v.Title });

            b
                .HasOne(v => v.AttendanceRecord)
                .WithMany(a => a.CustomAttendanceFieldValues)
                .HasForeignKey(v => new { v.AttendanceRecordEventId, v.AttendanceRecordMemberId });

            b
                .HasOne(v => v.CustomAttendanceFieldRules)
                .WithMany()
                .HasForeignKey(v => new { v.AttendanceRecordEventId, v.Title });

            b
                .HasDiscriminator<string>("Type")
                .HasValue<CustomAttendanceFieldCheckboxValue>("Checkbox")
                .HasValue<CustomAttendanceFieldDateValue>("Date")
                .HasValue<CustomAttendanceFieldNumberValue>("Number")
                .HasValue<CustomAttendanceFieldTextValue>("Text")
                .HasValue<CustomAttendanceFieldFilesValue>("Files");
        });

        modelBuilder.Entity<AttendanceApproval>(b =>
        {
            b.HasKey(a => new { a.AttendanceApprovalRequirementId, a.AttendanceRecordEventId, a.AttendanceRecordMemberId });

            b
                .HasOne(a => a.AttendanceRecord)
                .WithMany(a => a.Approvals)
                .HasForeignKey(a => new { a.AttendanceRecordEventId, a.AttendanceRecordMemberId });

            b
                .Property(a => a.ApprovalStatus)
                .HasConversion<string>();
        });

        modelBuilder.Entity<AttendanceApprovalRequirement>()
            .Property(r => r.ApprovalLevel)
            .HasConversion<string>();

        modelBuilder.Entity<RegularCalendarEvent>();
        modelBuilder.Entity<LinkedEvent>();

        modelBuilder.Entity<Equipment>()
            .HasDiscriminator<string>("Type")
            .HasValue<EventEquipment>("Event")
            .HasValue<SquadronEquipment>("Squadron");

        modelBuilder.Entity<PointOfContact>(b =>
        {
            b
                .HasDiscriminator<string>("Type")
                .HasValue<InternalPointOfContact>("Internal")
                .HasValue<ExternalPointOfContact>("External");
        });

        modelBuilder.Entity<Notification>()
            .HasDiscriminator<string>("Type")
            .HasValue<AdminNotification>("Admin")
            .HasValue<MemberNotification>("Member");

        modelBuilder.Entity<Team>()
            .Property(t => t.Visibility)
            .HasConversion<string>();

        modelBuilder.Entity<TeamMembership>()
            .HasKey(tm => new { tm.MemberId, tm.TeamId });

        modelBuilder.Entity<Models.NHQ.Member>();

        modelBuilder.Entity<Models.NHQ.CadetAchv>()
            .HasKey(ca => new { ca.CAPID, ca.CadetAchvID });

        modelBuilder.Entity<Models.NHQ.CadetAchvAprs>()
            .HasKey(ca => new { ca.CAPID, ca.CadetAchvID });

        modelBuilder.Entity<Models.NHQ.CadetActivities>()
            .HasKey(ca => new { ca.CAPID, ca.Completed });

        modelBuilder.Entity<Models.NHQ.CadetDutyPosition>()
            .HasKey(dp => new { dp.CAPID, dp.Duty, dp.ORGID, dp.Asst });

        modelBuilder.Entity<Models.NHQ.DutyPosition>()
            .HasKey(dp => new { dp.CAPID, dp.Duty, dp.ORGID, dp.Asst });

        modelBuilder.Entity<Models.NHQ.MbrContact>()
            .HasKey(mc => new { mc.CAPID, mc.Type, mc.Priority });

        modelBuilder.Entity<Models.NHQ.OFlight>()
            .HasKey(of => new { of.CAPID, of.FltDate });
    }
}