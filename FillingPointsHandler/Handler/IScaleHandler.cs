using FillingPointsHandler.Models;

namespace FillingPointsHandler.Handler;

public interface IScaleHandler
{
    List<Gebinde> Calc(List<TimeSeriesPoint> scaleValues);
}