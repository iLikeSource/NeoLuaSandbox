module Sandbox

open Neo.IronLua


///  変数を渡して実行
let testVariable () = 
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    g.["a"] <- 1.0
    g.["b"] <- 2.0
    let result  = g.DoChunk ("return a + b;", "test.lua")
    System.Convert.ToDouble result.[0]

///  ファイルから読み込み
let testFromFile (at, ft, j) = 
    use reader = new System.IO.StreamReader ("test1.lua")
    let code   = reader.ReadToEnd ()
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    g.["at"] <- at 
    g.["ft"] <- ft 
    g.["j"]  <- j
    let result  = g.DoChunk (code + "\r\n" + "return Ma()", "test.lua")
    System.Convert.ToDouble result.[0]
    
    

///  関数を渡す
type delA = delegate of float -> float 
let testFunction () = 
    let f = new delA (fun x -> x * 2.0)
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    let t = new LuaTable ()
    let _ = t.DefineFunction ("f1", f)
    g.["t"] <- t
    let result  = g.DoChunk ("return t.f1(10.0);", "test.lua")
    System.Convert.ToDouble result.[0]


    
    
     
