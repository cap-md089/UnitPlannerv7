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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UnitPlanner.Apis.Main.Models.NHQ;

[Table("NHQ_MbrAchievements")]
public class MbrAchievements
{
    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    public int AchvID { get; set; }

    [StringLength(25)]
    public string Status { get; set; } = null!;

    public DateTime OriginallyAccomplished { get; set; }

    public DateTime Completed { get; set; }

    public DateTime Expiration { get; set; }

    public int AuthByCAPID { get; set; }

    [StringLength(250)]
    public string AuthReason { get; set; } = null!;

    public DateTime AuthDate { get; set; }

    [StringLength(25)]
    public string Source { get; set; } = null!;

    public int RecID { get; set; }

    [StringLength(25)]
    public string FirstUsr { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    [StringLength(25)]
    public string UsrID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    public int ORGID { get; set; }
}