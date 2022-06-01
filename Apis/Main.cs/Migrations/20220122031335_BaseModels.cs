using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitPlanner.Apis.Main.Migrations
{
    public partial class BaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Units_UnitId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Events_UnitId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Author_MemberId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Author_Type",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Events",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorMemberId",
                table: "Events",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "Events",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Events",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetDateTime",
                table: "Events",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Events",
                type: "datetime(6)",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDateTime",
                table: "Events",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CdtAchvEnum",
                columns: table => new
                {
                    CadetAchvID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AchvName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurAwdNo = table.Column<int>(type: "int", nullable: false),
                    UsrID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FirstUsr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Rank = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CdtAchvEnum", x => x.CadetAchvID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_Organization",
                columns: table => new
                {
                    ORGID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Region = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Wing = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NextLevel = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateChartered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Scope = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsrID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FirstUsr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrgNotes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_Organization", x => x.ORGID);
                    table.ForeignKey(
                        name: "FK_NHQ_Organization_NHQ_Organization_NextLevel",
                        column: x => x.NextLevel,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visibility = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountDomain",
                columns: table => new
                {
                    Domain = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDomain", x => x.Domain);
                    table.ForeignKey(
                        name: "FK_AccountDomain_Accounts_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CAPWings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPWings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAPWings_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlanToUseProvidedTransportation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    SummaryEmailSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ParticipationFeePaid = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.EventId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_Attendance_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExtraAccountMembership",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraAccountMembership", x => new { x.AccountId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_ExtraAccountMembership_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraAccountMembership_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PointsOfContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceiveEventUpdates = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReceiveRoster = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReceiveSignUpUpdates = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DisplayPublicly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CalendarEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsOfContact_Events_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointsOfContact_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Signature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signature_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_Member",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    NameLast = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameFirst = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameMiddle = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameSuffix = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Profession = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EducationLevel = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Citizen = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ORGID = table.Column<int>(type: "int", nullable: false),
                    Wing = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rank = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Joined = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrgJoined = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsrID = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LSCode = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RankDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Region = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MbrStatus = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PicStatus = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PicDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CdtWaiver = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ethnicity = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_Member", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_NHQ_Member_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NHQ_Member_NHQ_Organization_ORGID",
                        column: x => x.ORGID,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NotificationDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationData_NotificationDataId",
                        column: x => x.NotificationDataId,
                        principalTable: "NotificationData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventsRegular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Details_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_Subtitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_MeetLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_StartDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Details_EventLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_EndDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Details_PickupLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_TransportationDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_Uniform_DressBlueA = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_DressBlueB = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_AirmanBattleUniform = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_PTGear = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_PoloShirts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_BlueUtilities = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_Civies = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_FlightSuit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Uniform_NotApplicable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_DesiredNumberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    Details_RegistrationDeadline_RegistrationDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Details_RegistrationDeadline_DeadlineInformation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_ParticipationFee_FeeDue = table.Column<double>(type: "double", nullable: true),
                    Details_ParticipationFee_FeeDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Details_EmailInformation_Body = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_Meals_NoMeals = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Meals_MealsProvided = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Meals_BringOwnFood = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Meals_BringMoney = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Meals_Other = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_LodgingArrangements_HotelOrIndividualRoom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_LodgingArrangements_OpenBayBuilding = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_LodgingArrangements_LargeTent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_LodgingArrangements_IndividualTent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_LodgingArrangements_Other = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_ActivityDescription_SquadronMeeting = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_ActivityDescription_ClassroomTourLight = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_ActivityDescription_Backcountry = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_ActivityDescription_Flying = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_ActivityDescription_PhysicallyRigorous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_HighAdventureDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_ExternalEventWebsite = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_RequiredForms_CAPIDCard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF31 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF6080 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF101 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF160 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF161 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_RequiredForms_CAPF163 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_MemberDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_SignupDenyMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_ShowOnMainPageAsUpcoming = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_Complete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_AdministrationComments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Details_Status = table.Column<int>(type: "int", nullable: false),
                    Details_AllowShifts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details_TeamInformation_TeamId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Details_TeamInformation_LimitSignupsToTeamMembers = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Details_AttendanceViewOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsRegular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsRegular_Events_Id",
                        column: x => x.Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsRegular_Teams_Details_TeamInformation_TeamId",
                        column: x => x.Details_TeamInformation_TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeamMembership",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLeadershipRole = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembership", x => new { x.MemberId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamMembership_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembership_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CAPGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAPGroups_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAPGroups_CAPWings_WingId",
                        column: x => x.WingId,
                        principalTable: "CAPWings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CadetAchvAprs",
                columns: table => new
                {
                    CadetAchvID = table.Column<int>(type: "int", nullable: false),
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AprCAPID = table.Column<int>(type: "int", nullable: false),
                    DspReason = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AwardNo = table.Column<int>(type: "int", nullable: false),
                    JROTCWaiver = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsrID = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FirstUsr = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PrintedCert = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CadetAchvAprs", x => new { x.CAPID, x.CadetAchvID });
                    table.ForeignKey(
                        name: "FK_NHQ_CadetAchvAprs_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CadetActivities",
                columns: table => new
                {
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CadetActivities", x => new { x.CAPID, x.Completed });
                    table.ForeignKey(
                        name: "FK_NHQ_CadetActivities_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CadetDutyPosition",
                columns: table => new
                {
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    Duty = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Asst = table.Column<int>(type: "int", nullable: false),
                    ORGID = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FunctArea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lvl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CadetDutyPosition", x => new { x.CAPID, x.Duty, x.ORGID, x.Asst });
                    table.ForeignKey(
                        name: "FK_NHQ_CadetDutyPosition_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NHQ_CadetDutyPosition_NHQ_Organization_ORGID",
                        column: x => x.ORGID,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CadetHFZInformation",
                columns: table => new
                {
                    HFZID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateTaken = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ORGID = table.Column<int>(type: "int", nullable: false),
                    IsPassed = table.Column<int>(type: "int", nullable: false),
                    WeatherWaiver = table.Column<int>(type: "int", nullable: false),
                    PacerRun = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PacerRunWaiver = table.Column<int>(type: "int", nullable: false),
                    PacerRunPassed = table.Column<int>(type: "int", nullable: false),
                    MileRun = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MileRunWaiver = table.Column<int>(type: "int", nullable: false),
                    MileRunPassed = table.Column<int>(type: "int", nullable: false),
                    CurlUp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurlUpWaiver = table.Column<int>(type: "int", nullable: false),
                    CurlUpPassed = table.Column<int>(type: "int", nullable: false),
                    PushUp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PushUpWaiver = table.Column<int>(type: "int", nullable: false),
                    PushUpPassed = table.Column<int>(type: "int", nullable: false),
                    SitAndReach = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SitAndReachWaiver = table.Column<int>(type: "int", nullable: false),
                    SitAndReachPassed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CadetHFZInformation", x => x.HFZID);
                    table.ForeignKey(
                        name: "FK_NHQ_CadetHFZInformation_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NHQ_CadetHFZInformation_NHQ_Organization_ORGID",
                        column: x => x.ORGID,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_DutyPosition",
                columns: table => new
                {
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    Duty = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Asst = table.Column<int>(type: "int", nullable: false),
                    ORGID = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FunctArea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lvl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_DutyPosition", x => new { x.CAPID, x.Duty, x.ORGID, x.Asst });
                    table.ForeignKey(
                        name: "FK_NHQ_DutyPosition_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NHQ_DutyPosition_NHQ_Organization_ORGID",
                        column: x => x.ORGID,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_MbrContact",
                columns: table => new
                {
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Priority = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoNotContact = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_MbrContact", x => new { x.CAPID, x.Type, x.Priority });
                    table.ForeignKey(
                        name: "FK_NHQ_MbrContact_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_OFlight",
                columns: table => new
                {
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    FltDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Wing = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    Syllabus = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TransDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FltRlsNum = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcftTailNum = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FltTime = table.Column<double>(type: "double", nullable: false),
                    LstUser = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LstDateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_OFlight", x => new { x.CAPID, x.FltDate });
                    table.ForeignKey(
                        name: "FK_NHQ_OFlight_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttendanceApprovalRequirement",
                columns: table => new
                {
                    AttendanceApprovalRequirementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PreviousApprovalRequirementAttendanceApprovalRequirementId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApprovalLevel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CalendarEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegularCalendarEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceApprovalRequirement", x => x.AttendanceApprovalRequirementId);
                    table.ForeignKey(
                        name: "FK_AttendanceApprovalRequirement_AttendanceApprovalRequirement_~",
                        column: x => x.PreviousApprovalRequirementAttendanceApprovalRequirementId,
                        principalTable: "AttendanceApprovalRequirement",
                        principalColumn: "AttendanceApprovalRequirementId");
                    table.ForeignKey(
                        name: "FK_AttendanceApprovalRequirement_Events_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceApprovalRequirement_EventsRegular_RegularCalendarE~",
                        column: x => x.RegularCalendarEventId,
                        principalTable: "EventsRegular",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DebriefItem",
                columns: table => new
                {
                    DebriefItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Submitted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SourceEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DisplayToPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DebriefText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebriefItem", x => x.DebriefItemId);
                    table.ForeignKey(
                        name: "FK_DebriefItem_EventsRegular_SourceEventId",
                        column: x => x.SourceEventId,
                        principalTable: "EventsRegular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DebriefItem_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventsLinked",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OverridenProperties_Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_Subtitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_MeetLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_StartDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OverridenProperties_EventLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_EndDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OverridenProperties_PickupLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_TransportationDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_Uniform_DressBlueA = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_DressBlueB = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_AirmanBattleUniform = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_PTGear = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_PoloShirts = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_BlueUtilities = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_Civies = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_FlightSuit = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Uniform_NotApplicable = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_DesiredNumberOfParticipants = table.Column<int>(type: "int", nullable: true),
                    OverridenProperties_RegistrationDeadline_RegistrationDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OverridenProperties_RegistrationDeadline_DeadlineInformation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_ParticipationFee_FeeDue = table.Column<double>(type: "double", nullable: true),
                    OverridenProperties_ParticipationFee_FeeDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OverridenProperties_EmailInformation_Body = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_Meals_NoMeals = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Meals_MealsProvided = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Meals_BringOwnFood = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Meals_BringMoney = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Meals_Other = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_LodgingArrangements_HotelOrIndividualRoom = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_LodgingArrangements_OpenBayBuilding = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_LodgingArrangements_LargeTent = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_LodgingArrangements_IndividualTent = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_LodgingArrangements_Other = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_ActivityDescription_SquadronMeeting = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_ActivityDescription_ClassroomTourLight = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_ActivityDescription_Backcountry = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_ActivityDescription_Flying = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_ActivityDescription_PhysicallyRigorous = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_HighAdventureDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_ExternalEventWebsite = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_RequiredForms_CAPIDCard = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF31 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF6080 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF101 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF160 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF161 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_RequiredForms_CAPF163 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_MemberDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_SignupDenyMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_ShowOnMainPageAsUpcoming = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_Complete = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_AdministrationComments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverridenProperties_Status = table.Column<int>(type: "int", nullable: true),
                    OverridenProperties_AllowShifts = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_TeamInformation_TeamId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    OverridenProperties_TeamInformation_LimitSignupsToTeamMembers = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    OverridenProperties_AttendanceViewOptions = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsLinked", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsLinked_Events_Id",
                        column: x => x.Id,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsLinked_EventsRegular_ParentId",
                        column: x => x.ParentId,
                        principalTable: "EventsRegular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsLinked_Teams_OverridenProperties_TeamInformation_TeamId",
                        column: x => x.OverridenProperties_TeamInformation_TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CAPActivityAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HostId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAPGroupId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAPWingId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPActivityAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAPActivityAccounts_Accounts_HostId",
                        column: x => x.HostId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAPActivityAccounts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAPActivityAccounts_CAPGroups_CAPGroupId",
                        column: x => x.CAPGroupId,
                        principalTable: "CAPGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CAPActivityAccounts_CAPWings_CAPWingId",
                        column: x => x.CAPWingId,
                        principalTable: "CAPWings",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CAPSquadrons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WingId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPSquadrons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAPSquadrons_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAPSquadrons_CAPGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CAPGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAPSquadrons_CAPWings_WingId",
                        column: x => x.WingId,
                        principalTable: "CAPWings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NHQ_CadetAchv",
                columns: table => new
                {
                    CadetAchvID = table.Column<int>(type: "int", nullable: false),
                    CAPID = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PhyFitTest = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LeadLabDateP = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LeadLabScore = table.Column<int>(type: "int", nullable: false),
                    AEDateP = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AEScore = table.Column<int>(type: "int", nullable: false),
                    AEMod = table.Column<int>(type: "int", nullable: false),
                    AETest = table.Column<int>(type: "int", nullable: false),
                    MoralLDateP = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActivePart = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OtherReq = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SDAReport = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsrID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateMod = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FirstUsr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DrillDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DrillScore = table.Column<int>(type: "int", nullable: false),
                    LeadCurr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CadetOath = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AEBookValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffServicesDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TechnicalWritingAssignment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TechnicalWritingAssignmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OralPresentationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SpeechDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LeadershipEssayDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CadetHFZInformationHFZID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHQ_CadetAchv", x => new { x.CAPID, x.CadetAchvID });
                    table.ForeignKey(
                        name: "FK_NHQ_CadetAchv_NHQ_CadetHFZInformation_CadetHFZInformationHFZ~",
                        column: x => x.CadetHFZInformationHFZID,
                        principalTable: "NHQ_CadetHFZInformation",
                        principalColumn: "HFZID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NHQ_CadetAchv_NHQ_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "NHQ_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttendanceApproval",
                columns: table => new
                {
                    AttendanceApprovalRequirementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AttendanceRecordMemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AttendanceRecordEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SignatureId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SignOffDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    ApprovalStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceApproval", x => new { x.AttendanceApprovalRequirementId, x.AttendanceRecordEventId, x.AttendanceRecordMemberId });
                    table.ForeignKey(
                        name: "FK_AttendanceApproval_Attendance_AttendanceRecordEventId_Attend~",
                        columns: x => new { x.AttendanceRecordEventId, x.AttendanceRecordMemberId },
                        principalTable: "Attendance",
                        principalColumns: new[] { "EventId", "MemberId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceApproval_AttendanceApprovalRequirement_AttendanceA~",
                        column: x => x.AttendanceApprovalRequirementId,
                        principalTable: "AttendanceApprovalRequirement",
                        principalColumn: "AttendanceApprovalRequirementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceApproval_Signature_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomAttendanceField",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayToMember = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowMemberToModify = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LinkedEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RegularCalendarEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreFill = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CustomAttendanceFieldDate_PreFill = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CustomAttendanceFieldNumber_PreFill = table.Column<double>(type: "double", nullable: true),
                    CustomAttendanceFieldText_PreFill = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAttendanceField", x => new { x.EventId, x.Title });
                    table.ForeignKey(
                        name: "FK_CustomAttendanceField_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomAttendanceField_EventsLinked_LinkedEventId",
                        column: x => x.LinkedEventId,
                        principalTable: "EventsLinked",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomAttendanceField_EventsRegular_RegularCalendarEventId",
                        column: x => x.RegularCalendarEventId,
                        principalTable: "EventsRegular",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RegularCalendarEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipment_EventsLinked_LinkedEventId",
                        column: x => x.LinkedEventId,
                        principalTable: "EventsLinked",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_EventsRegular_RegularCalendarEventId",
                        column: x => x.RegularCalendarEventId,
                        principalTable: "EventsRegular",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountOrganizationMapping",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ORGID = table.Column<int>(type: "int", nullable: false),
                    CAPGroupId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAPSquadronId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAPWingId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOrganizationMapping", x => new { x.AccountId, x.ORGID });
                    table.ForeignKey(
                        name: "FK_AccountOrganizationMapping_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountOrganizationMapping_CAPGroups_CAPGroupId",
                        column: x => x.CAPGroupId,
                        principalTable: "CAPGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountOrganizationMapping_CAPSquadrons_CAPSquadronId",
                        column: x => x.CAPSquadronId,
                        principalTable: "CAPSquadrons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountOrganizationMapping_CAPWings_CAPWingId",
                        column: x => x.CAPWingId,
                        principalTable: "CAPWings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountOrganizationMapping_NHQ_Organization_ORGID",
                        column: x => x.ORGID,
                        principalTable: "NHQ_Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomAttendanceFieldValue",
                columns: table => new
                {
                    AttendanceRecordEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AttendanceRecordMemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomAttendanceFieldCheckboxValue_Value = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CustomAttendanceFieldDateValue_Value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CustomAttendanceFieldNumberValue_Value = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAttendanceFieldValue", x => new { x.AttendanceRecordEventId, x.AttendanceRecordMemberId, x.Title });
                    table.ForeignKey(
                        name: "FK_CustomAttendanceFieldValue_Attendance_AttendanceRecordEventI~",
                        columns: x => new { x.AttendanceRecordEventId, x.AttendanceRecordMemberId },
                        principalTable: "Attendance",
                        principalColumns: new[] { "EventId", "MemberId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomAttendanceFieldValue_CustomAttendanceField_AttendanceR~",
                        columns: x => new { x.AttendanceRecordEventId, x.Title },
                        principalTable: "CustomAttendanceField",
                        principalColumns: new[] { "EventId", "Title" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomAttendanceFieldFileSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CustomAttendanceFieldFilesValueAttendanceRecordEventId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CustomAttendanceFieldFilesValueAttendanceRecordMemberId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CustomAttendanceFieldFilesValueTitle = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAttendanceFieldFileSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomAttendanceFieldFileSubmission_CustomAttendanceFieldVal~",
                        columns: x => new { x.CustomAttendanceFieldFilesValueAttendanceRecordEventId, x.CustomAttendanceFieldFilesValueAttendanceRecordMemberId, x.CustomAttendanceFieldFilesValueTitle },
                        principalTable: "CustomAttendanceFieldValue",
                        principalColumns: new[] { "AttendanceRecordEventId", "AttendanceRecordMemberId", "Title" });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorMemberId",
                table: "Events",
                column: "AuthorMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CalendarId",
                table: "Events",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDomain_UnitId",
                table: "AccountDomain",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizationMapping_CAPGroupId",
                table: "AccountOrganizationMapping",
                column: "CAPGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizationMapping_CAPSquadronId",
                table: "AccountOrganizationMapping",
                column: "CAPSquadronId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizationMapping_CAPWingId",
                table: "AccountOrganizationMapping",
                column: "CAPWingId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizationMapping_ORGID",
                table: "AccountOrganizationMapping",
                column: "ORGID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_MemberId",
                table: "Attendance",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceApproval_AttendanceRecordEventId_AttendanceRecordM~",
                table: "AttendanceApproval",
                columns: new[] { "AttendanceRecordEventId", "AttendanceRecordMemberId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceApproval_SignatureId",
                table: "AttendanceApproval",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceApprovalRequirement_CalendarEventId",
                table: "AttendanceApprovalRequirement",
                column: "CalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceApprovalRequirement_PreviousApprovalRequirementAtt~",
                table: "AttendanceApprovalRequirement",
                column: "PreviousApprovalRequirementAttendanceApprovalRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceApprovalRequirement_RegularCalendarEventId",
                table: "AttendanceApprovalRequirement",
                column: "RegularCalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_AccountId",
                table: "Calendars",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPActivityAccounts_CAPGroupId",
                table: "CAPActivityAccounts",
                column: "CAPGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPActivityAccounts_CAPWingId",
                table: "CAPActivityAccounts",
                column: "CAPWingId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPActivityAccounts_HostId",
                table: "CAPActivityAccounts",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPGroups_WingId",
                table: "CAPGroups",
                column: "WingId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPSquadrons_GroupId",
                table: "CAPSquadrons",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CAPSquadrons_WingId",
                table: "CAPSquadrons",
                column: "WingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomAttendanceField_LinkedEventId",
                table: "CustomAttendanceField",
                column: "LinkedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomAttendanceField_RegularCalendarEventId",
                table: "CustomAttendanceField",
                column: "RegularCalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomAttendanceFieldFileSubmission_CustomAttendanceFieldFil~",
                table: "CustomAttendanceFieldFileSubmission",
                columns: new[] { "CustomAttendanceFieldFilesValueAttendanceRecordEventId", "CustomAttendanceFieldFilesValueAttendanceRecordMemberId", "CustomAttendanceFieldFilesValueTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomAttendanceFieldValue_AttendanceRecordEventId_Title",
                table: "CustomAttendanceFieldValue",
                columns: new[] { "AttendanceRecordEventId", "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_DebriefItem_MemberId",
                table: "DebriefItem",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DebriefItem_SourceEventId",
                table: "DebriefItem",
                column: "SourceEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LinkedEventId",
                table: "Equipment",
                column: "LinkedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RegularCalendarEventId",
                table: "Equipment",
                column: "RegularCalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsLinked_OverridenProperties_TeamInformation_TeamId",
                table: "EventsLinked",
                column: "OverridenProperties_TeamInformation_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsLinked_ParentId",
                table: "EventsLinked",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsRegular_Details_TeamInformation_TeamId",
                table: "EventsRegular",
                column: "Details_TeamInformation_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraAccountMembership_MemberId",
                table: "ExtraAccountMembership",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetAchv_CadetHFZInformationHFZID",
                table: "NHQ_CadetAchv",
                column: "CadetHFZInformationHFZID");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetAchv_MemberId",
                table: "NHQ_CadetAchv",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetAchvAprs_MemberId",
                table: "NHQ_CadetAchvAprs",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetActivities_MemberId",
                table: "NHQ_CadetActivities",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetDutyPosition_MemberId",
                table: "NHQ_CadetDutyPosition",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetDutyPosition_ORGID",
                table: "NHQ_CadetDutyPosition",
                column: "ORGID");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetHFZInformation_MemberId",
                table: "NHQ_CadetHFZInformation",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_CadetHFZInformation_ORGID",
                table: "NHQ_CadetHFZInformation",
                column: "ORGID");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_DutyPosition_MemberId",
                table: "NHQ_DutyPosition",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_DutyPosition_ORGID",
                table: "NHQ_DutyPosition",
                column: "ORGID");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_MbrContact_MemberId",
                table: "NHQ_MbrContact",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_Member_ORGID",
                table: "NHQ_Member",
                column: "ORGID");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_OFlight_MemberId",
                table: "NHQ_OFlight",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NHQ_Organization_NextLevel",
                table: "NHQ_Organization",
                column: "NextLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MemberId",
                table: "Notifications",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationDataId",
                table: "Notifications",
                column: "NotificationDataId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfContact_CalendarEventId",
                table: "PointsOfContact",
                column: "CalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfContact_MemberId",
                table: "PointsOfContact",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Signature_MemberId",
                table: "Signature",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembership_TeamId",
                table: "TeamMembership",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Members_AuthorMemberId",
                table: "Events",
                column: "AuthorMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Calendars_CalendarId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Members_AuthorMemberId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "AccountDomain");

            migrationBuilder.DropTable(
                name: "AccountOrganizationMapping");

            migrationBuilder.DropTable(
                name: "AttendanceApproval");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "CAPActivityAccounts");

            migrationBuilder.DropTable(
                name: "CustomAttendanceFieldFileSubmission");

            migrationBuilder.DropTable(
                name: "DebriefItem");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "ExtraAccountMembership");

            migrationBuilder.DropTable(
                name: "NHQ_CadetAchv");

            migrationBuilder.DropTable(
                name: "NHQ_CadetAchvAprs");

            migrationBuilder.DropTable(
                name: "NHQ_CadetActivities");

            migrationBuilder.DropTable(
                name: "NHQ_CadetDutyPosition");

            migrationBuilder.DropTable(
                name: "NHQ_CdtAchvEnum");

            migrationBuilder.DropTable(
                name: "NHQ_DutyPosition");

            migrationBuilder.DropTable(
                name: "NHQ_MbrContact");

            migrationBuilder.DropTable(
                name: "NHQ_OFlight");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PointsOfContact");

            migrationBuilder.DropTable(
                name: "TeamMembership");

            migrationBuilder.DropTable(
                name: "CAPSquadrons");

            migrationBuilder.DropTable(
                name: "AttendanceApprovalRequirement");

            migrationBuilder.DropTable(
                name: "Signature");

            migrationBuilder.DropTable(
                name: "CustomAttendanceFieldValue");

            migrationBuilder.DropTable(
                name: "NHQ_CadetHFZInformation");

            migrationBuilder.DropTable(
                name: "NotificationData");

            migrationBuilder.DropTable(
                name: "CAPGroups");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "CustomAttendanceField");

            migrationBuilder.DropTable(
                name: "NHQ_Member");

            migrationBuilder.DropTable(
                name: "CAPWings");

            migrationBuilder.DropTable(
                name: "EventsLinked");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "NHQ_Organization");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "EventsRegular");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Events_AuthorMemberId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CalendarId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AuthorMemberId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MeetDateTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PickupDateTime",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Events",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "Author_MemberId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Author_Type",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "Events",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UnitId",
                table: "Events",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Units_UnitId",
                table: "Events",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
