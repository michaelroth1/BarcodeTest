namespace Math;

public class CounterHelper
{
    public static DateTime? FindEndTime(List<Point> points, double deltaValue, DateTime? startTime = null)
    {
        if (points.Count < 2) return null; //Kurve zu klein

        var clone = points.ToList();

        //Startpunkt setzen
        InsertStartPoint(clone, startTime);
        
        //Alles vor dem Startpunkt entfernen
        clone = TrimStart(clone, startTime);

        //Ab hier habe ich eine Liste in der Hand die von Punkt eins an durchsucht werden kann
        if (clone.Count == 0)
        {
            throw new InvalidOperationException("Something went wrong: perhaps is the 'starttime' not in list.");
        }

        var targetValue = clone[0].Value + deltaValue;
        var date = FindInterpolatedTime(clone, targetValue);

        return date == null
            ? throw new InvalidOperationException($"Something went wrong: targetValue '{targetValue}' not reachable in timeseries.")
            : date;
    }

    private static void InsertStartPoint(List<Point> points, DateTime? startTime)
    {
        if (startTime == null)
        {
            return;
        }

        //Liegt genau auf einem vorhanden Punkt, dann muss nichts getan werden
        //Liegt zwischen zwei Punkten, muss ein interpolierter Wert dazu genommen werden
        for (int i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];

            if (points[i].Time < startTime && startTime < points[i + 1].Time)
            {
                double valueDiff = p2.Value - p1.Value;
                double timeDiff = (p2.Time - p1.Time).TotalSeconds;
                double timeFraction = (startTime.Value - p1.Time).TotalSeconds / timeDiff;

                double startValue = p1.Value + timeFraction * valueDiff; // Interpolierter Zählerwert

                var pNew = new Point()
                {
                    Time = startTime.Value,
                    Value = startValue,
                };

                points.Insert(i + 1, pNew);
            }
        }
    }

    private static List<Point> TrimStart(List<Point> points, DateTime? startTime)
    {
        if (startTime == null)
        {
            return points;
        }

        var result = new List<Point>();

        foreach (var point in points)
        {
            if (point.Time >= startTime)
            {
                result.Add(point);
            }
        }

        return result;
    }

    private static DateTime? FindInterpolatedTime(List<Point> points, double targetValue)
    {
        if (points.Count < 2) return null;

        for (int i = 1; i < points.Count; i++)
        {
            double prevValue = points[i - 1].Value;
            double currValue = points[i].Value;

            // Prüfen, ob der Zielwert zwischen den beiden Werten liegt
            if (prevValue <= targetValue && currValue >= targetValue)
            {
                return InterpolateTime(points[i - 1], points[i], targetValue);
            }
        }

        return null; // Zielwert wurde nicht erreicht
    }

    // Helferfunktion für die lineare Interpolation zwischen zwei Punkten
    private static DateTime InterpolateTime(Point p1, Point p2, double targetValue)
    {
        double fraction = (targetValue - p1.Value) / (p2.Value - p1.Value);
        double interpolatedSeconds = (p2.Time - p1.Time).TotalSeconds * fraction;

        return p1.Time.AddSeconds(interpolatedSeconds);
    }
}