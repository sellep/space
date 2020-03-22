//https://bloc97.github.io/SpaceEngineersModAPIDocs/html/8fcbd568-8be9-c0d3-1381-71f4076f2b0c.htm

private IMyTextPanel m_panel;

public Program()
{
	Runtime.UpdateFrequency = UpdateFrequency.Update10;
	
	m_panel = GridTerminalSystem.GetBlockWithName("Text Pascal") as IMyTextPanel;
}

public void Main(string argument, UpdateType updateSource)
{
	var sb = new StringBuilder();

	var blocks = new List<IMyProductionBlock>();
	GridTerminalSystem.GetBlocksOfType(blocks, blockType => blockType is IMyProductionBlock);

	blocks = blocks.OrderBy(b => b.DisplayNameText).ToList();

	foreach (var block in blocks)
	{
		sb.Append(block.DisplayNameText);
		sb.Append(": ");
		
		if (block.IsQueueEmpty)
		{
			sb.AppendLine("NOT WORKING!!! >.<");
		}
		else
		{
			sb.AppendLine("WORKING :)");
		}
	}

	m_panel.WritePublicText(sb.ToString());
}