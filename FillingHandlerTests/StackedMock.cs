using FillingPointsHandler;
using FillingPointsHandler.FillingPointTypes;
using FillingPointsHandler.Helpers;
using FillingPointsHandler.Models;

namespace FillingHandlerTests;

internal class StackedMock(DateTime _start, DateTime _end, string _crid, uint _phaseId) : IStackGetter
{
    public Stack GetStack()
    {
        var id = "TestCalculateStackedScaleScans";

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

        var stack = StackHelper
            .TryGetByTimeSpan(id, _start, _end)
            .GebindeStack;

        var unprocessed = StackHelper.TryGetLastUnprocessed(id, _start, _crid, _phaseId);
        stack.InsertRange(0, unprocessed); //Letzte nicht verarbeitete Gebinde VOR Start hinzufügen        
                                           //stack.Add(StackHelper.TryGetNext(_id, end)); //Nächstes Gebinde NACH Ende hinzufügen

        return new Stack()
        {
            Id = id,
            GebindeStack = stack,
        };
    }
}