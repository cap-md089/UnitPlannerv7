module UnitPlanner.Apis.Main.Models.NHQ.Organization

open System

type Organization =
    { ORGID: int
      Region: string
      Wing: string
      Unit: string
      NextLevel: int option
      Name: string
      Type: string
      DateChartered: DateTime
      Status: string
      Scope: string
      UsrID: string
      DateMod: DateTime
      FirstUsr: string
      DateCreated: DateTime
      DateReceived: DateTime
      OrgNotes: string }
