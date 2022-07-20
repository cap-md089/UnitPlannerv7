module UnitPlanner.Apis.Main.Models.Notifications

open System

open UnitPlanner.Apis.Main.Models.CalendarEvents
open UnitPlanner.Apis.Main.Models.Member

type GlobalNotificationTarget =
    | Everyone
    | Account of accountId: string

type NotificationData = EventUpdate of eventId: Guid * newData: ModifiedEventDetails

type AdminNotification =
    { Id: Guid
      AccountId: string
      Created: DateTimeOffset
      Read: bool
      NotificationData: NotificationData }

type MemberNotification =
    { Id: Guid
      MemberId: MemberReference
      Created: DateTimeOffset
      Read: bool
      NotificationData: NotificationData }

type AccountNotification = { Id: Guid }
