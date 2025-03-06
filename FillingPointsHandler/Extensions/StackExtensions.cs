using FillingPointsHandler.Models;

namespace FillingPointsHandler.Extensions;

public static class StackExtensions
{
    public static List<Gebinde> GetLastUnprocessed(this TimeSeries timeSeries, DateTime date, string crid, uint phaseId)
    {
        var stack = timeSeries.Stack
            .Where(g => g.InsertionTime <= date)
            .Where(g => string.IsNullOrEmpty(g.CRID) || g.CRID == crid)
            .Where(g => g.PhaseId == -1 || g.PhaseId == phaseId)
            .OrderBy(g => g.InsertionTime)
            .ToList();

        return stack;
    }

    public static Gebinde GetNext(this TimeSeries timeSeries, DateTime date)
    {
        var gebinde = timeSeries.Stack
            .Where(g => g.InsertionTime >= date)
            .OrderBy(g => g.InsertionTime)
            .FirstOrDefault();

        gebinde ??= timeSeries.Stack.Last();

        if (gebinde == null)
        {
            throw new InvalidOperationException("There is no 'Next' Gebinde.");
        }

        return gebinde;
    }

    public static TimeSeries GetBetween(this TimeSeries timeSeries, DateTime start, DateTime end)
    {
        var stack = timeSeries.Stack
            .Where(g => g.InsertionTime >= start && g.InsertionTime <= end)
            .ToList();

        return new TimeSeries()
        {
            Id = timeSeries.Id,
            Stack = stack,
        };
    }
}