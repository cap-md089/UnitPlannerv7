module UnitPlanner.Apis.Main.Util

open Suave

let handleRequest ok bad res (ctx: HttpContext) =
    async {
        let! res = res ctx

        return!
            match res with
            | Ok (v) -> ok v ctx
            | Error (e) -> bad e ctx
    }
