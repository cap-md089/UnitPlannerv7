// CalendarEvent.cs: Describes the events that go on the different calendars
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
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitPlanner.Apis.Main.Models;

public enum EventStatus
{
    Draft,
    Tentative,
    Confirmed,
    Complete,
    Cancelled,
    InformationOnly
}

public enum AttendanceViewOptions
{
    PrivateToEventAdmins,
    PrivateToAccount,
    PublicToMembers
}

[Table("Events")]
public abstract class CalendarEvent
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Member Author { get; set; } = null!;

    public Calendar Calendar { get; set; } = null!;

    public DateTime PickupDateTime { get; set; }

    public DateTime MeetDateTime { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Created { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Modified { get; set; }

    public ICollection<AttendanceRecord> Attendance { get; set; } = null!;

    public ICollection<PointOfContact> PointsOfContact { get; set; } = null!;
}

[Table("EventsRegular")]
public class RegularCalendarEvent : CalendarEvent
{
    public EventDetails Details { get; set; } = null!;

    public ICollection<DebriefItem> Debriefs { get; set; } = null!;

    public ICollection<Equipment> RequiredEquipment { get; set; } = null!;

    public ICollection<CustomAttendanceField> CustomAttendanceFields { get; set; } = null!;

    public ICollection<AttendanceApprovalRequirement> AttendanceApprovalRequirements { get; set; } = null!;
}

[Table("EventsLinked")]
public class LinkedEvent : CalendarEvent
{
    public RegularCalendarEvent Parent { get; set; } = null!;

    public LinkedEventOverrideProperties OverridenProperties { get; set; } = null!;

    public ICollection<Equipment> ExtraRequiredEquipment { get; set; } = null!;

    public ICollection<CustomAttendanceField> ExtraCustomAttendanceFields { get; set; } = null!;
}

[Table("PointsOfContact")]
public class PointOfContact
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Position { get; set; } = null!;

    public bool ReceiveEventUpdates { get; set; }

    public bool ReceiveRoster { get; set; }

    public bool ReceiveSignUpUpdates { get; set; }

    public bool DisplayPublicly { get; set; }
}

public class InternalPointOfContact : PointOfContact
{
    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;
}

public class ExternalPointOfContact : PointOfContact
{
    public string Name { get; set; } = null!;
}

public class DebriefItem
{
    public Guid DebriefItemId { get; set; }

    public Member Member { get; set; } = null!;

    public DateTime Submitted { get; set; }

    public RegularCalendarEvent SourceEvent { get; set; } = null!;

    public bool DisplayToPublic { get; set; }

    public string DebriefText { get; set; } = null!;
}

public class EquipmentAssignment
{
    public Equipment Equipment { get; set; } = null!;

    public CalendarEvent Event { get; set; } = null!;
}

[Owned]
public class EventDetails
{
    public string Name { get; set; } = null!;

    public string Subtitle { get; set; } = null!;

    public string MeetLocation { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public string EventLocation { get; set; } = null!;

    public DateTime EndDateTime { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string? TransportationDescription { get; set; }

    public UniformSelection Uniform { get; set; } = null!;

    public int DesiredNumberOfParticipants { get; set; }

    public RegistrationDeadlineInformation? RegistrationDeadline { get; set; }

    public ParticipationFeeInformation? ParticipationFee { get; set; }

    public EmailSignupInformation? EmailInformation { get; set; }

    public MealsInformation Meals { get; set; } = null!;

    public LodgingArrangementInformation LodgingArrangements { get; set; } = null!;

    public ActivityDescription ActivityDescription { get; set; } = null!;

    public string HighAdventureDescription { get; set; } = null!;

    public string ExternalEventWebsite { get; set; } = null!;

    public RequiredForms RequiredForms { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string MemberDescription { get; set; } = null!;

    // If not null: Deny sign up attempts with the message provided
    public string? SignupDenyMessage { get; set; }

    public bool ShowOnMainPageAsUpcoming { get; set; }

    public bool Complete { get; set; }

    public string AdministrationComments { get; set; } = null!;

    public EventStatus Status { get; set; }

    public bool AllowShifts { get; set; }

    public TeamInformation? TeamInformation { get; set; }

    public AttendanceViewOptions AttendanceViewOptions { get; set; }

}

public abstract class CustomAttendanceField
{
    public Guid EventId { get; set; }
    public CalendarEvent Event { get; set; } = null!;

    public string Title { get; set; } = null!;

    public bool DisplayToMember { get; set; }

    public bool AllowMemberToModify { get; set; }
}

public class CustomAttendanceFieldCheckbox : CustomAttendanceField
{
    public bool PreFill { get; set; }
}

public class CustomAttendanceFieldDate : CustomAttendanceField
{
    public DateTime PreFill { get; set; }
}

public class CustomAttendanceFieldNumber : CustomAttendanceField
{
    public double PreFill { get; set; }
}

public class CustomAttendanceFieldText : CustomAttendanceField
{
    public string PreFill { get; set; } = null!;
}

public class CustomAttendanceFieldFiles : CustomAttendanceField
{
}

[Owned]
public class UniformSelection
{
    public bool DressBlueA { get; set; }

    public bool DressBlueB { get; set; }

    public bool AirmanBattleUniform { get; set; }

    public bool PTGear { get; set; }

    public bool PoloShirts { get; set; }

    public bool BlueUtilities { get; set; }

    public bool Civies { get; set; }

    public bool FlightSuit { get; set; }

    public bool NotApplicable { get; set; }
}

[Owned]
public class RegistrationDeadlineInformation
{
    public DateTime RegistrationDeadline { get; set; }

    public string DeadlineInformation { get; set; } = null!;
}

[Owned]
public class ParticipationFeeInformation
{
    public double FeeDue { get; set; }

    public DateTime FeeDeadline { get; set; }
}

[Owned]
public class EmailSignupInformation
{
    public string Body { get; set; } = null!;
}

[Owned]
public class MealsInformation
{
    public bool NoMeals { get; set; }

    public bool MealsProvided { get; set; }

    public bool BringOwnFood { get; set; }

    public bool BringMoney { get; set; }

    public string? Other { get; set; }
}

[Owned]
public class LodgingArrangementInformation
{
    public bool HotelOrIndividualRoom { get; set; }

    public bool OpenBayBuilding { get; set; }

    public bool LargeTent { get; set; }

    public bool IndividualTent { get; set; }

    public string? Other { get; set; }
}

[Owned]
public class ActivityDescription
{
    public bool SquadronMeeting { get; set; }

    public bool ClassroomTourLight { get; set; }

    public bool Backcountry { get; set; }

    public bool Flying { get; set; }

    public bool PhysicallyRigorous { get; set; }
}

[Owned]
public class RequiredForms
{
    public bool CAPIDCard { get; set; }

    public bool CAPF31 { get; set; }

    public bool CAPF6080 { get; set; }

    public bool CAPF101 { get; set; }

    public bool CAPF160 { get; set; }

    public bool CAPF161 { get; set; }

    public bool CAPF163 { get; set; }
}

[Owned]
public class TeamInformation
{
    public Team Team { get; set; } = null!;

    public bool LimitSignupsToTeamMembers { get; set; }
}

[Owned]
public class LinkedEventOverrideProperties
{
    public string? Name { get; set; }

    public string? Subtitle { get; set; }

    public string? MeetLocation { get; set; }

    public DateTime? StartDateTime { get; set; }

    public string? EventLocation { get; set; }

    public DateTime? EndDateTime { get; set; }

    public string? PickupLocation { get; set; }

    public string? TransportationDescription { get; set; }

    public UniformSelection? Uniform { get; set; }

    public int? DesiredNumberOfParticipants { get; set; }

    public RegistrationDeadlineInformation? RegistrationDeadline { get; set; }

    public ParticipationFeeInformation? ParticipationFee { get; set; }

    public EmailSignupInformation? EmailInformation { get; set; }

    public MealsInformation? Meals { get; set; }

    public LodgingArrangementInformation? LodgingArrangements { get; set; }

    public ActivityDescription? ActivityDescription { get; set; }

    public string? HighAdventureDescription { get; set; }

    public string? ExternalEventWebsite { get; set; }

    public RequiredForms? RequiredForms { get; set; }

    public string? Description { get; set; }

    public string? MemberDescription { get; set; }

    public string? SignupDenyMessage { get; set; }

    public bool? ShowOnMainPageAsUpcoming { get; set; }

    public bool? Complete { get; set; }

    public string? AdministrationComments { get; set; }

    public EventStatus? Status { get; set; }

    public bool? AllowShifts { get; set; }

    public TeamInformation? TeamInformation { get; set; }

    public AttendanceViewOptions? AttendanceViewOptions { get; set; }
}