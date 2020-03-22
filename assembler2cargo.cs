private IMyInventory m_cargoInventory;
private IMyTextPanel m_panel;

private List<string> m_logs = new List<string>();

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;

    var cargo = (IMyCargoContainer)GridTerminalSystem.GetBlockWithName("LargeCargoContainer");
    m_cargoInventory = cargo.GetInventory(0);

    m_panel = (IMyTextPanel)GridTerminalSystem.GetBlockWithName("Text Pascal");
}

public void Main(string argument, UpdateType updateSource)
{
    var assemblers = new List<IMyAssembler>();
    var items = new List<MyInventoryItem>();

    GridTerminalSystem.GetBlocksOfType(assemblers, blockType => blockType is IMyAssembler);

    foreach (var assembler in assemblers)
    {
        var inventory = assembler.GetInventory(1);

        inventory.GetItems(items);

        foreach (var item in items)
        {
            var info = item.Type.GetItemInfo();

            if (info.IsIngot || info.IsOre)
                continue;

            if (m_cargoInventory.TransferItemFrom(inventory, item, item.Amount))
            {
                m_logs.Add(assembler.DisplayNameText + ": " + GetItemName(item.Type) + " (" + item.Amount + ")");
            }
        }
    }

    Print();
}

private void Print()
{
    while (m_logs.Count > 10)
    {
        m_logs.RemoveAt(0);
    }

    m_panel.WritePublicText(string.Join(Environment.NewLine, m_logs));
}

private static string GetItemName(MyItemType type)
{
    var str = type.ToString();
    var idx = str.IndexOf('/');
    return idx < 0
        ? str
        : str.Substring(idx + 1);
}