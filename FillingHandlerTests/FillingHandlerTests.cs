using FillingPointsHandler.Handler;
using FillingPointsHandler.Models;

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
        var stacked = new StackedWithScaleHandler("Supermarket", "4711", 2);

        //var start = new DateTime(2025, 1, 1, 13, 00, 00);
        //var end = new DateTime(2025, 1, 1, 15, 00, 00);

        var scaleValues = new List<TimeSeriesPoint>()
        {
            new()
            {
                Time =  new DateTime(2025, 1, 1, 13, 00, 00),
                Value = 0d,
            },
            new()
            {
                Time =  new DateTime(2025, 1, 1, 13, 05, 00),
                Value = 10d,
            },
            new()
            {
                Time =  new DateTime(2025, 1, 1, 13, 55, 00),
                Value = 100d,
            },
            new()
            {
                Time =  new DateTime(2025, 1, 1, 14, 30, 00),
                Value = 150d,
            },
            new()
            {
                Time =  new DateTime(2025, 1, 1, 15, 00, 00),
                Value = 180d,
            },
        };

        var result = stacked.Calc(scaleValues);

        Assert.AreEqual(3, result.Count);
    }
}