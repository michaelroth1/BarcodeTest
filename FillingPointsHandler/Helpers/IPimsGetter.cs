using PimsMock.Models;

namespace FillingPointsHandler.Helpers;

public interface IPimsGetter
{
    List<TimeSeriesPoint> GetScaleValues();
}