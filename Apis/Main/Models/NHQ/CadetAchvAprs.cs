// Copyright (C) 2022 Andrew Rioux, Glenn Rioux
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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitPlanner.Apis.Main.Models.NHQ;

[Table("NHQ_CadetAchvAprs")]
public class CadetAchvAprs
{
    public int CadetAchvID { get; set; }

    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    [StringLength(3)]
    public string Status { get; set; } = null!;

    public int AprCAPID { get; set; }

    [StringLength(15)]
    public string DspReason { get; set; } = null!;

    public int AwardNo { get; set; }

    public bool JROTCWaiver { get; set; }

    [StringLength(25)]
    public string UsrID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    [StringLength(25)]
    public string FirstUsr { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public bool PrintedCert { get; set; }
}