module UnitPlanner.Apis.Main.Models.CalendarEvents

open System

open UnitPlanner.Common.Util

type EventStatus =
    | Draft
    | Tentative
    | Confirmed
    | Complete
    | Cancelled
    | InformationOnly

and AttendanceViewOptions =
    | PrivateToEventAdmins
    | PrivateToAccount
    | PrivateToMembers

and AttendanceStatus =
    | CommittedAttended
    | NoShow
    | RescindedCommitment
    | NoPlans
    | Processing

and AttendanceApprovalStatus =
    | Approved
    | Denied

and AttendanceApprovalLevel =
    | Squadron
    | Group
    | Wing
    | EventOrganizer

and SelectionMethod =
    | Radio
    | Select
    | Checkbox

and ItemResourceType =
    | Room
    | Vehicle
    | Other

and CustomAttendanceFieldCheckbox =
    { EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: bool }

and CustomAttendanceFieldDate =
    { EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: DateTime }

and CustomAttendanceFieldNumber =
    { EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: double }

and CustomAttendanceFieldSelect =
    { EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      SelectionMethod: SelectionMethod
      AllowedValues: string list }

and CustomAttendanceFieldText =
    { EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: string }

and CustomAttendanceFieldFiles =
    { Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool }

and CustomAttendanceField =
    | CustomAttendanceFieldCheckbox of CustomAttendanceFieldCheckbox
    | CustomAttendanceFieldDate of CustomAttendanceFieldDate
    | CustomAttendanceFieldNumber of CustomAttendanceFieldNumber
    | CustomAttendanceFieldSelect of CustomAttendanceFieldSelect
    | CustomAttendanceFieldText of CustomAttendanceFieldText
    | CustomAttendanceFieldFiles of CustomAttendanceFieldFiles

and FinishedAttendanceApproval =
    { SignOffDate: DateTime
      Status: AttendanceApprovalStatus
      Comment: string
      Level: AttendanceApprovalLevel
      ApproverId: Guid }

and PendingAttendanceApproval = { Level: AttendanceApprovalLevel }

and CustomAttendanceFieldValue =
    | CustomAttendanceFieldValueCheckbox of bool
    | CustomAttendanceFieldValueDate of DateTime
    | CustomAttendanceFieldValueNumber of double
    | CustomAttendanceFieldValueSelect of string
    | CustomAttendanceFieldValueText of string
    | CustomAttendanceFieldValueFiles of Guid list

and AttendanceRecordShift =
    | TimeSpan of startTime: DateTime * shiftLength: TimeSpan
    | ScheduleIndex of int

and AttendanceRecordShiftSelection =
    | FullTime
    | PartTime of AttendanceRecordShift nel

and AttendanceRecord =
    { EventId: Guid
      MemberId: Guid

      Comments: string
      Status: AttendanceStatus
      PlanToUseProvideTransportation: bool
      Created: DateTime
      Modified: DateTime
      SummaryEmailSent: bool
      ShiftTimes: AttendanceRecordShiftSelection }

and UnitItemResource =
    { Name: string
      ItemResourceType: ItemResourceType }

and EventResourceAcquisition =
    | EventResource of string
    | UnitResource of UnitItemResource

and Calendar =
    { Id: Guid
      Name: string
      Color: string }

and InternalPointOfContact =
    { Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool

      MemberId: Guid
      MemberName: string }

and ExternalPointOfContact =
    { Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool
      Name: string }

and PointOfContact =
    | Internal of InternalPointOfContact
    | External of ExternalPointOfContact

and DebriefItem =
    { DebriefId: Guid
      Submitted: DateTime
      DisplayToPublic: bool
      DebriefText: string }

and EventDetails = { Name: string; Subtitle: string }

(*

Event permutation list:

- Regular event
    - optionally has shifts
    - has details
- Composite event
    - (has) Regular event(s)
      - has details
    - has details
- Recurring event
    - (has) Regular event(s)
      - has details
    - has details
- Linked event
    - (links to) Regular event
    - (links to) Composite event
    - (links to) Recurring event
    - has details
    - has modified details

*)

and RegularCalendarEvent =
    { EventId: Guid

      MeetDateTime: DateTime
      StartDateTime: DateTime
      EndDateTime: DateTime
      PickupDateTime: DateTime

      Details: EventDetails }

and SubEventDetails =
    { EventId: Guid
      Details: EventDetails
      StartTimeOffset: DateTimeOffset
      Length: TimeSpan }

and CompositeCalendarEvent =
    { EventId: Guid
      MeetDateTime: DateTime
      StartDateTime: DateTime
      EndDateTime: DateTime
      PickupDateTime: DateTime
      SubEvents: SubEventDetails list }

and ModifiedEventDetails =
    { Name: string option
      Subtitle: string option }

and LinkedCalendarEvent =
    { EventId: Guid

      MeetDateTime: DateTime
      StartDateTime: DateTime
      EndDateTime: DateTime
      PickupDateTime: DateTime

      SourceEventId: Guid
      Details: EventDetails
      ModifiedEventDetails: ModifiedEventDetails }

and RecurringCalendarEvent =
    { EventId: Guid
      Details: EventDetails }
