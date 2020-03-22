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
    var items = new List<MyProductionItem>();

    GridTerminalSystem.GetBlocksOfType(blocks, blockType => blockType is IMyProductionBlock);

    foreach (var block in blocks.OrderBy(b => b.DisplayNameText))
    {
        sb.Append(block.DisplayNameText);

        if (block.IsQueueEmpty)
        {
            sb.AppendLine(": -");
        }
        else
        {
            items.Clear();

            block.GetQueue(items);
            var item = items.First();

            sb.Append(": ");
            sb.AppendLine(item.BlueprintId.SubtypeName);
        }
    }

    m_panel.WritePublicText(sb.ToString());
}