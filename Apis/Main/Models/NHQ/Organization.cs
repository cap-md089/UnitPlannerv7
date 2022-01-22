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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitPlanner.Apis.Main.Models.NHQ;

[Table("NHQ_Organization")]
public class Organization
{
    [Key]
    public int ORGID { get; set; }

    public string Region { get; set; } = null!;

    public string Wing { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int? NextLevel { get; set; }
    [ForeignKey("NextLevel")]
    public Organization? Parent { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime DateChartered { get; set; }

    public string Status { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string UsrID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    public string FirstUsr { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateReceived { get; set; }

    public string OrgNotes { get; set; } = null!;

    public ICollection<Member> Members { get; set; } = null!;

    public ICollection<Organization> SubordinateOrganizations { get; set; } = null!; 
}