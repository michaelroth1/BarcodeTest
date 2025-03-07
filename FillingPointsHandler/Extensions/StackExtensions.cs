using FillingPointsHandler.Models;

namespace FillingPointsHandler.Extensions;

public static class StackExtensions
{
    public static List<Gebinde> Sort(this List<Gebinde> stack)
    {
        var result = stack
            .OrderBy(g => g.InsertionTime)
            .ToList();

        return result;
    }


    public static List<Gebinde> GetLastUnprocessed(this Stack stack, DateTime date, string crid, uint phaseId)
    {
        var result = stack.GebindeStack
            .Where(g => g.InsertionTime <= date)
            .Where(g => string.IsNullOrEmpty(g.CRID) || g.CRID == crid)
            .Where(g => g.PhaseId == -1 || g.PhaseId == phaseId)
            .OrderBy(g => g.InsertionTime)
            .ToList();

        return result;
    }

    public static Gebinde GetNext(this Stack timeSeries, DateTime date)
    {
        var gebinde = timeSeries.GebindeStack
            .Where(g => g.InsertionTime >= date)
            .OrderBy(g => g.InsertionTime)
            .FirstOrDefault();

        gebinde ??= timeSeries.GebindeStack.Last();

        if (gebinde == null)
        {
            throw new InvalidOperationException("There is no 'Next' Gebinde.");
        }

        return gebinde;
    }

    public static Stack GetBetween(this Stack stack, DateTime start, DateTime end)
    {
        var result = stack.GebindeStack
            .Where(g => g.InsertionTime >= start && g.InsertionTime <= end)
            .ToList();

        return new Stack()
        {
            Id = stack.Id,
            GebindeStack = result,
        };
    }
}