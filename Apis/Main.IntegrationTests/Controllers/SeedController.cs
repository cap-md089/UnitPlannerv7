// SeedController: Provides an interface for integration tests to seed the database
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

using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using k8s;

using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;

namespace UnitPlanner.Apis.Main.IntegrationTests.Controllers;

[ApiController]
[Route("/api/seed/")]
public class SeedController : ControllerBase
{
    private readonly UnitPlannerDbContext _context;

    public SeedController(UnitPlannerDbContext context) =>
        (_context) = (context);

    [HttpGet]
    [Route("current")]
    public async Task<CurrentDatabaseState> GetDbState()
    {
        var (
            AccountsTask, CalendarsTask, EquipmentTask,
            EventsTask, MembersTask, NotificationsTask, TeamsTask
        ) = (
            _context.Accounts
                .Include(a => a.Domains)
                .Include(a => a.ExtraAccountMembership)
                .Include(a => (a as CAPSquadron)!.Organizations)
                .Include(a => (a as CAPGroup)!.Organizations)
                .Include(a => (a as CAPWing)!.Organizations)
                .ToListAsync(),
            _context.Calendars.ToListAsync(),
            _context.Events
                .Include(e => e.Attendance)
                    .ThenInclude(a => a.Approvals)
                .Include(e => e.Attendance)
                    .ThenInclude(a => a.CustomAttendanceFieldValues)
                    .ThenInclude(v => (v as CustomAttendanceFieldFilesValue)!.Submissions)
                .Include(e => e.PointsOfContact)
                .Include(e => (e as RegularCalendarEvent)!.Debriefs)
                .Include(e => (e as RegularCalendarEvent)!.RequiredEquipment)
                .Include(e => (e as RegularCalendarEvent)!.CustomAttendanceFields)
                .Include(e => (e as RegularCalendarEvent)!.AttendanceApprovalRequirements)
                .Include(e => (e as LinkedEvent)!.ExtraRequiredEquipment)
                .Include(e => (e as LinkedEvent)!.ExtraCustomAttendanceFields)
                .ToListAsync(),
            _context.Equipment.ToListAsync(),
            _context.Members
                .ToListAsync(),
            _context.Notifications
                .Include(n => n.NotificationData)
                .ToListAsync(),
            _context.Teams
                .Include(t => t.TeamMembers)
                .ToListAsync()
        );
        await Task.WhenAll(
            AccountsTask, CalendarsTask,
            EquipmentTask, EventsTask, MembersTask, NotificationsTask, TeamsTask
        );
        var (
            Accounts, Calendars, CalendarEvents,
            Equipment, Members, Notifications, Teams
        ) = (
            AccountsTask.Result,
            CalendarsTask.Result, EquipmentTask.Result, EventsTask.Result,
            MembersTask.Result, NotificationsTask.Result, TeamsTask.Result
        );

        return new CurrentDatabaseState(
            accounts: Accounts,
            calendars: Calendars,
            equipment: Equipment,
            calendarEvents: CalendarEvents,
            members: Members,
            notifications: Notifications,
            teams: Teams
        );
    }

    [HttpPost]
    [Route("clear")]
    public async Task Clear()
    {
        var tables = new List<string>()
        {
            "AccountDomain",
            "AccountOrganizationMapping",
            "Attendance",
            "AttendanceApproval",
            "AttendanceApprovalRequirement",
            "CAPActivityAccounts",
            "CAPGroups",
            "CAPSquadrons",
            "CAPWings",
            "Calendars",
            "CustomAttendanceFieldFileSubmission",
            "CustomAttendanceFieldValue",
            "CustomAttendanceField",
            "DebriefItem",
            "ExtraAccountMembership",
            "NHQ_CadetAchv",
            "NHQ_CadetAchvAprs",
            "NHQ_CadetActivities",
            "NHQ_CadetDutyPosition",
            "NHQ_CadetHFZInformation",
            "NHQ_CdtAchvEnum",
            "NHQ_DutyPosition",
            "NHQ_MbrContact",
            "NHQ_Member",
            "NHQ_OFlight",
            "NHQ_Organization",
            "NotificationData",
            "Notifications",
            "PointsOfContact",
            "Signature",
            "TeamMembership",
            "Teams",
            "Accounts",
            "Members",
            "EventsLinked",
            "EventsRegular",
            "Events",
        };

        try
        {
            foreach (var table in tables)
            {
                await _context.Database.ExecuteSqlRawAsync($"DELETE FROM {table};");
            }
        }
        catch
        {
            // Ignore; database hasn't been set up, and will be set up by the actual main process shortly
        }

        using var cluster = new Kubernetes(KubernetesClientConfiguration.InClusterConfig());
        var ingresses = await cluster.ListNamespacedIngressAsync("unitplannerv7");
        foreach (var ingress in ingresses.Items)
        {
            if (ingress.Metadata.Name.StartsWith("account-ingress-"))
            {
                await cluster.DeleteNamespacedIngressAsync(ingress.Metadata.Name, "unitplannerv7");
            }
        }
    }

    [HttpPost]
    [Route("write")]
    public async Task Write(SetDatabaseState state, string db)
    {
        await Clear();

        async Task AddRange<T>(DbSet<T> db, IEnumerable<IEnumerable<T>?> values)
            where T : class
        {
            foreach (var value in values)
            {
                if (value is not null)
                    await db.AddRangeAsync(value);
            }
        }

        await AddRange(
            _context.Accounts,
            new[] { state.CAPSquadrons?.Select(sqd => sqd as Account), state.CAPGroups, state.CAPWings, state.CAPActivities }
        );
        await AddRange(_context.Calendars, new[] { state.Calendars });
        await AddRange(_context.Events, new[] { state.Events });
        await AddRange(_context.Equipment, new[] { state.Equipment });
        await AddRange(_context.Members, new[] { state.Members });
        await AddRange(_context.Notifications, new[] { state.Notifications });
        await AddRange(_context.Teams, new[] { state.Teams });
    }
}

public class CurrentDatabaseState
{
    public IEnumerable<Account> Accounts { get; set; }
    public IEnumerable<Calendar> Calendars { get; set; }
    public IEnumerable<Equipment> Equipment { get; set; }
    public IEnumerable<CalendarEvent> CalendarEvents { get; set; }
    public IEnumerable<Member> Members { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    public IEnumerable<Team> Teams { get; set; }

    public CurrentDatabaseState(
        IEnumerable<Account> accounts,
        IEnumerable<Calendar> calendars,
        IEnumerable<Equipment> equipment,
        IEnumerable<CalendarEvent> calendarEvents,
        IEnumerable<Member> members,
        IEnumerable<Notification> notifications,
        IEnumerable<Team> teams
    )
    {
        Accounts = accounts;
        Calendars = calendars;
        Equipment = equipment;
        CalendarEvents = calendarEvents;
        Members = members;
        Notifications = notifications;
        Teams = teams;
    }
}

public class SetDatabaseState
{
    public IEnumerable<Member>? Members { get; set; }
    public IEnumerable<CAPSquadron>? CAPSquadrons { get; set; }
    public IEnumerable<CAPGroup>? CAPGroups { get; set; }
    public IEnumerable<CAPWing>? CAPWings { get; set; }
    public IEnumerable<CAPActivity>? CAPActivities { get; set; }
    public IEnumerable<Calendar>? Calendars { get; set; }
    public IEnumerable<CalendarEvent>? Events { get; set; }
    public IEnumerable<Equipment>? Equipment { get; set; }
    public IEnumerable<Notification>? Notifications { get; set; }
    public IEnumerable<NotificationData>? NotificationData { get; set; }
    public IEnumerable<Team>? Teams { get; set; }
}