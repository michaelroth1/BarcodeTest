using FillingPointsHandler.Models;

namespace FillingPointsHandler.Handler;

public class StackedHandler(string _id) : IFillingPointHandler
{
    public List<Gebinde> Calc(DateTime start, DateTime end)
    {
        //FIFO
        return StackHelper
            .TryGetByTimeSpan(_id, start, end)
            .GebindeStack;
    }
}