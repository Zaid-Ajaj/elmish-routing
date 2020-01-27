module App

open Elmish
open Elmish.React
open Feliz
open Feliz.Router

type State =
    { CurrentUrl: string list }

type Msg =
    | UrlChanged of string list

let init() =
    { CurrentUrl = [ ] }

let update (msg: Msg) (state: State): State =
  match msg with
  | UrlChanged url ->
    { state with CurrentUrl = url }

let render (state: State) (dispatch: Msg -> unit) =
  let activePage =
    match state.CurrentUrl with
    | [ ] -> Html.h1 "Home"
    | [ "about" ] -> Html.h1 "About"
    | [ "contact" ] -> Html.h1 "Contact"
    | _ -> Html.h1 "Not Found"

  Router.router [
    Router.onUrlChanged (UrlChanged >> dispatch)
    Router.application [
      activePage
    ]
  ]

Program.mkSimple init update render
|> Program.withReactSynchronous "elmish-app"
|> Program.run