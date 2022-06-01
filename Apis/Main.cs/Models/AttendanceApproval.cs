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

namespace UnitPlanner.Apis.Main.Models;

public enum ApprovalStatus
{
    Approved,
    Denied,
    Pending
}

public enum AttendanceApprovalLevel
{
    Squadron,
    Group,
    Wing,
    EventOrganizer
}

public class AttendanceApproval
{
    public Guid AttendanceApprovalRequirementId { get; set; }
    public AttendanceApprovalRequirement AttendanceApprovalRequirement { get; set; } = null!;

    public Signature Signature { get; set; } = null!;

    public Guid AttendanceRecordMemberId { get; set; }
    public Guid AttendanceRecordEventId { get; set; }
    public AttendanceRecord AttendanceRecord { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime SignOffDate { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }
}

public class AttendanceApprovalRequirement
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AttendanceApprovalRequirementId { get; set; }

    public ICollection<AttendanceApproval> AttendanceApprovals { get; set; } = null!;

    public AttendanceApprovalRequirement? PreviousApprovalRequirement { get; set; }

    public AttendanceApprovalLevel ApprovalLevel { get; set; }

    public CalendarEvent CalendarEvent { get; set; } = null!;
}

public class Signature
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Member Member { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreationDate { get; set; }
}