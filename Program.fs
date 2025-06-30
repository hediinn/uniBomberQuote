// For more information see https://aka.ms/fsharp-console-apps
open System.IO
open System.Text.RegularExpressions
open System.Collections.Generic
/// this will get the deepest directory name in a path like string

let filename = Path.GetFileName("./Industrial_Society.txt")

let lines = File.ReadAllLines(filename)|>List.ofSeq

let rx = Regex(@"^\d+(\.|,)",RegexOptions.Compiled)
let rx2 = Regex(@"\w+",RegexOptions.Compiled)

type typeOfSentence = |real =0|note=1|headers=2 
type sentenceStruct(liness:string,typeoFs:typeOfSentence)=
    let lines: string = liness
    let typeOfs: typeOfSentence = typeoFs
    member x.Lines:string = lines
    member x.TypeOfs: typeOfSentence = typeOfs

let startOfSentense (newline:string)= rx.IsMatch(newline) && newline.Length> 55

// 3470 birja notes

let checkIfUpper (newline:string) = 
    newline.Replace(' ','A') |> Seq.map(fun x -> if System.Char.IsUpper x then 1 else 0) |> Seq.sum


let findSentense (mLines:string list) = 
    let sentence = mLines |> Seq.reduce (fun s1 s2 -> s1+"\n"+s2)
    let sentence=sentence.Split("\n\n")
    let myStruct = sentence |> Seq.map (fun x -> 
                                            if startOfSentense x 
                                            then 
                                                new sentenceStruct(x, typeOfSentence.real)
                                            elif checkIfUpper x>x.Length-7 || x.Length <30
                                            then 
                                                new sentenceStruct(x, typeOfSentence.headers)
                                            else 
                                                new sentenceStruct(x, typeOfSentence.note)
                                        )

    myStruct
let newPrinter (mLines: sentenceStruct list) =
    let mutable currentHeader:string = "start"
    let newDickt = new Dictionary<string, string list>()
    mLines |> Seq.iteri(fun i x-> 
        if x.TypeOfs = typeOfSentence.headers
        then 
            currentHeader <- x.Lines
            newDickt.Add(x.Lines,[])
        elif x.TypeOfs = typeOfSentence.real
        then
            newDickt[currentHeader] <- newDickt[currentHeader]@[x.Lines] 
        else ()
        )
    newDickt
let myhead =lines |> findSentense|> Seq.toList
//myhead|> Seq.iteri(fun i x  -> if x.TypeOfs = typeOfSentence.headers then printfn $"{x.Lines} {i}")

let mydict = newPrinter myhead

let getWhereFrom: string = mydict.Keys |> Seq.filter(fun x -> x <> "Notes" ) |> Seq.randomChoice

printfn "%s" getWhereFrom

mydict |> Seq.iter( fun x  -> if x.Key = getWhereFrom then x.Value |> List.randomChoice |> fun y -> printfn "%s" y)