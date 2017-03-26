
-- table of model
nodeTable     = {};
memberTable   = {};
nodeIdTable   = {};
memberIdTable = {};


-- register node.
function node(tbl)
	table.insert(nodeTable, tbl);
	nodeIdTable[ tbl.name ] = #nodeTable;
end;

-- register member.
function member(tbl)
	table.insert(memberTable, tbl);
	memberIdTable[ tbl.name ] = #memberTable
end;

-- member length.
function length(member) 
	local node1 = nodeTable[ nodeIdTable[ member.nodes[1] ] ];
	local node2 = nodeTable[ nodeIdTable[ member.nodes[2] ] ];
	local x1 = node1.x;
	local y1 = node1.y;
	local z1 = node1.y;
	local x2 = node2.x;
	local y2 = node2.y;
	local z2 = node2.y;
	return ((x1 - x2) ^ 2.0 + (y1 - y2) ^ 2.0 + (z1 - z2) ^ 2.0) ^ 0.5;
end;


function convertNode(cmdTbl, node)
	cmdTbl.cmdNode(node.x, node.y, node.z);
end;

function convertNodes(cmdTbl)
	for _, node in ipairs(nodeTable) do
		convertNode(cmdTbl, node);	
	end
end;

function convertMember(cmdTbl, member)
	local nodeIds = {};
	for _, nodeName in ipairs(member.nodes) do
		local nodeId = nodeIdTable[nodeName];
		table.insert(nodeIds, nodeId);	
	end
	cmdTbl.cmdMember(nodeIds[1], nodeIds[2]);
end;

function convertMembers(cmdTbl)
	for _, member in ipairs(memberTable) do
		convertMember(cmdTbl, member);	
	end
end;
