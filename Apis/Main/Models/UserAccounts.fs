module UnitPlanner.Apis.Main.Models.UserAccounts

open System

open UnitPlanner.Apis.Main.Models.Account
open UnitPlanner.Apis.Main.Models.Member
open UnitPlanner.Apis.Main.Models.CalendarEvents
open UnitPlanner.Apis.Main.Models.Permissions

type AlgorithmType = PBKDF2

type SessionId = SessionId of Guid

type AccountPasswordInformation =
    { Algorithm: AlgorithmType
      Created: DateTimeOffset
      HashedPassword: string
      PasswordSalt: string
      IteractionCount: uint }

type DANGER_USE_WHEN_AUTHENTICING_ONLY_UserAccount =
    { Username: string
      Member: MemberReference
      PasswordHistory: AccountPasswordInformation list }

type UserAccount =
    { Username: string
      Member: MemberReference }

type PasswordResetToken =
    { ExpireTime: DateTimeOffset
      Token: string
      Username: string }

type SessionType =
    | Regular
    | InProgressMFASession
    | PasswordReset
    | ScanAdd of EventId * AccountId

type Session =
    { Id: SessionId
      Expires: DateTimeOffset
      UserAccount: UserAccount
      Type: SessionType }

type MFASecret =
    { Secret: string
      Member: MemberReference }

type User =
    { Member: Member
      Session: Session
      Permissions: MemberPermissions }
