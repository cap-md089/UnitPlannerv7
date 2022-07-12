namespace UnitPlanner.Common.Util

type NonEmptyList<'a> =
    | Cons of 'a * NonEmptyList<'a>
    | Single of 'a

type 'a nel = NonEmptyList<'a>

module NonEmptyList =
    let rec add v =
        function
        | Single e -> Cons(e, Single v)
        | Cons (e, l') -> Cons(e, add v l')

    let rec append n1 n2 =
        match n1 with
        | Cons (e, l') -> Cons(e, append l' n2)
        | Single (e) -> Cons(e, n2)

    let rec choose f l =
        match l with
        | Single e ->
            match f e with
            | Some e' -> Single e' |> Some
            | None -> None
        | Cons (e, l') ->
            match (f e, choose f l') with
            | (Some f, Some o) -> Cons(f, o) |> Some
            | (Some f, None) -> Single f |> Some
            | (None, Some o) -> Some o
            | (None, None) -> None

    let rec collect f l =
        match l with
        | Cons (e, l') -> append (f e) (collect f l')
        | Single e -> f e

    let rec concat =
        function
        | Single e -> e
        | Cons (e, l') -> append e (concat l')

    let rec contains value =
        function
        | Single e -> e = value
        | Cons (e, l') -> e = value || contains value l'

    let rec exists pred =
        function
        | Single e -> pred e
        | Cons (e, l') -> pred e || exists pred l'

    let filter f =
        choose (fun x -> if f x then Some x else None)

    let rec find pred =
        function
        | Single e -> if pred e then Some e else None
        | Cons (e, l') -> if pred e then Some e else find pred l'

    let rec fold f st =
        function
        | Single e -> f st e
        | Cons (e, l') -> fold f (f st e) l'

    let rec forall pred =
        function
        | Single e -> pred e
        | Cons (e, l') -> pred e && forall pred l'

    let rec head =
        function
        | Single e -> e
        | Cons (e, _) -> e

    let rec last =
        function
        | Single e -> e
        | Cons (_, l') -> last l'

    let rec length =
        function
        | Single _ -> 1
        | Cons (_, l') -> length l' + 1

    let rec map f =
        function
        | Single e -> f e |> Single
        | Cons (e, l') -> Cons(f e, map f l')

    let rec fromList =
        function
        | [] -> None
        | [ e ] -> Single e |> Some
        | e :: l' -> fromList l' |> Option.map (fun c -> Cons(e, c))

    let tail =
        function
        | Single _ -> None
        | Cons (_, l') -> Some l'

    let rec toList =
        function
        | Single e -> [ e ]
        | Cons (e, l') -> e :: toList l'

    let singleton = Single

    let rec zip l1 l2 =
        match (l1, l2) with
        | (Single e1, Single e2) -> Single(e1, e2)
        | (Single e1, Cons (e2, _)) -> Single(e1, e2)
        | (Cons (e1, _), Single e2) -> Single(e1, e2)
        | (Cons (e1, l'1), Cons (e2, l'2)) -> Cons((e1, e2), zip l'1 l'2)
