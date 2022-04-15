namespace UnitPlanner.Services.Capwatch.Service.Models

open System
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.EntityFrameworkCore

[<TableAttribute("NHQ_CadetAchvAprs")>]
type CadetAchvAprs<'a>(CadetAchvID : int, CAPID : int, Member : 'a, Status : string, AprCAPID : int, DspReason : string,
                        AwardNo : int, JROTCWaiver : bool, UsrID : string, DateMod : DateTime, FirstUsr : string,
                        DateCreated : DateTime, PrintedCert : bool) =

    [<Key>]
    member this.CadetAchvID = CadetAchvID

    member this.CAPID = CAPID
    [<ForeignKey("ORGID")>]
    member this.Member = Member

    member this.Status = Status

    member this.AprCAPID = AprCAPID

    member this.DspReason = DspReason

    member this.AwardNo = AwardNo 

    member this.JROTCWaiver = JROTCWaiver

    member this.UsrID = UsrID

    member this.DateMod = DateMod

    member this.FirstUsr = FirstUsr

    member this.DateCreated = DateCreated

    member this.PrintedCert = PrintedCert