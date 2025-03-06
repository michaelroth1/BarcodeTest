using FillingPointsHandler.FillingPointTypes;
using FillingPointsHandler.Models;

namespace FillingHandlerTests;

[TestClass]
public sealed class BarcodingTests
{
    [TestMethod]
    public void AddScansInSupermarketMode()
    {
        var supermarket = new SupermarketController("Supermarket");

        supermarket.Open();

        supermarket.RequestScan(new Gebinde()
        {
            Materaial = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            Amount = 120,
            Batch = "4711",
            Time = new DateTime(2025, 1, 1, 11, 00, 00),
        });

        supermarket.RequestScan(new Gebinde()
        {
            Materaial = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            Amount = 120,
            Batch = "4711",
            Time = new DateTime(2025, 1, 1, 12, 00, 00),
        });

        supermarket.RequestScan(new Gebinde()
        {
            Materaial = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            Amount = 120,
            Batch = "0815",
            Time = new DateTime(2025, 1, 1, 14, 00, 00),
        });

        supermarket.RequestScan(new Gebinde()
        {
            Materaial = "00001",
            CRID = "4711",
            PhaseId = 2,
            FillingPointId = 1,
            Amount = 120,
            Batch = "0817",
            Time = new DateTime(2025, 1, 1, 16, 00, 00),
        });

        supermarket.Close();
    }
}