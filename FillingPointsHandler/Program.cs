// See https://aka.ms/new-console-template for more information
using FillingPointsHandler.FillingPointTypes;
using FillingPointsHandler.Models;

Console.WriteLine("Hello, World!");

var supermarket = new SupermarketController("Test");

supermarket.Open();

supermarket.RequestScan(new Gebinde()
{
    Materaial = "00001",
    Amount = 120,
    Batch = "4711",
    Time = new DateTime(2025, 1, 1, 12, 00, 00),
});

supermarket.RequestScan(new Gebinde()
{
    Materaial = "00001",
    Amount = 120,
    Batch = "0815",
    Time = new DateTime(2025, 1, 1, 14, 00, 00),
});

supermarket.RequestScan(new Gebinde()
{
    Materaial = "00001",
    Amount = 120,
    Batch = "0817",
    Time = new DateTime(2025, 1, 1, 16, 00, 00),
});
