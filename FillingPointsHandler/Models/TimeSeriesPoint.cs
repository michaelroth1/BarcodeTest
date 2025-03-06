namespace FillingPointsHandler.Models;

public class TimeSeriesPoint
{
    public DateTime Time { get; set; }

    public double Value { get; set; }

    public int Quality { get; set; } = 0;
}