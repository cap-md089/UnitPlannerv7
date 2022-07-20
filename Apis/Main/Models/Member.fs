module UnitPlanner.Apis.Main.Models.Member

open System

type CAPID = CAPID of uint
type ORGID = ORGID of uint

type CAPMemberType =
    | Cadet
    | CadetSponsor
    | Patron
    | Senior
    | FiftyYear
    | Indefinite
    | Life
    | StateLeg

type ServiceBranch =
    | Navy
    | Marines
    | AirForce
    | CoastGuard
    | Army

type MemberContact =
    { PrimaryParentEmail: string list
      PrimaryParentPhone: string list
      PrimaryCellPhone: string list
      PrimaryEmail: string list
      PrimaryHomePhone: string list
      PrimaryWorkPhone: string list
      SecondaryParentEmail: string list
      SecondaryParentPhone: string list
      SecondaryCellPhone: string list
      SecondaryEmail: string list
      SecondaryHomePhone: string list
      SecondaryWorkPhone: string list
      EmergencyParentEmail: string list
      EmergencyParentPhone: string list
      EmergencyCellPhone: string list
      EmergencyEmail: string list
      EmergencyHomePhone: string list
      EmergencyWorkPhone: string list }

type CAPDutyPosition =
    { Duty: string
      Wing: string
      Assistant: bool
      ORGID: ORGID }

type MemberReference =
    | CAPNHQMember of Guid * CAPID
    | CAPProspectiveMember of Guid
    | CAPParent of Guid
    | MilitaryMember of Guid
    | ExternalCivilian of Guid

type CAPNHQMember =
    { Id: Guid
      CAPID: CAPID
      NameLast: string
      NameFirst: string
      NameMiddle: string
      NameSuffix: string
      Rank: string
      Parents: MemberReference list
      Children: CAPID list
      MemberType: CAPMemberType
      ORGID: ORGID }

type CAPProspectiveMember =
    { Id: Guid
      NameLast: string
      NameFirst: string
      NameMiddle: string
      NameSuffix: string
      SeniorMember: bool }

type CAPParent =
    { Id: Guid
      NameLast: string
      NameFirst: string
      NameSuffix: string
      Children: CAPID list }

type MilitaryMember =
    { Id: Guid
      NameLast: string
      NameFirst: string
      NameSuffix: string
      Rank: string
      ServiceBranch: ServiceBranch }

type ExternalCivilian =
    { Id: Guid
      NameLast: string
      NameMiddle: string
      NameFirst: string }

type Member =
    | CAPNHQMember of CAPNHQMember
    | CAPProspectiveMember of CAPProspectiveMember
    | CAPParent of CAPParent
    | MilitaryMember of MilitaryMember
    | ExternalCivilian of ExternalCivilian
