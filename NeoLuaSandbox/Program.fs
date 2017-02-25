// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    //let x = Sandbox.testFunction ()
    let x = Sandbox.testFromFile (1.0, 2.0, 3.0)
    let y = Sandbox.testFromFile (10.0, 2.0, 3.0)
    0 // 整数の終了コードを返します
