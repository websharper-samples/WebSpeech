namespace Site

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Html5
open IntelliFactory.WebSharper.JQuery

[<JavaScript>]
module SpeechUtterance =

    let Texts =
        [
            "Hey! How are you doing?", "en-US"
            "Hey! How are you doing?", "en-GB"
            "Hallo! Wie geht's?", "de-DE"
            "Ciao! Come và?", "it-IT"
            "こんにちはお元気ですか", "ja-JP"
        ]

    let MkField (sp : SpeechSynthesis) (u : SpeechSynthesisUtterance) =
        Texts
        |> List.map (fun elem ->
                        let text, lang = elem
                        let t = TextArea [ Attr.Width "200"; Text text ]
                        let b = Button [ Text "Speak!" ]
                                |>! OnClick (fun _ _ ->                                                     
                                                u.Text <- t.Value
                                                u.Lang <- lang

                                                sp.Speak(u)
                                            )

                        let style =
                            "margin: 5px; display: inline-block;"

                        Div [ Attr.Style style ] -< [ t; Tags.Br []; b ]
                    )

    let Main (e : Dom.Element) =
        let m = Div <| MkField (Window.SpeechSynthesis) (new SpeechSynthesisUtterance())

        JQuery.Of(e).Append(m.Dom).Ignore

    let Sample = 
        Samples.Build()
            .Id("SpeechUtterance")
            .FileName(__SOURCE_FILE__)
            .Keywords(["user media"; "speech utterance"; "speech"; "TTS"])
            .Render(Main)
            .Create()       