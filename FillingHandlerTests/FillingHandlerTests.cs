using FillingPointsHandler.FillingPointTypes;
using FillingPointsHandler.Handler;
using FillingPointsHandler.Models;
using PimsMock;
using PimsMock.Models;

namespace FillingHandlerTests;

[TestClass]
public sealed class FillingHandlerTests
{
    [TestMethod]
    public void CalculateSupermarketScans()
    {
        var supermarket = new SupermarketHandler("Supermarket");

        var start = new DateTime(2025, 1, 1, 13, 00, 00);
        var end = new DateTime(2025, 1, 1, 17, 00, 00);
        var result = supermarket.Calc(start, end);

        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void CalculateStackedScans()
    {
        var supermarket = new SupermarketHandler("Supermarket");

        var start = new DateTime(2025, 1, 1, 00, 00, 00);
        var end = new DateTime(2025, 1, 1, 15, 00, 00);
        var result = supermarket.Calc(start, end);

        Assert.AreEqual(3, result.Count);
    }

    [TestMethod]
    public void CalculateStackedScaleScans()
    {
        var id = "TestCalculateStackedScaleScans";

        var start = new DateTime(2025, 1, 1, 13, 00, 00);
        var end = new DateTime(2025, 1, 1, 15, 00, 00);

        ScaleMock.ScaleValues =
        [
            new()
            {
                 Time = new DateTime(2025, 1, 1, 13, 00, 00),
                Value = 0d,
                Quality = 1,
            },
            new()
            {
                Time = new DateTime(2025, 1, 1, 13, 05, 00),
                Value = 10d,
                Quality = 1,
            },
            new()
            {
                Time = new DateTime(2025, 1, 1, 13, 55, 00),
                Value = 100d,
                Quality = 1,
            },
            new()
            {
                Time = new DateTime(2025, 1, 1, 14, 30, 00),
                Value = 150d,
                Quality = 1,
            },
            new()
            {
                Time = new DateTime(2025, 1, 1, 15, 00, 00),
                Value = 180d,
                Quality = 1,
            },
        ];

        var scannig = new SupermarketController(id);

        scannig.Open();

        scannig.RequestScan(new Gebinde()
        {
            Material = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            TotalAmount = 120,
            Batch = "4711",
            InsertionTime = new DateTime(2025, 1, 1, 11, 00, 00),
        });

        scannig.RequestScan(new Gebinde()
        {
            Material = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            TotalAmount = 120,
            Batch = "4711",
            InsertionTime = new DateTime(2025, 1, 1, 12, 00, 00),
        });

        scannig.RequestScan(new Gebinde()
        {
            Material = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            TotalAmount = 120,
            Batch = "0815",
            InsertionTime = new DateTime(2025, 1, 1, 14, 00, 00),
        });

        scannig.RequestScan(new Gebinde()
        {
            Material = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            TotalAmount = 120,
            Batch = "0817",
            InsertionTime = new DateTime(2025, 1, 1, 16, 00, 00),
        });

        scannig.Close();

        var stacked = new StackedWithScaleHandler(id, "4711", 2);
        var result = stacked.Calc(start, end);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(120d, result[0].UsedAmount);
        Assert.AreEqual(60d, result[1].UsedAmount);
        Assert.AreEqual(0d, result[2].UsedAmount);
    }
}