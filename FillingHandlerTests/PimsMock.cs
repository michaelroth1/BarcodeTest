using FillingPointsHandler.Helpers;
using PimsMock;
using PimsMock.Models;

namespace FillingHandlerTests;

internal class PimsMock : IPimsGetter
{
    public List<TimeSeriesPoint> GetScaleValues()
    {
        var scaleValues = new List<TimeSeriesPoint>()
        {
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
        };

        return scaleValues;
    }
}