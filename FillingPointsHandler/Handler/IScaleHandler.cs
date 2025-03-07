using FillingPointsHandler.Models;
using Math;

namespace FillingPointsHandler.Handler;

public interface IScaleHandler
{
    List<Gebinde> Calc(DateTime start, DateTime end);

    List<Gebinde> Calc(List<Gebinde> stack, List<Point> scaleValues);
}