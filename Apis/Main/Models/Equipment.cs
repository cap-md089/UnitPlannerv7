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

namespace UnitPlanner.Apis.Main.Models;

public abstract class Equipment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid EquipmentId { get; set; }

    public string Name { get; set; } = null!;
}

public class EventEquipment : Equipment
{
}

public class SquadronEquipment : Equipment
{
}