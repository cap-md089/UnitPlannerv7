module UnitPlanner.Apis.Main.Models.Account

type Timezone =
    | New_York
    | Chicago
    | Denver
    | Los_Angeles
    | Arizona
    | Anchorage
    | Hawaii
    | Puerto_Rico

type AccountSettings =
    { WebsiteName: string
      ShowUpcomingEventCount: uint
      Timezone: Timezone
      FaviconFileId: string option
      FlightNames: string list }

and CAPActivityAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      HostId: string
      Domains: string list }

and CAPVolunteerUniversityAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings }

and CAPNationalAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      Organizations: int list }

and CAPRegionAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      Organizations: int list }

and CAPWingAccount<'c> =
    { Id: string
      BaseUrl: string
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      Organizations: int list }

and CAPGroupAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      Organizations: int list }

and CAPSquadronAccount<'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: string list
      Calendars: 'c list
      AccountSettings: AccountSettings
      Organizations: int list }

and Account_<'c> =
    | CAPActivity of CAPActivityAccount<'c>
    | CAPVolunteerUniversity of CAPVolunteerUniversityAccount<'c>
    | CAPNational of CAPNationalAccount<'c>
    | CAPRegion of CAPRegionAccount<'c>
    | CAPWing of CAPWingAccount<'c>
    | CAPGroup of CAPGroupAccount<'c>
    | CAPSquadron of CAPSquadronAccount<'c>
