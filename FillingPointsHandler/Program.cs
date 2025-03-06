// See https://aka.ms/new-console-template for more information
using FillingPointsHandler.FillingPointTypes;
using FillingPointsHandler.Handler;
using FillingPointsHandler.Models;

var mode = "scan";
Console.WriteLine($"Mode: {mode}");

if (mode == "scan")
{
    var supermarket = new SupermarketController("Test");

    supermarket.Open();

    supermarket.RequestScan(new Gebinde()
    {
        Material = "00001",
        TotalAmount = 120,
        Batch = "4711",
        InsertionTime = new DateTime(2025, 1, 1, 11, 00, 00),
    });

    supermarket.RequestScan(new Gebinde()
    {
        Material = "00001",
        TotalAmount = 120,
        Batch = "0815",
        InsertionTime = new DateTime(2025, 1, 1, 14, 00, 00),
    });

    supermarket.RequestScan(new Gebinde()
    {
        Material = "00001",
        TotalAmount = 120,
        Batch = "0817",
        InsertionTime = new DateTime(2025, 1, 1, 16, 00, 00),
    });

    supermarket.Close();
}
else if(mode == "calc")
{
    var supermarket = new SupermarketHandler("Test");

    var start = new DateTime(2025, 1, 1, 13, 00, 00);
    var end = new DateTime(2025, 1, 1, 17, 00, 00);
    var _ = supermarket.Calc(start, end);

}