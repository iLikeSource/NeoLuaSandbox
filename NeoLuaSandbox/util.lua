
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
