// Unit.cs: Represents a unit and the squadrons under them
//
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
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace UnitPlanner.Apis.Main.Models;

public abstract class Account
{
    [Key]
    public string Id { get; set; } = null!;

    [NotMapped]
    public abstract string Type { get; }

    public ICollection<Calendar> Calendars { get; set; } = null!;

    public ICollection<AccountDomain> Domains { get; set; } = null!;

    public ICollection<ExtraAccountMembership> ExtraAccountMembership { get; set; } = null!;

    public ICollection<AdminNotification> AdminNotifications { get; set; } = null!;

    public AccountSettings Settings { get; set; } = null!;
}

public class AccountOrganizationMapping
{
    public string AccountId { get; set; } = null!;
    [JsonIgnore]
    public Account Account { get; set; } = null!;

    public int ORGID { get; set; }
    [ForeignKey("ORGID")]
    public Models.NHQ.Organization Organization { get; set; } = null!;
}

[Table("ExtraAccountMembership")]
public class ExtraAccountMembership
{
    public string AccountId { get; set; } = null!;
    [JsonIgnore]
    public Account Account { get; set; } = null!;

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;
}

public class AccountDomain
{
    [Key]
    public string Domain { get; set; } = null!;

    [JsonIgnore]
    public Account Unit { get; set; } = null!;
}

[Owned]
public class AccountSettings
{
}