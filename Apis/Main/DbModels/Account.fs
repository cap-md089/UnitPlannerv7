module UnitPlanner.Apis.Main.DbModels.Account

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema

open Microsoft.EntityFrameworkCore

// open UnitPlanner.Apis.Main.DbModels.Notifications

type Timezone =
    | New_York = 0
    | Chicago = 1
    | Denver = 2
    | Los_Angeles = 3
    | Arizona = 4
    | Anchorage = 5
    | Hawaii = 6
    | Puerto_Rico = 7

[<CLIMutable>]
type AccountOrganizationMapping<'m, 'c> =
    { AccountId: string
      Account: AccountModel<'m, 'c>
      ORGID: int }

and [<CLIMutable; Owned>] AccountSettings =
    { WebsiteName: string
      ShowUpcomingEventCount: uint
      Timezone: Timezone
      FaviconFileId: string option
      FlightNames: string }

and [<CLIMutable>] ExtraAccountMembership<'m, 'c> =
    { AccountId: string
      Account: AccountModel<'m, 'c>
      MemberId: Guid
      Member: 'm }

and [<CLIMutable>] AccountDomain<'m, 'c> =
    { Domain: string
      Account: AccountModel<'m, 'c> }

and [<CLIMutable>] CAPActivityAccount<'m, 'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: ICollection<AccountDomain<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      HostId: string
      Host: AccountModel<'m, 'c> }

and [<CLIMutable>] CAPVolunteerUniversityAccount<'m, 'c> =
    { Id: string
      BaseUrl: string
      Domains: ICollection<AccountDomain<'m, 'c>>
      Members: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      ActivityAccounts: ICollection<CAPActivityAccount<'m, 'c>> }

and [<CLIMutable>] CAPNationalAccount<'m, 'c> =
    { Id: string
      BaseUrl: string
      Domains: ICollection<AccountDomain<'m, 'c>>
      ExtraMembers: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      Organizations: ICollection<AccountOrganizationMapping<'m, 'c>>
      ActivityAccounts: ICollection<CAPActivityAccount<'m, 'c>>
      Regions: ICollection<CAPRegionAccount<'m, 'c>> }

and [<CLIMutable>] CAPRegionAccount<'m, 'c> =
    { Id: string
      BaseUrl: string
      Domains: ICollection<AccountDomain<'m, 'c>>
      ExtraMembers: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      Organizations: ICollection<AccountOrganizationMapping<'m, 'c>>
      ActivityAccounts: ICollection<CAPActivityAccount<'m, 'c>>
      National: CAPNationalAccount<'m, 'c>
      Wings: ICollection<CAPWingAccount<'m, 'c>> }

and [<CLIMutable>] CAPWingAccount<'m, 'c> =
    { Id: string
      BaseUrl: string
      Domains: ICollection<AccountDomain<'m, 'c>>
      ExtraMembers: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      Organizations: ICollection<AccountOrganizationMapping<'m, 'c>>
      ActivityAccounts: ICollection<CAPActivityAccount<'m, 'c>>
      Region: CAPRegionAccount<'m, 'c>
      Groups: ICollection<CAPGroupAccount<'m, 'c>> }

and [<CLIMutable>] CAPGroupAccount<'m, 'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: ICollection<AccountDomain<'m, 'c>>
      ExtraMembers: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      Organizations: ICollection<AccountOrganizationMapping<'m, 'c>>
      ActivityAccounts: ICollection<CAPActivityAccount<'m, 'c>>
      Wing: CAPWingAccount<'m, 'c>
      Squadrons: ICollection<CAPSquadronAccount<'m, 'c>> }

and [<CLIMutable>] CAPSquadronAccount<'m, 'c> =
    { Id: string
      OverrideBaseUrl: string option
      Domains: ICollection<AccountDomain<'m, 'c>>
      ExtraMembers: ICollection<ExtraAccountMembership<'m, 'c>>
      Calendars: ICollection<'c>
      AccountSettings: AccountSettings
      Organizations: ICollection<AccountOrganizationMapping<'m, 'c>>
      Group: CAPGroupAccount<'m, 'c> }

and [<CLIMutable>] AccountModel<'m, 'c> =
    { [<Key>]
      Id: string

      [<ForeignKey("Id")>]
      CAPActivity: CAPActivityAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPVolunteerUniversity: CAPVolunteerUniversityAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPNational: CAPNationalAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPRegion: CAPRegionAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPWing: CAPWingAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPGroup: CAPGroupAccount<'m, 'c> option
      [<ForeignKey("Id")>]
      CAPSquadron: CAPSquadronAccount<'m, 'c> option }
