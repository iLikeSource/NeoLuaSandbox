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

(*
/// オブジェクトを生成して返す
type CmdNode   = delegate of string * float * float * float -> unit 
type CmdMember = delegate of string * (string array) -> unit
let testObject () = 
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    let t = new LuaTable ()
    
    let model     = Models.Model.Create ()
    let cmdNode = 
        new CmdNode (fun name x y z -> model.AddNode (name, x, y, z))
    let cmdMember = 
        new CmdMember (fun name refNames -> model.AddMember (name, refNames))
    let _ = t.DefineFunction ("cmdNode"  , cmdNode)
    let _ = t.DefineFunction ("cmdMember", cmdMember)
    
    g.["cmd"] <- t

    let cmdMember = model.AddMember
    let script = """
function node(tbl)
    return cmd.cmdNode(tbl.name, tbl.x, tbl.y, tbl.z);    
end;

node( { name = "n1", x = 0.0, y = 1.0, z = 2.0 } ); 
return 1;
    """
    let _ = g.DoChunk (script, "object.lua")
    let node   = model.Nodes |> Map.find "n1"
    ()   
*)

///  Luaからテーブルで返してもらった値を取り扱う
let testTableReturn () = 
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    let result  = g.DoChunk ("return { { 1, 1, 12.0 }, { 1, 1, 1.0 } };", "test.lua")
    let luaTable = result.[0] :?> LuaTable;
    let a = luaTable.[1]
    let b = luaTable.[2]
    () 
    

///  ファイルから読み込み
type CmdNode   = delegate of float * float * float -> unit 
type CmdMember = delegate of int * int -> unit
let testUtility () = 
    use reader  = new System.IO.StreamReader ("util.lua")
    let t       = new LuaTable ()
    let code    = reader.ReadToEnd ()
    let model   = Models.Model.Create ()
    let cmdNode = 
        new CmdNode (fun x y z -> model.AddNode (x, y, z))
    
    let cmdMember = 
        new CmdMember (fun nodeId1 nodeId2 -> 
            model.AddMember (nodeId1, nodeId2)
        )
    
    let _ = t.DefineFunction ("cmdNode"  , cmdNode)
    let _ = t.DefineFunction ("cmdMember", cmdMember)
    let l = new Lua ()
    let g = l.CreateEnvironment ()
    g.["cmd"] <- t
    let mainCode = """
node({ name="n1", x=1.0, y=2.0, z=10.0}); 
node({ name="n2", x=3.0, y=2.0, z=10.0}); 
node({ name="n3", x=3.0, y=4.0, z=10.0}); 
member({ name="m1", nodes={"n1","n2"}});
member({ name="m2", nodes={"n1","n3"}});

convertNodes( cmd );
convertMembers( cmd );

    """
    let result  = g.DoChunk (code + "\r\n" + mainCode, "usingUtil.lua")
    let node1   = model.Nodes.[1]
    let node2   = model.Nodes.[2]
    let member1 = model.Members.[1]
    let member2 = model.Members.[2]
    ()   
    


    
    
     
