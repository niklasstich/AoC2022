module AdventOfTestFSharp

open AdventOfCodeFSharp
open AdventOfCodeFSharp.Solutions
open NUnit.Framework

let testInputs =
    [
    Day01.SolvePartOne, Util.getInputLines(01), Day01.SolutionPartOne
    Day01.SolvePartTwo, Util.getInputLines(01), Day01.SolutionPartTwo
    ] |> List.map TestCaseData
[<Test>]
[<TestCaseSource(nameof(testInputs))>]
let TestDays (solver:string[]->string, input:string[], expected:string) =
    let actual = solver input
    Assert.AreEqual(expected, actual)
    
    