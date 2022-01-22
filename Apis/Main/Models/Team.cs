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

namespace UnitPlanner.Apis.Main.Models;

public enum TeamVisibility
{
    Public,
    Protected,
    Private
}

public class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<TeamMembership> TeamMembers { get; set; } = null!;

    public TeamVisibility Visibility { get; set; }
}

public class TeamMembership
{
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsLeadershipRole { get; set; }
}