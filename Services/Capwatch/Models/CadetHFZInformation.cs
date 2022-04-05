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

namespace UnitPlanner.Services.Capwatch.Models;

[Table("NHQ_CadetHFZInformation")]
public class CadetHFZInformation
{
    [Key]
    public int HFZID { get; set; }

    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    public DateTime DateTaken { get; set; }

    public int ORGID { get; set; }
    [ForeignKey("ORGID")]
    public Organization Organization { get; set; } = null!;

    public int IsPassed { get; set; }

    public int WeatherWaiver { get; set; }

    public string PacerRun { get; set; } = null!;

    public int PacerRunWaiver { get; set; }

    public int PacerRunPassed { get; set; }

    public string MileRun { get; set; } = null!;

    public int MileRunWaiver { get; set; }

    public int MileRunPassed { get; set; }

    public string CurlUp { get; set; } = null!;

    public int CurlUpWaiver { get; set; }

    public int CurlUpPassed { get; set; }

    public string PushUp { get; set; } = null!;

    public int PushUpWaiver { get; set; }

    public int PushUpPassed { get; set; }

    public string SitAndReach { get; set; } = null!;

    public int SitAndReachWaiver { get; set; }

    public int SitAndReachPassed { get; set; }
}