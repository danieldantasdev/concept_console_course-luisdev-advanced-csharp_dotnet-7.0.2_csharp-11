namespace AdvancedCharp.Structures.Structs;

public struct ShelfLocationStruct : IEquatable<ShelfLocationStruct>
{
    public int Row { get; set; }
    public int Column { get; set; }

    public bool Equals(ShelfLocationStruct other)
    {
        return Row == other.Row && Column == other.Column;
    }
}