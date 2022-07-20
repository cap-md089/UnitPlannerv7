module UnitPlanner.Apis.Main.Models.Account

open System

open UnitPlanner.Apis.Main.Models.Files

type AccountId = AccountId of string

type AccountType =
    | CAPActivity
    | CAPVolunteerUniversity
    | CAPNational
    | CAPRegion
    | CAPWing
    | CAPGroup
    | CAPSquadron

type AccountSettings =
    { WebsiteName: string
      ShowUpcomingEventCount: uint
      Timezone: TimeZoneInfo
      FaviconFileId: FileId option
      FlightNames: string list }

type CAPActivityAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      HostId: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings }

type CAPVolunteerUniversityAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings }

type CAPNationalAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      OrganizationIds: int list }

type CAPRegionAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      NationalId: string
      OrganizationIds: int list }

type CAPWingAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      RegionId: string
      OrganizationIds: int list }

type CAPGroupAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      WingId: string
      OrganizationIds: int list }

type CAPSquadronAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      GroupId: string
      OrganizationIds: int list }

type AccountC<'c> =
    | CAPActivity of CAPActivityAccount<'c>
    | CAPVolunteerUniversity of CAPVolunteerUniversityAccount<'c>
    | CAPNational of CAPNationalAccount<'c>
    | CAPRegion of CAPRegionAccount<'c>
    | CAPWing of CAPWingAccount<'c>
    | CAPGroup of CAPGroupAccount<'c>
    | CAPSquadron of CAPSquadronAccount<'c>
