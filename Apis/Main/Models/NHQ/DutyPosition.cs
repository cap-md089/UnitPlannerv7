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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitPlanner.Apis.Main.Models.NHQ;

[Table("NHQ_DutyPosition")]
public class DutyPosition
{
    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    public string Duty { get; set; } = null!;

    public string FunctArea { get; set; } = null!;

    public string Lvl { get; set; } = null!;

    public int Asst { get; set; }

    public string UserID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    public int ORGID { get; set; }
    [ForeignKey("ORGID")]
    public Organization Organization { get; set; } = null!;
}