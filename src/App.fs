module App

open Browser.Dom
open Fetch
open Thoth.Fetch
open Feliz

[<ReactComponent>]
let Counter () =
    let (count, setCount) = React.useState (0)

    Html.div [ Html.h1 count
               Html.button [ prop.text "Increment"
                             prop.onClick (fun _ -> setCount (count + 1)) ] ]

[<ReactComponent>]
let Message () =
    let (message, setMessage) = React.useState ("")

    Html.div [ Html.button [ prop.text "Get a message from the API"
                             prop.onClick
                                 (fun _ ->
                                     promise {
                                         let! message =
                                             Fetch.get (
                                                 "/api/GetMessage?name=FSharp",
                                                 headers = [ HttpRequestHeaders.Accept "application/json" ]
                                             )

                                         setMessage message
                                         return ()
                                     }
                                     |> ignore) ]
               if message = "" then
                   Html.none
               else
                   Html.p message ]

[<ReactComponent>]
let App () = React.fragment [ Counter(); Message() ]

open Browser.Dom

ReactDOM.render (App(), document.getElementById "root")
