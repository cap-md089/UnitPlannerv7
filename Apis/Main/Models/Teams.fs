module UnitPlanner.Apis.Main.Models.Teams

open System

open UnitPlanner.Apis.Main.Models.Member

type TeamVisibility =
    | Public
    | Protected
    | Private

type RoleStatus =
    | Completed of DateTimeOffset
    | Current

type TeamMember =
    { TeamId: Guid
      MemberId: MemberReference
      Role: string
      IsLeadershipRole: bool
      AssignedRole: DateTimeOffset
      FinishedRole: RoleStatus }

type Team =
    { Id: Guid
      Name: string
      Description: string
      Visibility: TeamVisibility }
