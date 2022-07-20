module UnitPlanner.Apis.Main.DbModels.CalendarEvents

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

open UnitPlanner.Apis.Main.DbModels.Account

[<CLIMutable; Owned>]
type ExactDateAndTime =
    { DateTime: DateTimeOffset
      TimeZone: string }

and DayOfWeek =
    | Sunday = 0
    | Monday = 1
    | Tuesday = 2
    | Wednesday = 3
    | Thursday = 4
    | Friday = 5
    | Saturday = 6

and MonthOfYear =
    | January = 0
    | February = 1
    | March = 2
    | April = 3
    | May = 4
    | June = 5
    | July = 6
    | August = 7
    | September = 8
    | October = 9
    | November = 10
    | December = 11

and WeekSelection =
    | First = 0
    | Second = 1
    | Third = 2
    | Fourth = 3
    | Fifth = 4
    | Last = 5

and EventStatus =
    | Draft = 0
    | Tentative = 1
    | Confirmed = 2
    | Complete = 3
    | Cancelled = 4
    | InformationOnly = 5

and AttendanceViewOptions =
    | PrivateToEventAdmins = 0
    | PrivateToAccount = 1
    | PrivateToMembers = 2

and AttendanceStatus =
    | Committed = 0
    | NoShow = 1
    | RescindedCommitment = 2
    | NoPlans = 3

and ItemResourceType =
    | Equipment = 0
    | Vehicle = 1
    | Room = 2

and AttendanceApprovalStatus =
    | Approved = 0
    | Denied = 1

and AttendanceApprovalLevel =
    | Squadron = 0
    | Group = 1
    | Wing = 2
    | EventOrganizer = 3

and SelectionMethod =
    | Radio = 0
    | Select = 1
    | Checkbox = 2

and [<CLIMutable>] CustomAttendanceFieldCheckbox =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: bool }

and [<CLIMutable>] CustomAttendanceFieldDate =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: ExactDateAndTime }

and [<CLIMutable>] CustomAttendanceFieldNumber =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: double }

and [<CLIMutable>] CustomAttendanceFieldSelect =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      SelectionMethod: SelectionMethod
      (* JSON serialized string array *)
      AllowedValues: string }

and [<CLIMutable>] CustomAttendanceFieldText =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: string }

and [<CLIMutable>] CustomAttendanceFieldFiles =
    { AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool }

and [<CLIMutable>] CustomAttendanceFieldModel =
    { Title: string
      EventId: Guid

      Checkbox: CustomAttendanceFieldCheckbox option
      Date: CustomAttendanceFieldDate option
      Number: CustomAttendanceFieldNumber option
      Select: CustomAttendanceFieldSelectValue option
      Text: CustomAttendanceFieldText option
      Files: CustomAttendanceFieldFiles option }

and [<CLIMutable>] FinishedAttendanceApproval<'m> =
    { EventId: Guid
      MemberId: Guid
      Level: AttendanceApprovalLevel

      ApproverId: Guid
      ApprovalMember: 'm

      SignOffDate: ExactDateAndTime

      Status: AttendanceApprovalStatus

      SignedFormId: Guid

      Comment: string }

and [<CLIMutable>] PendingAttendanceApproval =
    { EventId: Guid
      MemberId: Guid
      Level: AttendanceApprovalLevel

      UploadedFormId: Guid option }

and [<CLIMutable>] AttendanceApprovalModel<'m> =
    { AttendanceRecordMemberId: Guid
      AttendanceRecordEventId: Guid
      ApprovalLevel: AttendanceApprovalLevel
      AttendanceRecord: AttendanceRecord<'m>


      RequirementId: Guid
      Requirement: AttendanceApprovalRequirement<'m>

      FinishedAttendanceApproval: FinishedAttendanceApproval<'m> option
      PendingAttendanceApproval: PendingAttendanceApproval option }

and [<CLIMutable>] CustomAttendanceFieldCheckboxValue =
    { MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldCheckbox

      Value: bool }

and [<CLIMutable>] CustomAttendanceFieldDateValue =
    { MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldDate

      Value: ExactDateAndTime }

and [<CLIMutable>] CustomAttendanceFieldNumberValue =
    { MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldNumber

      Value: double }

and [<CLIMutable>] CustomAttendanceFieldSelectValue =
    { MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldSelect

      Value: string }

and [<CLIMutable>] CustomAttendanceFieldTextValue =
    { MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldText

      Value: string }

and [<CLIMutable>] CustomAttendanceFieldFilesValue =
    { AccountId: string
      MemberId: Guid
      EventId: Guid

      FieldId: Guid
      Field: CustomAttendanceFieldFiles

      (* JSON serialized Guid array *)
      Value: string }

and [<CLIMutable>] CustomAttendanceFieldValueModel =
    { AccountId: string
      MemberId: Guid
      EventId: Guid
      FieldId: Guid

      Checkbox: CustomAttendanceFieldCheckboxValue option
      Date: CustomAttendanceFieldDateValue option
      Number: CustomAttendanceFieldNumberValue option
      Select: CustomAttendanceFieldSelectValue option
      Text: CustomAttendanceFieldTextValue option
      Files: CustomAttendanceFieldFilesValue option }

and [<CLIMutable; Owned>] AttendanceRecordShift =
    { ScheduleIndex: int option

      StartTime: ExactDateAndTime
      ShiftLength: TimeSpan }

and [<CLIMutable>] AttendanceRecord<'m> =
    { AccountId: string
      Account: AccountModel<'m, CalendarEventModel_<'m>>

      MemberId: Guid
      Member: 'm

      EventId: Guid
      Event: CalendarEventModel_<'m>

      Comments: string

      Status: AttendanceStatus

      PlanToUseProvideTransportation: bool

      Created: DateTime
      [<DatabaseGenerated(DatabaseGeneratedOption.Computed)>]
      Modified: DateTime

      Approvals: ICollection<AttendanceApprovalModel<'m>>
      CustomFieldValues: ICollection<CustomAttendanceFieldValueModel>

      SummaryEmailSent: bool

      (* ShiftTimes: ICollection<AttendanceRecordShift> *)
      (* JSON string; check domain model for actual type *)
      ShiftTimes: string }

and [<CLIMutable>] AttendanceApprovalRequirement<'m> =
    { [<Key>]
      Id: Guid

      ApprovalLevel: AttendanceApprovalLevel
      CalendarEvent: CalendarEventModel_<'m> }

and [<CLIMutable>] ResourceAcquisitionModel =
    { EventId: Guid
      Id: Guid

      [<ForeignKey("Id")>]
      EventResource: string option
      [<ForeignKey("Id")>]
      UnitItem: UnitItemResource option }

and [<CLIMutable>] UnitItemResource =
    { Id: Guid
      Name: string
      ItemResourceType: ItemResourceType }

and [<CLIMutable>] Calendar_<'m> =
    { [<Key>]
      Id: Guid

      Name: string
      Color: string

      Events: ICollection<CalendarEventModel_<'m>>
      AccountId: string
      Account: AccountModel<'m, Calendar_<'m>> }

and [<CLIMutable>] InternalPointOfContact_<'m> =
    { Id: Guid
      Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool

      MemberId: Guid
      Member: 'm }

and [<CLIMutable>] ExternalPointOfContact =
    { Id: Guid
      Email: string
      Phone: string
      Position: string
      ReceiveEventUpdates: bool
      ReceiveRoster: bool
      ReceiveSignUpUpdates: bool
      DisplayPublicly: bool

      Name: string }

and [<CLIMutable>] PointOfContactModel_<'m> =
    { [<Key>]
      Id: Guid

      [<ForeignKey("Id")>]
      ExternalPointOfContact: ExternalPointOfContact option

      [<ForeignKey("Id")>]
      InternalPointOfContact: InternalPointOfContact_<'m> option }

and [<CLIMutable>] DebriefItem_<'m> =
    { DebriefItemId: Guid
      MemberId: Guid
      Member: 'm
      Submitted: ExactDateAndTime
      SourceEventId: Guid
      SourceEvent: CalendarEventModel_<'m>
      DisplayToPublic: bool
      DebriefText: string }

and [<CLIMutable; Owned>] EventDetails = { Name: string; Subtitle: string }

and [<CLIMutable>] RegularCalendarEvent_<'m> =
    { Id: Guid
      Details: EventDetails
      Debriefs: ICollection<DebriefItem_<'m>>
      Resources: ICollection<ResourceAcquisitionModel>
      AttendanceApprovalRequirements: ICollection<AttendanceApprovalRequirement<'m>>
      CustomAttendanceFields: ICollection<int> }

and [<CLIMutable>] LinkedCalendarEvent_<'m> = { Id: Guid }

and [<CLIMutable>] RecurringCalendarEvent_ = { Id: Guid }

and [<CLIMutable>] CalendarEventModel_<'m> =
    { AccountId: string
      Account: AccountModel<'m, Calendar_<'m>>

      CalendarId: Guid
      Calendar: Calendar_<'m>

      Id: Guid

      Created: DateTime
      [<DatabaseGenerated(DatabaseGeneratedOption.Computed)>]
      Modified: DateTime

      PointsOfContact: ICollection<PointOfContactModel_<'m>>

      [<ForeignKey("Id")>]
      RegularEvent: RegularCalendarEvent_<'m> option
      [<ForeignKey("Id")>]
      LinkedEvent: LinkedCalendarEvent_<'m> option
      [<ForeignKey("Id")>]
      RecurringEvent: RecurringCalendarEvent_ option }
