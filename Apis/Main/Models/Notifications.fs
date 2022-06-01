module UnitPlanner.Apis.Main.Models.Notifications

open System

open System.ComponentModel.DataAnnotations.Schema

type AdminNotification<'a> =
    { Id: Guid
      [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>]
      Created: DateTime
      Read: bool
      NotificationData: string
      Account: 'a }

type MemberNotification<'m> =
    { Id: Guid
      [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>]
      Created: DateTime
      Read: bool
      NotificationData: string
      Member: 'm }
