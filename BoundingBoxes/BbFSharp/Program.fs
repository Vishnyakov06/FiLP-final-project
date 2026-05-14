open System
open System.Diagnostics

[<EntryPoint>]
let main _ =
    let n = Console.ReadLine().Trim() |> int

    let sw = Stopwatch.StartNew()
    let startMemory = GC.GetTotalMemory(true)

    let volume =
        Array.init n (fun _ ->
            Console.ReadLine().Trim().Split(' ')
            |> Array.map int
            |> Array.sort)
        |> Array.reduce (fun acc d ->
            [| min acc.[0] d.[0]
               min acc.[1] d.[1]
               min acc.[2] d.[2] |])
        |> Array.map int64
        |> Array.reduce ( * )

    let endMemory = GC.GetTotalMemory(true)
    sw.Stop()

    printfn "%d" volume

    let elapsedSec = float sw.ElapsedMilliseconds / 1000.0
    let usedMemoryMb = float (endMemory - startMemory) / 1024.0 / 1024.0

    printfn "\nВремя выполнения: %.3f сек" elapsedSec
    printfn "Использовано памяти: %.2f МБ" usedMemoryMb
    
    0