namespace Models

type Node (name:string, x:float, y:float, z:float) = 
    
    member __.Name with get () = name
    member __.X    with get () = x
    member __.Y    with get () = y 
    member __.Z    with get () = z 

type Member (name:string, nodes:Node array) = 
    
    member __.Name  with get () = name
    member __.Nodes with get () = nodes

type Model = 
    { mutable Nodes   : Map<string, Node>
      mutable Members : Map<string, Member> }
    with
    static member Create () = 
        { Nodes   = Map.empty 
          Members = Map.empty }
    member __.AddNode (name, x, y, z) = 
        __.Nodes <- 
            __.Nodes 
            |> Map.add name (new Node (name, x, y, z)) 
    
    member __.AddMember (name, refNames) =
        let nodes = 
            refNames 
            |> Array.map (fun x -> __.Nodes |> Map.find x) 
        __.Members <- 
            __.Members 
            |> Map.add name (new Member (name, nodes)) 
