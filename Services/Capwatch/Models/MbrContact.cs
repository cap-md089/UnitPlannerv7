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

[Table("NHQ_MbrContact")]
public class MbrContact
{
    public int CAPID { get; set; }
    public Member Member { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string UserID { get; set; } = null!;

    public DateTime DateMod { get; set; }

    public int DoNotContact { get; set; }

    public string ContactName { get; set; } = null!;
}