namespace Models

type Node (x:float, y:float, z:float) = 
    
    member __.X    with get () = x
    member __.Y    with get () = y 
    member __.Z    with get () = z 

type Nodes () =
    let mutable nodes : Node array = Array.empty
    member __.Item with get (id) = nodes.[id - 1]
    member __.Add (aNode) = 
        nodes <- Array.append nodes [| aNode |]
    
type Member (nodes:Node array) =
    
    new (node1, node2) = Member ([| node1; node2 |])
    
    member __.Nodes with get () = nodes

type Members () =
    let mutable members : Member array = [||]
    member __.Item with get (id) = members.[id - 1]
    member __.Add (aMember) = 
        members <- Array.append members [| aMember |]

type Model = 
    { mutable Nodes   : Nodes
      mutable Members : Members }
    with
    static member Create () = 
        { Nodes   = new Nodes () 
          Members = new Members () }
    member __.AddNode (x, y, z) = 
        __.Nodes.Add (new Node (x, y, z)) 
    
    member __.AddMember (refIds) =
        let nodes = 
            refIds 
            |> Array.map (fun id -> __.Nodes.[id]) 
        __.Members.Add (new Member (nodes)) 
    
    member __.AddMember (refId1, refId2) =
        __.AddMember [| refId1; refId2 |]