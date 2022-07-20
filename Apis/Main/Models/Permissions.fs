module UnitPlanner.Apis.Main.Models.Permissions

open System

open UnitPlanner.Common.Util

open UnitPlanner.Apis.Main.Models.Account
open UnitPlanner.Apis.Main.Models.Member

type PermissionAccountType =
    | All
    | AccountSpecific of AccountType nel

type FlightAssignP =
    | Yes
    | No

type MusterSheetP =
    | Yes
    | No

type PTSheetP =
    | Yes
    | No

type PromotionManagementP =
    | Yes
    | No

type AssignTasksP =
    | Yes
    | No

type MemberPermissions =
    { MemberId: MemberReference
      AccountId: AccountId

     }
