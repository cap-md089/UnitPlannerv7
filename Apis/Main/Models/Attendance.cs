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

public enum AttendanceStatus
{
    CommittedAttended,
    NoShow,
    RescindedCommitment,
    NoPlans,
    Processing
}

[Table("Attendance")]
public class AttendanceRecord
{
    public Guid EventId { get; set; }
    public CalendarEvent Event { get; set; } = null!;

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public string Comments { get; set; } = null!;

    public AttendanceStatus Status { get; set; }

    public bool PlanToUseProvidedTransportation { get; set; }

    public ICollection<CustomAttendanceFieldValue> CustomAttendanceFieldValues { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Timestamp { get; set; }

    public bool SummaryEmailSent { get; set; }

    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }

    public ICollection<AttendanceApproval> Approvals { get; set; } = null!;

    public bool ParticipationFeePaid { get; set; }
}

public class CustomAttendanceFieldValue
{
    public Guid AttendanceRecordEventId { get; set; }
    public Guid AttendanceRecordMemberId { get; set; }
    public AttendanceRecord AttendanceRecord { get; set; } = null!;

    public CustomAttendanceField CustomAttendanceFieldRules { get; set; } = null!;

    public string Title { get; set; } = null!;
}

public class CustomAttendanceFieldCheckboxValue : CustomAttendanceFieldValue
{
    public bool Value { get; set; }
}

public class CustomAttendanceFieldDateValue : CustomAttendanceFieldValue
{
    public DateTime Value { get; set; }
}

public class CustomAttendanceFieldNumberValue : CustomAttendanceFieldValue
{
    public int Value { get; set; }
}

public class CustomAttendanceFieldTextValue : CustomAttendanceFieldValue
{
    public string Value { get; set; } = null!;
}

public class CustomAttendanceFieldFilesValue : CustomAttendanceFieldValue
{
    public ICollection<CustomAttendanceFieldFileSubmission> Submissions { get; set; } = null!;
}

public class CustomAttendanceFieldFileSubmission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid FileId { get; set; }
}