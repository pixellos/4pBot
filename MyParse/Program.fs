// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open FParsec;
open FParsec.CharParsers;

type UserState = unit // doesn't have to be unit, of course
type Parser<'t> = Parser<'t, UserState>

let test p str = 
    match run p str with
    | Success(result,_,_) -> printfn "Success: %A" result
    | Failure(errorMsg,_,_) -> printfn "Failure %s" errorMsg

let str s = pstring s

let floatBetweenBrackes : Parser<_> = str "[" >>. pfloat .>> str "]"

type BotCommand = BCommand of string
                | BParameter of string


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    
    test pfloat "1.25"
    test floatBetweenBrackes "[1.22]"
    
    test (many (str "a" <|> str "b")) "abba"

    let s = Console.ReadLine();
    0 // return an integer exit code
