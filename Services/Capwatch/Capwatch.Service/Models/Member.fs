// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UnitPlanner.Services.Capwatch.Service.Models

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

type CadetAchv_<'a> = CadetAchv<'a, CadetHFZInformation<'a, Organization<'a>>>
type CadetHFZInformation_<'a> = CadetHFZInformation<'a, Organization<'a>>
type CadetDutyPosition_<'a> = CadetDutyPosition<'a, Organization<'a>>
type DutyPosition_<'a> = DutyPosition<'a, Organization<'a>>

[<Table("NHQ_Member")>]
type Member(CAPID : int, NameLast : string, NameFirst : string,
            NameMiddle : string, NameSuffix : string, Gender : string,
            DOB : DateTime, Profession : string, EducationLevel : string,
            Citizen : string, ORGID : int, Organization : Organization<Member>,
            Wing : string, Unit : string, Rank : string, Joined : DateTime,
            Expiration : DateTime, OrgJoined : DateTime, UsrID : string,
            DateMod : DateTime, LSCode : string, Type : string, RankDate : DateTime,
            Region : string, MbrStatus : string, PicStatus : string,
            PicDate : DateTime, CdtWaiver : string, Ethnicity : string,
            CadetAchv : ICollection<CadetAchv_<Member>>,
            CadetAchvAprs : ICollection<CadetAchvAprs<Member>>,
            CadetActivities : ICollection<CadetActivities<Member>>,
            HFZInformation : ICollection<CadetHFZInformation_<Member>>,
            CadetDutyPosition : ICollection<CadetDutyPosition_<Member>>,
            DutyPosition : ICollection<DutyPosition_<Member>>,
            ContactInfo : ICollection<MbrContact<Member>>,
            OFlights : ICollection<OFlight<Member>>) =
    [<Key>]
    member this.CAPID = CAPID

    member this.SSN = ""

    [<StringLength(25)>]
    member this.NameLast = NameLast

    [<StringLength(25)>]
    member this.NameFirst = NameFirst

    [<StringLength(25)>]
    member this.NameMiddle = NameMiddle

    [<StringLength(25)>]
    member this.NameSuffix = NameSuffix

    [<StringLength(6)>]
    member this.Gender = Gender

    member this.DOB = DOB
    
    [<StringLength(150)>]
    member this.Profession = Profession

    [<StringLength(2)>]
    member this.EducationLevel = EducationLevel

    [<StringLength(15)>]
    member this.Citizen = Citizen

    member this.ORGID = ORGID
    [<ForeignKey("ORGID")>]
    member this.Organization = Organization

    [<StringLength(3)>]
    member this.Wing = Wing

    [<StringLength(3)>]
    member this.Unit = Unit

    [<StringLength(15)>]
    member this.Rank = Rank

    member this.Joined = Joined

    member this.Expiration = Expiration

    member this.OrgJoined = OrgJoined

    [<StringLength(25)>]
    member this.UsrID = UsrID

    member this.DateMod = DateMod

    [<StringLength(1)>]
    member this.LSCode = LSCode

    member this.Type = Type

    member this.RankDate = RankDate

    [<StringLength(3)>]
    member this.Region = Region

    [<StringLength(15)>]
    member this.MbrStatus = MbrStatus

    [<StringLength(15)>]
    member this.PicStatus = PicStatus

    member this.PicDate = PicDate

    [<StringLength(25)>]
    member this.CdtWaiver = CdtWaiver

    [<StringLength(45)>]
    member this.Ethnicity = Ethnicity

    member this.CadetActivities = CadetActivities
    member this.CadetAchv = CadetAchv
    member this.CadetAchvAprs = CadetAchvAprs
    member this.HFZInformation = HFZInformation
    member this.CadetDutyPositions = CadetDutyPosition
    member this.DutyPositions = DutyPosition
    member this.ContactInfo = ContactInfo
    member this.OFlights = OFlights

type CadetAchvM = CadetAchv_<Member>
type CadetHFZInformationM = CadetHFZInformation_<Member>
type CadetDutyPositionM = CadetDutyPosition_<Member>
type DutyPositionM = DutyPosition_<Member>
type CadetAchvAprsM = CadetAchvAprs<Member>
type CadetActivitiesM = CadetActivities<Member>
type OFlightM = OFlight<Member>
type OrganizationM = Organization<Member>
type MbrContactM = MbrContact<Member>