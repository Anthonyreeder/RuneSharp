module RuneSharp

let postCaptchaRequest() =
    Http.RequestString("http://2captcha.com/in.php", body = FormValues [    
    "key", "2bc08ce6750eb3ff4b9a6615529e8213";
    "method", "userrecaptcha";
    "googlekey", "6LccFA0TAAAAAHEwUJx_c1TfTBWMTAOIphwTtd1b";
    "pageurl", "https://secure.runescape.com/m=account-creation/create_account"]) |> fun x -> (x.Replace("OK|", ""))
