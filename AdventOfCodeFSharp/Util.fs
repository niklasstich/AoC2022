module AdventOfCodeFSharp.Util

open System.IO
open InputProvider
let getInput day =
    InputExtensions.GetInput day
   
let getInputLines day =
    InputExtensions.GetInputLines day