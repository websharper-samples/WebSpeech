namespace Site

open IntelliFactory.WebSharper

[<JavaScript>]
module Client =
    let All =
        let ( !+ ) x = Samples.Set.Singleton(x)

        Samples.Set.Create [
            !+ SpeechUtterance.Sample
            !+ SpeechSynthesis.Sample
        ]

    let Main = All.Show()