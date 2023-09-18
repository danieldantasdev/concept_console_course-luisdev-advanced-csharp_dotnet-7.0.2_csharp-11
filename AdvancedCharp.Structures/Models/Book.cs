using AdvancedCharp.Structures.Enums;
using AdvancedCharp.Structures.Records;
using AdvancedCharp.Structures.Structs;

namespace AdvancedCharp.Structures.Models;

public class Book
{
    public Book(string title, GenreEnum genre, ShelfLocationStruct locationStruct,
        BookDimensionsRecord dimensionsRecord)
    {
        Title = title;
        GenreEnum = genre;
        LocationStruct = locationStruct;
        DimensionsRecord = dimensionsRecord;
    }

    public string Title { get; set; }
    public GenreEnum GenreEnum { get; set; }
    public ShelfLocationStruct LocationStruct { get; set; }
    public BookDimensionsRecord DimensionsRecord { get; set; }
}