private IMyInventory m_cargoInventory;

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;

    var cargo = (IMyCargoContainer)GridTerminalSystem.GetBlockWithName("LargeCargoContainer");
    m_cargoInventory = cargo.GetInventory(0);
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
                Echo(assembler.DisplayNameText + ": " + GetItemName(item.Type) + " (" + item.Amount + ")");
            }
            //else
            //{
            //    Echo("transfer failed from " + assembler.DisplayNameText + ", " + item.Type.ToString());
            //}
        }
    }
}

private static string GetItemName(MyItemType type)
{
    var str = type.ToString();
    var idx = str.IndexOf('/');
    return idx < 0
        ? str
        : str.Substring(idx + 1);
}