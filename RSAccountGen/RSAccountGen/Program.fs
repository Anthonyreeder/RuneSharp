open System.Net
open System
open FSharp.Data
open System.Threading
open System.Threading

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
    werqw
let postCaptchaRequest() =
    Http.RequestString("http://2captcha.com/in.php", body = FormValues [    
    "key", "2bc08ce6750eb3ff4b9a6615529e8213";
    "method", "userrecaptcha";
    "googlekey", "6LccFA0TAAAAAHEwUJx_c1TfTBWMTAOIphwTtd1b";
    "pageurl", "https://secure.runescape.com/m=account-creation/create_account"]) |> fun x -> (x.Replace("OK|", ""))

let rec getCaptchaRequest id:string =
    match Http.RequestString("http://2captcha.com/res.php", 
                              httpMethod = "GET", 
                              query = ["key", "2bc08ce6750eb3ff4b9a6615529e8213"; "action", "get"; "id", id]) with
         | "CAPCHA_NOT_READY" -> printfn "Sleeping for 1 min"
                                 Thread.Sleep(60000)
                                 getCaptchaRequest id
         | msg  -> msg |> fun x -> (x.Replace("OK|", ""))
                  
let rec AccountGen(x:int) =
        for i in 1 .. x do
            postCaptchaRequest() |> getCaptchaRequest |> printfn "%A"

let AccountGenAstnc n =
        async  {
                do postCaptchaRequest() |> getCaptchaRequest |> printfn "Task: %i %A" n
               }
let creatework(amounst:int) =
    [1..1] |> Seq.map AccountGenAstnc |> Async.Parallel |> Async.RunSynchronously

let rec ConsoleLoop() =
    match Console.ReadLine() |> fun x -> tryToInt x with
    | Some x    ->  printfn "Start creating %i sets of 5 accounts" x
                    for i in 1 .. x do
                         creatework(x) |> ignore
                    printfn "Finished"
                    ConsoleLoop()
    | None      -> printfn "This is not the correct value"
                   ConsoleLoop()
    
[<EntryPoint>]
let main argv = 
    ConsoleLoop()
    printfn "%A" argv
    0 // return an integer exit code
