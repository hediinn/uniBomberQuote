// For more information see https://aka.ms/fsharp-console-apps
open System.IO
open System.Text.RegularExpressions
/// this will get the deepest directory name in a path like string
let dirname = Path.GetDirectoryName("./Industrial_Society.txt")

let filename = Path.GetFileName("./Industrial_Society.txt")

printfn $"{filename}, {dirname}\n"
printfn "Hello from F#"
let mutable leb = []
let rx = Regex(@"^\d+\. ",RegexOptions.Compiled)
let lines = File.ReadLines(filename)
let a = 0
for i in lines do 
    if rx.IsMatch(i) then printfn "lol"
    else leb <- [i]|>  List.append leb
//for i in leb do
//    printfn $"{i}"
//lines |> Seq.iter(fun x -> if rx.IsMatch(x) then leb@x) 




