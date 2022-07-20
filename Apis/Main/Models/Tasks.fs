module UnitPlanner.Apis.Main.Models.Tasks

open System

type TaskRecipientStatus =
    | Done of comments: string
    | Incomplete

type TaskRecipient =
    { TaskId: Guid
      MemberId: Guid

      Status: TaskRecipientStatus }

type AssignedTask =
    { TaskId: Guid

      Name: string
      Description: string

      AssignmentDate: DateTimeOffset
      DueDate: DateTimeOffset }
