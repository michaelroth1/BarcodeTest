using FillingPointsHandler.Models;

namespace FillingPointsHandler.Handler;

public interface IFillingPointHandler
{
    List<Gebinde> Calc(DateTime start, DateTime end);
}