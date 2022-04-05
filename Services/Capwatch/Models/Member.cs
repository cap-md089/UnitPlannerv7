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

namespace UnitPlanner.Services.Capwatch.Models;

[Table("NHQ_Member")]
public class Member
{
    [Key]
    public int CAPID { get; set; }

    public string SSN => "";

    [StringLength(25)]
    public string NameLast { get; set; } = null!;

    [StringLength(25)]
    public string NameFirst { get; set; } = null!;

    [StringLength(25)]
    public string NameMiddle { get; set; } = null!;

    [StringLength(5)]
    public string NameSuffix { get; set; } = null!;

    [StringLength(6)]
    public string Gender { get; set; } = null!;

    public DateTime DOB { get; set; }

    [StringLength(150)]
    public string Profession { get; set; } = null!;

    [StringLength(2)]
    public string EducationLevel { get; set; } = null!;

    [StringLength(15)]
    public string Citizen { get; set; } = null!;

    public int ORGID { get; set; }
    [ForeignKey("ORGID")]
    public Organization Organization { get; set; } = null!;

    [StringLength(3)]
    public string Wing { get; set; } = null!;

    [StringLength(3)]
    public string Unit { get; set; } = null!;

    [StringLength(15)]
    public string Rank { get; set; } = null!;

    public DateTime Joined { get; set; }

    public DateTime Expiration { get; set; }

    public DateTime OrgJoined { get; set; }

    [StringLength(25)]
    public string UsrID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    [StringLength(1)]
    public string LSCode { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime RankDate { get; set; }

    [StringLength(3)]
    public string Region { get; set; } = null!;

    [StringLength(15)]
    public string MbrStatus { get; set; } = null!;

    [StringLength(15)]
    public string PicStatus { get; set; } = null!;

    public DateTime PicDate { get; set; }

    [StringLength(25)]
    public string CdtWaiver { get; set; } = null!;

    [StringLength(45)]
    public string Ethnicity { get; set; } = null!;

    public ICollection<CadetAchv> CadetAchv { get; set; } = null!;

    public ICollection<CadetAchvAprs> CadetAchvAprs { get; set; } = null!;

    public ICollection<CadetActivities> Activities { get; set; } = null!;

    public ICollection<CadetHFZInformation> HFZInformation { get; set; } = null!;

    public ICollection<CadetDutyPosition> CadetDutyPositions { get; set; } = null!;

    public ICollection<DutyPosition> DutyPositions { get; set; } = null!;

    public ICollection<MbrContact> ContactInfo { get; set; } = null!;

    public ICollection<OFlight> OFlights { get; set; } = null!;
}