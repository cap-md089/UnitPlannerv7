module UnitPlanner.Apis.Main.DbModels.CalendarEvents

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

open UnitPlanner.Apis.Main.DbModels.Account

type EventStatus =
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
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: bool }

and [<CLIMutable>] CustomAttendanceFieldDate =
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: DateTime }

and [<CLIMutable>] CustomAttendanceFieldNumber =
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: double }

and [<CLIMutable>] CustomAttendanceFieldSelect =
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      SelectionMethod: SelectionMethod
      (* JSON serialized string array *)
      AllowedValues: string }

and [<CLIMutable>] CustomAttendanceFieldText =
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool
      PreFill: string }

and [<CLIMutable>] CustomAttendanceFieldFiles =
    { Id: Guid
      AccountId: string
      EventId: Guid
      Title: string
      DisplayToMember: bool
      AllowMemberToModify: bool }

and [<CLIMutable>] CustomAttendanceFieldModel =
    { Id: Guid
      EventId: Guid

      Checkbox: CustomAttendanceFieldCheckbox option
      Date: CustomAttendanceFieldDate option
      Number: CustomAttendanceFieldNumber option
      Select: CustomAttendanceFieldSelectValue option
      Text: CustomAttendanceFieldText option
      Files: CustomAttendanceFieldFiles option }

and [<CLIMutable>] FinishedAttendanceApproval<'m> =
    { Id: Guid

      ApprovalMember: 'm
      SignOffDate: DateTime

      Status: AttendanceApprovalStatus

      Comment: string }

and [<CLIMutable>] PendingAttendanceApproval =
    { Id: Guid

      Level: AttendanceApprovalLevel
      UploadedFormId: string option }

and [<CLIMutable>] AttendanceApprovalModel<'m> =
    { AttendanceRecordMemberId: Guid
      AttendanceRecordEventId: Guid
      AttendanceRecord: AttendanceRecord<'m>

      RequirementId: Guid
      Requirement: AttendanceApprovalRequirement<'m>

      ApprovalId: Guid
      [<ForeignKey("ApprovalId")>]
      FinishedAttendanceApproval: FinishedAttendanceApproval<'m> option
      [<ForeignKey("ApprovalId")>]
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

      Value: DateTime }

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

      (* JSON string; check domain model for actual type *)
      ShiftTimes: string option }

and [<CLIMutable>] AttendanceApprovalRequirement<'m> =
    { [<Key>]
      Id: Guid

      ApprovalLevel: AttendanceApprovalLevel
      CalendarEvent: CalendarEventModel_<'m> }

and [<CLIMutable>] ResourceAcquisitionModel =
    { EventId: Guid
      Id: Guid

      [<ForeignKey("Id")>]
      EventResource: EventResource option
      [<ForeignKey("Id")>]
      UnitItem: UnitItemResource option }

and [<CLIMutable>] EventResource = { Id: Guid; Name: string }

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
      Member: 'm
      Submitted: DateTime
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
