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

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UnitPlanner.Services.Capwatch.Models;

[Table("NHQ_CadetAchv")]
public class CadetAchv
{
    public int CadetAchvID { get; set; }

    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    public DateTime PhyFitTest { get; set; }

    public DateTime LeadLabDateP { get; set; }

    public int LeadLabScore { get; set; }

    public DateTime AEDateP { get; set; }

    public int AEScore { get; set; }

    public int AEMod { get; set; }

    public int AETest { get; set; }

    public DateTime MoralLDateP { get; set; }

    public bool ActivePart { get; set; }

    public bool OtherReq { get; set; }

    public bool SDAReport { get; set; }

    public string UsrID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    public string FirstUsr { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DrillDate { get; set; }

    public int DrillScore { get; set; }

    public string LeadCurr { get; set; } = null!;

    public bool CadetOath { get; set; }

    public string AEBookValue { get; set; } = null!;

    public DateTime StaffServicesDate { get; set; }

    public string TechnicalWritingAssignment { get; set; } = null!;

    public DateTime TechnicalWritingAssignmentDate { get; set; }

    public DateTime OralPresentationDate { get; set; }

    public DateTime SpeechDate { get; set; }

    public DateTime LeadershipEssayDate { get; set; }

    public CadetHFZInformation CadetHFZInformation { get; set; } = null!;
}