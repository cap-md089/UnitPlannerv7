module UnitPlanner.Apis.Main.DbModels.Member

open System
open System.Collections.Generic

open UnitPlanner.Apis.Main.DbModels.Account

[<CLIMutable>]
type Member =
    { MemberId: Guid
      ExtraAccountMembership: ICollection<ExtraAccountMembership<Member, uint32>> }
