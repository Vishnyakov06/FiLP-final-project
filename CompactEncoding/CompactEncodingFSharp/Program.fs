open System
open System.Diagnostics

let rec extractChunks (n: int64) (acc: int list) =
    match n with
    | 0L -> acc
    | _ -> 
        let chunk = int (n &&& 0x7FL)
        let nextN = n >>> 7
        extractChunks nextN (chunk :: acc)

let rec setContinuationBits (chunks: int list) =
    match chunks with
    | [] -> []
    | [last] -> [last]
    | head :: tail -> 
        let modifiedHead = head ||| 128
        modifiedHead :: setContinuationBits tail

let encodeCompact (n: int64) =
    match n with
    | 0L -> [0]
    | _ -> extractChunks n [] |> setContinuationBits

[<EntryPoint>]
let main _ =
    let input = Console.ReadLine()
    
    GC.Collect()
    let startMem = GC.GetTotalMemory(true)
    let timer = Stopwatch.StartNew()

    match String.IsNullOrWhiteSpace(input) with
    | true -> ()
    | false ->
        let n = Int64.Parse(input)
        let result = encodeCompact n
        printfn "%s" (String.Join(" ", result))

    timer.Stop()
    let endMem = GC.GetTotalMemory(false)
    eprintfn "Time: %fs" timer.Elapsed.TotalSeconds
    eprintfn "Memory: %fMB" (float(endMem - startMem) / 1024.0 / 1024.0)
    0