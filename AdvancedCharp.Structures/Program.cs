using AdvancedCharp.Structures.Enums;
using AdvancedCharp.Structures.Models;
using AdvancedCharp.Structures.Records;
using AdvancedCharp.Structures.Structs;

// Instances
var book1 = new Book(
    "Lord of The Rings",
    GenreEnum.Fantasy,
    new ShelfLocationStruct { Row = 1, Column = 1 },
    new BookDimensionsRecord(5f, 7.5f, 1.2f)
);

var book2 = new Book(
    "Clean Architecture",
    GenreEnum.Fantasy,
    new ShelfLocationStruct { Row = 3, Column = 2 },
    new BookDimensionsRecord(5f, 7.5f, 1.2f)
);

var book3 = new Book(
    "Clean Code",
    GenreEnum.Fantasy,
    new ShelfLocationStruct { Row = 2, Column = 2 },
    new BookDimensionsRecord(5f, 7.5f, 1.2f)
);

// DotLive 1: C# Moderno

Console.WriteLine(book2.LocationStruct.Equals(book3.LocationStruct));
Console.WriteLine(book2.DimensionsRecord.Equals(book3.DimensionsRecord));

Console.WriteLine(book1);
Console.WriteLine(book2);
Console.WriteLine(book3);

Console.ReadLine();