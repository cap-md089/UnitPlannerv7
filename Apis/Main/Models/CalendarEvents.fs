module UnitPlanner.Apis.Main.Models.CalendarEvents

open System

open UnitPlanner.Common.Util

open UnitPlanner.Apis.Main.Models.Account
open UnitPlanner.Apis.Main.Models.Member
open UnitPlanner.Apis.Main.Models.Files

type CalendarId = CalendarId of Guid
type EventId = EventId of Guid
type DebriefId = DebriefId of Guid
type ResourceId = ResourceId of Guid

type ExactDateAndTime =
    { DateTime: DateTimeOffset
      TimeZone: TimeZoneInfo }

type DayOfWeek =
    | Sunday
    | Monday
    | Tuesday
    | Wednesday
    | Thursday
    | Friday
    | Saturday

type MonthOfYear =
    | January
    | February
    | March
    | April
    | May
    | June
    | July
    | August
    | September
    | October
    | November
    | December

type WeekSelection =
    | First
    | Second
    | Third
    | Fourth
    | Fifth
    | Last

type EventStatus =
    | Draft
    | Tentative
    | Confirmed
    | Complete
    | Cancelled
    | InformationOnly

type AttendanceViewOptions =
    | PrivateToEventAdmins
    | PrivateToAccount
    | PrivateToMembers

type AttendanceStatus =
    | CommittedAttended
    | NoShow
    | RescindedCommitment
    | NoPlans
    | Processing

type AttendanceApprovalStatus =
    | Approved
    | Denied

type AttendanceApprovalLevel =
    | Squadron
    | Group
    | Wing
    | EventOrganizer

type SelectionMethod =
    | Radio
    | Select
    | Checkbox

type ItemResourceType =
    | Room
    | Vehicle
    | Other

type CustomAttendanceFieldCheckbox =
    { EventId: EventId
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: bool }

type CustomAttendanceFieldDate =
    { EventId: EventId
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: ExactDateAndTime }

type CustomAttendanceFieldNumber =
    { EventId: EventId
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: double }

type CustomAttendanceFieldSelect =
    { EventId: EventId
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      SelectionMethod: SelectionMethod
      AllowedValues: string list }

type CustomAttendanceFieldText =
    { EventId: EventId
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: string }

type CustomAttendanceFieldFiles =
    { Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool }

type CustomAttendanceField =
    | CustomAttendanceFieldCheckbox of CustomAttendanceFieldCheckbox
    | CustomAttendanceFieldDate of CustomAttendanceFieldDate
    | CustomAttendanceFieldNumber of CustomAttendanceFieldNumber
    | CustomAttendanceFieldSelect of CustomAttendanceFieldSelect
    | CustomAttendanceFieldText of CustomAttendanceFieldText
    | CustomAttendanceFieldFiles of CustomAttendanceFieldFiles

type FinishedAttendanceApproval =
    { SignOffDate: ExactDateAndTime
      Status: AttendanceApprovalStatus
      Comment: string
      Level: AttendanceApprovalLevel
      ApproverId: MemberReference
      SignedFormId: FileId }

type PendingAttendanceApproval =
    { Level: AttendanceApprovalLevel
      UploadedFormId: FileId option }

type CustomAttendanceFieldValueType =
    | CustomAttendanceFieldValueCheckbox of bool
    | CustomAttendanceFieldValueDate of ExactDateAndTime
    | CustomAttendanceFieldValueNumber of double
    | CustomAttendanceFieldValueSelect of string
    | CustomAttendanceFieldValueText of string
    | CustomAttendanceFieldValueFiles of FileId list

type CustomAttendanceFieldValue =
    { Title: string
      Value: CustomAttendanceFieldValueType }

type AttendanceRecordShift =
    | TimeSpan of startTime: ExactDateAndTime * shiftLength: TimeSpan
    | ScheduleIndex of int

type AttendanceRecordShiftSelection =
    | FullTime
    | PartTime of AttendanceRecordShift nel

type AttendanceRecord =
    { EventId: EventId
      MemberId: MemberReference

      Comments: string
      Status: AttendanceStatus
      PlanToUseProvideTransportation: bool
      Created: ExactDateAndTime
      Modified: ExactDateAndTime
      SummaryEmailSent: bool
      ShiftTimes: AttendanceRecordShiftSelection }

type UnitItemResource =
    { Id: Guid
      Name: string
      ItemResourceType: ItemResourceType }

type EventResourceAcquisition =
    | EventResource of string
    | UnitResource of UnitItemResource

type Calendar =
    { Id: CalendarId
      Name: string
      Color: string }

type InternalPointOfContact =
    { Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool

      MemberId: MemberReference
      MemberName: string }

type ExternalPointOfContact =
    { Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool
      Name: string }

type PointOfContact =
    | Internal of InternalPointOfContact
    | External of ExternalPointOfContact

type DebriefItem =
    { DebriefId: DebriefId
      MemberId: MemberReference
      SourceEvent: EventId
      Submitted: ExactDateAndTime
      DisplayToPublic: bool
      DebriefText: string }

type EventDetails = { Name: string; Subtitle: string }

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

type RegularCalendarEvent =
    { EventId: EventId

      MeetDateTime: ExactDateAndTime
      StartDateTime: ExactDateAndTime
      EndDateTime: ExactDateAndTime
      PickupDateTime: ExactDateAndTime

      SourceRecurringEvent: EventId option

      Details: EventDetails }

type SubEventDetails =
    { EventId: EventId
      Details: EventDetails
      StartTimeOffset: ExactDateAndTime
      Length: TimeSpan }

type CompositeCalendarEvent =
    { EventId: EventId
      MeetDateTime: ExactDateAndTime
      StartDateTime: ExactDateAndTime
      EndDateTime: ExactDateAndTime
      PickupDateTime: ExactDateAndTime
      SubEvents: SubEventDetails list }

type ModifiedEventDetails =
    { Name: string option
      Subtitle: string option }

type LinkedCalendarEvent =
    { EventId: EventId

      MeetDateTime: ExactDateAndTime
      StartDateTime: ExactDateAndTime
      EndDateTime: ExactDateAndTime
      PickupDateTime: ExactDateAndTime

      SourceEventId: EventId
      Details: EventDetails
      ModifiedEventDetails: ModifiedEventDetails }

type RecurrenceRule =
    | Daily of repeatEvery: int
    | Weekly of repeatEvery: int * daySelection: DayOfWeek nel
    | MonthlyOnDay of repeatEvery: int * dayOfMonth: int
    | MonthlyOnDayOfWeek of repeatEvery: int * WeekSelection * DayOfWeek
    | YearlyOnDay of repeatEvery: int * MonthOfYear * dayOfMonth: int
    | YearlyOnDayOfWeek of repeatEvery: int * WeekSelection * DayOfWeek * MonthOfYear

type RecurringCalendarEvent =
    { EventId: EventId

      Recurrence: RecurrenceRule
      StartDate: DateOnly
      EndDate: DateOnly option

      MeetTime: TimeOnly
      StartTime: TimeOnly
      EndTime: TimeOnly
      PickupTime: TimeOnly

      Details: EventDetails }

type Account = AccountC<Calendar>
