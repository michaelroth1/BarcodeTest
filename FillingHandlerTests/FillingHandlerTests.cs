using FillingPointsHandler.Handler;

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
        var start = new DateTime(2025, 1, 1, 13, 00, 00);
        var end = new DateTime(2025, 1, 1, 15, 00, 00);

        var stacked = new StackedWithScaleHandler()
        {
            StackGetter  = new StackedMock(start, end, "4711", 2),
            ScaleValueGetter = new PimsMock(),
        };

        var result = stacked.Calc(start, end);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(120d, result[0].UsedAmount);
        Assert.AreEqual(60d, result[1].UsedAmount);
        Assert.AreEqual(0d, result[2].UsedAmount);
    }
}