module AdventOfCodeFSharp.Solutions.Day01

let calorieLists input =
    let folder list s =
        match s with
        | "" -> 0 :: list
        | a -> match list with
               | head::tail -> head + int a :: tail
               | [] -> [int a]
    Array.fold folder [0] input


let SolvePartOne input =
    input |> calorieLists |> List.max |> string
    
let SolvePartTwo input =
    input |> calorieLists |> List.sortDescending |> List.take 3 |> List.sum |> string
    
let SolutionPartOne = "69206"
let SolutionPartTwo = "197400"