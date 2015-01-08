namespace Site

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.JavaScript
open IntelliFactory.WebSharper.Html.Client
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
module SpeechSynthesis =

    module Utils =
        let ToArray (a : ArrayLike<_>) =
            let rec helper n acc =
                if n = -1 then acc
                else helper (n - 1) (a.Item(n) :: acc)

            helper (a.Length - 1) []
            |> Array.ofList

    let MaxConfidence = Array.maxBy (fun (e : SpeechRecognitionAlternative) -> e.Confidence)

    let Main (e : Dom.Element) =

        let info = Div [ Text "Speak loud and clear." ]
        let pre = Pre [ Attr.Style "width: 550px; height: 300px; overflow-y: scroll; display: block;" ]

        let srecog = new SpeechRecognition ()
        srecog.Lang <- "en-US"
        srecog.Continuous <- true
        srecog.Onresult <-
            fun res ->
                (res.Results
                |> Utils.ToArray).[res.ResultIndex ..]
                |> Array.map Utils.ToArray
                |> Array.map MaxConfidence
                |> Array.iter (fun e ->
                                    pre.Text <- pre.Text + e.Transcript                             
                              )
                let height = pre.Dom?scrollHeight
                pre.Dom?scrollTop <- height

        let startBtn = Button [ Text "Start" ]
                        |>! OnClick (fun _ _ -> srecog.Start())
        let stopBtn = Button [ Text "Stop" ]
                        |>! OnClick (fun _ _ -> srecog.Stop())
        let m = Div [ info; pre; startBtn; stopBtn ]

        JQuery.Of(e).Append(m.Dom).Ignore

    let Sample = 
        Samples.Build()
            .Id("SpeechSynthesis")
            .FileName(__SOURCE_FILE__)
            .Keywords(["user media"; "speech synthesis"; "speech"; "speech-to-text"])
            .Render(Main)
            .Create()
