// For more information see https://aka.ms/fsharp-console-apps
open System.IO
open System.Text.RegularExpressions
/// this will get the deepest directory name in a path like string
let dirname = Path.GetDirectoryName("./Industrial_Society.txt")

let filename = Path.GetFileName("./Industrial_Society.txt")

printfn $"{filename}, {dirname}\n"
printfn "Hello from F#"
let mutable leb = []
let rx = Regex(@"^\d+(\.|,) \w",RegexOptions.Compiled)
let rx2 = Regex(@"",RegexOptions.Compiled)
let lines = File.ReadLines(filename)
let mutable a = 0
let mutable s:string = ""
for i in lines do
    if rx.IsMatch(i) 
    then 
        s<-s + i 
        printfn $"-- {i[0..10]}"
    elif String.length i <1 && s.Length > 0
    then 
        leb<- [s] |> List.append leb 
        s <- ""
    elif String.length i >2 && s.Length >0
    then 
        s<-s+i
    else 
        printfn "??"

printfn $"{leb[leb.Length-3..leb.Length-1]}"
//for i in leb do
//    printfn $"{i}"
//lines |> Seq.iter(fun x -> if rx.IsMatch(x) then leb@x) 




