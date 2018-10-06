module test

open System
open Cap

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

//Select how many to make
//Send captcha request
//Wait for captcha to come back
//Send RS request using captcha response
//Wait for response
//Save to notepad
    

let tryToInt s =
    match System.Int32.TryParse s with
    | true, v -> Some v
    | false, _ -> None


let rec ConsoleLoop() = 
    match Console.ReadLine() |> fun x -> tryToInt x with
    | Some x    ->  printfn "Start creating %i sets of 5 accounts" x
                    for i in 1 .. x do
                         Async.Start (Cap.AccountGenAstnc 1)

                    printfn "Finished"
                    ConsoleLoop()
    | None      -> printfn "This is not the correct value"
                   ConsoleLoop()
    
[<EntryPoint>]
let main argv = 
    ConsoleLoop()
    printfn "%A" argv
    0 // return an integer exit code
