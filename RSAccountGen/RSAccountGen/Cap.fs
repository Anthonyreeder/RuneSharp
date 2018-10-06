module Cap

open FSharp.Data
open System
open System.Threading



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
let creatework() =
    [1..5] |> Seq.map AccountGenAstnc |> Async.Parallel |> Async.RunSynchronously
