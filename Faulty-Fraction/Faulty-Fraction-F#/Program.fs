open System
open System.Numerics
open System.Diagnostics

let check (s: string) (c: BigInteger) (la: int) =
    if la <= 0 || la >= s.Length then None
    else
        let aStr = s.Substring(0, la)
        let bStr = s.Substring(la)
        if bStr.[0] = '0' && bStr.Length > 1 then None
        else
            let a = BigInteger.Parse(aStr)
            let b = BigInteger.Parse(bStr)
            if a = b * c then Some(aStr + " " + bStr) else None

let solve (input: string) =
    let parts = input.Split()
    let s, cStr = parts.[0], parts.[1]
    let c = BigInteger.Parse(cStr)
    let ls, lc = s.Length, cStr.Length
    
    [ (ls + lc) / 2; (ls + lc) / 2 - 1; (ls + lc) / 2 + 1 ]
    |> List.choose (check s c)
    |> List.tryHead
    |> Option.defaultValue "Не найдено"


let tests = [ "42 2"; "2025225 9"; "239239239 1001"; "123123 1"; "1010 1"; "8421 4"; 
"400200 2"; "10000000001 1000000000"; "11 1"; "102102 1"; "999999999999 1"]

tests |> List.iter (fun t -> 
    GC.Collect()
    let startMemory = GC.GetTotalMemory(true)
    let sw = Stopwatch.StartNew()
    
    let result = solve t
    
    sw.Stop()
    let endMemory = GC.GetTotalMemory(false)
    
    printfn "Вход: %s -> Выход: %s" t result
    eprintfn "Выход: %f s" sw.Elapsed.TotalSeconds
    eprintfn "Память: %f MB" (float(endMemory - startMemory) / 1024.0 / 1024.0)
    eprintfn "-------------------"
)