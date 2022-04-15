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

[<Table("NHQ_CadetHFZInformation")>]
type CadetHFZInformation<'a, 'b>(HFZID : int, CAPID : int, Member : 'a,
                            DateTaken : DateTime, ORGID : int, Organization : 'b,
                            IsPassed : int, WeatherWaiver : int,
                            PacerRun : string, PacerRunWaiver : int,
                            PacerRunPassed : int, MileRun : string,
                            MileRunWaiver : int, MileRunPassed : int,
                            CurlUp : string, CurlUpWaiver : int,
                            CurlUpPassed : int, PushUp : string,
                            PushUpWaiver : int, PushUpPassed : int,
                            SitAndReach : string, SitAndReachWaiver : int,
                            SitAndReachPassed : int) =
    [<Key>]
    member this.HFZID = HFZID

    member this.CAPID = CAPID
    [<ForeignKey("CAPID")>]
    member this.Member = Member

    member this.DateTaken = DateTaken

    member this.ORGID = ORGID
    [<ForeignKey("ORGID")>]
    member this.Organization = Organization

    member this.IsPassed = IsPassed

    member this.WeatherWaiver = WeatherWaiver

    member this.PacerRun = PacerRun

    member this.PacerRunWaiver = PacerRunWaiver

    member this.PacerRunPassed = PacerRunPassed

    member this.MileRun = MileRun

    member this.MileRunWaiver = MileRunWaiver

    member this.MileRunPassed = MileRunPassed

    member this.CurlUp = CurlUp

    member this.CurlUpWaiver = CurlUpWaiver

    member this.CurlUpPassed = CurlUpPassed

    member this.PushUp = PushUp

    member this.PushUpWaiver = PushUpWaiver

    member this.PushUpPassed = PushUpPassed

    member this.SitAndReach = SitAndReach

    member this.SitAndReachWaiver = SitAndReachWaiver

    member this.SitAndReachPassed = SitAndReachPassed