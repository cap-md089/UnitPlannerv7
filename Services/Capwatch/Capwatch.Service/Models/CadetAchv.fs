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
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

[<Table("NHQ_CadetAchv")>]
type CadetAchv<'a, 'b>(CadetAchvId : int, CAPID : int, Member: 'a,
                    PhyFitTest : DateTime, LeadLabDateP : DateTime, LeadLabScore : int,
                    AEDateP : DateTime, AEScore : int, AEMod : int, AETest : int, MoralLDateP : DateTime,
                    ActivePart : bool, OtherReq : bool, SDAReport : bool, UsrID : string, DateMod : DateTime,
                    FirstUsr : string, DateCreated : DateTime, DrillDate : DateTime, DrillScore : int, LeadCurr : string,
                    CadetOath : bool, AEBookValue : string, StaffServicesDate : DateTime, TechnicalWritingAssignment : string, TechnicalWritingAssignmentDate : DateTime,
                    OralPresentationDate : DateTime, SpeechDate : DateTime, LeadershipEssayDate : DateTime, CadetHFZInformation : 'b) =
    
    member this.CadetAchvID = CadetAchvId

    member this.CAPID = CAPID
    [<ForeignKey("CAPID")>]
    member this.Member = Member

    member this.PhyFitTest = PhyFitTest

    member this.LeadLabDateP = LeadLabDateP

    member this.LeadLabScore = LeadLabScore

    member this.AEDateP = AEDateP

    member this.AEScore = AEScore

    member this.AEMOd = AEMod

    member this.AETest = AETest

    member this.MoralLDateP = MoralLDateP

    member this.ActivePart = ActivePart

    member this.OtherReq = OtherReq

    member this.SDAReport = SDAReport

    member this.UsrID = UsrID

    member this.DateMod = DateMod

    member this.FirstUsr = FirstUsr

    member this.DateCreated = DateCreated

    member this.DrillDate = DrillDate

    member this.DrillScore = DrillScore

    member this.LeadCurr = LeadCurr

    member this.CadetOath = CadetOath

    member this.AEBookValue = AEBookValue

    member this.StaffServicesDate = StaffServicesDate

    member this.TechnicalWritingAssignment = TechnicalWritingAssignment

    member this.TechnicalWritingAssignmentDate = TechnicalWritingAssignmentDate

    member this.OralPresentationDate = OralPresentationDate

    member this.SpeechDate = SpeechDate

    member this.LeadershipEssayDate = LeadershipEssayDate

    member this.CadetHFZInformation = CadetHFZInformation
