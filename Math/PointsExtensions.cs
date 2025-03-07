namespace Math;

public static class PointsExtensions
{
    public static double GetDeltaAmount(this List<Point> points, DateTime startTime, DateTime endtime)
    {
        if (points.Count < 2)
        {
            throw new InvalidOperationException("Too less points."); //Kurve hat zu wenig Stützpunkte
        }

        var clone = points.ToList();

        //eine streng Monoton steigende Kurve daraus machen
        clone = clone.TransformToMonotonicIncreasing();

        //Startpunkt setzen
        clone = clone.InsertPointByDateTime(startTime);

        //Startpunkt setzen
        clone = clone.InsertPointByDateTime(endtime);

        var startValue = clone.Single(p => p.Time == startTime).Value;

        var endValue = clone.Single(p => p.Time == endtime).Value;

        return endValue - startValue;
    }

    public static TimeSpan GetDeltaTime(this List<Point> points, double deltaValue, DateTime? startTime = null)
    {
        if (points.Count < 2)
        {
            throw new InvalidOperationException("Too less points."); //Kurve hat zu wenig Stützpunkte
        }

        if (startTime == null)
        {
            startTime = points.First().Time;
        }

        List<Point> clone = [.. points];

        clone = PrepareStart(clone, startTime);

        //eine streng monoton steigende Kurve daraus machen
        clone = clone.TransformToMonotonicIncreasing();

        //Ab hier habe ich eine Liste in der Hand die von Punkt eins an durchsucht werden kann
        if (clone.Count == 0)
        {
            throw new InvalidOperationException("Something went wrong: perhaps is the 'starttime' not in list.");
        }

        var targetValue = clone[0].Value + deltaValue;
        var date = clone.FindInterpolatedTime(targetValue);

        return date == null
            ? points.Last().Time - startTime.Value //Gebinde wurde nicht vollständig in Zeitreihe aufgegeben
            : date.Value - startTime.Value;
    }

    /// <summary>
    /// Startpunkt setzen
    /// Alles vor dem Startpunkt entfernen
    /// </summary>
    private static List<Point> PrepareStart(this List<Point> clone, DateTime? startTime)
    {        
        clone = clone.InsertPointByDateTime(startTime);
        
        clone = clone.TrimStart(startTime);

        return clone;
    }

    public static List<Point> InsertPointByDateTime(this List<Point> points, DateTime? startTime)
    {
        List<Point> clone = [.. points];

        //Liegt genau auf einem vorhanden Punkt, dann muss nichts getan werden
        //Liegt zwischen zwei Punkten, muss ein interpolierter Wert dazu genommen werden
        for (int i = 0; i < clone.Count - 1; i++)
        {
            var p1 = clone[i];
            var p2 = clone[i + 1];

            if (clone[i].Time < startTime && startTime < clone[i + 1].Time)
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

                clone.Insert(i + 1, pNew);
            }
        }

        return clone;
    }

    public static List<Point> TrimStart(this List<Point> points, DateTime? startTime)
    {
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

    public static List<Point> TransformToMonotonicIncreasing(this List<Point> points)
    {
        if (points.Count < 1) return [];

        var result = new List<Point>()
        {
            new()
            {
                Value = points.First().Value,
                Time = points.First().Time
            }
        };

        for (int i = 1; i < points.Count; i++)
        {
            var p1 = points[i - 1];
            var p2 = points[i];
            var steigung = p2.Value - p1.Value;

            if (steigung < 0)
            {
                steigung = 0;
            }

            result.Add(new Point() { Value = result.Last().Value + steigung, Time = p2.Time });
        }

        return result;
    }

    private static DateTime? FindInterpolatedTime(this List<Point> points, double targetValue)
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

    /// <summary>
    /// Helferfunktion für die lineare Interpolation zwischen zwei Punkten
    /// </summary>
    private static DateTime? InterpolateTime(Point p1, Point p2, double targetValue)
    {
        double fraction = (targetValue - p1.Value) / (p2.Value - p1.Value);
        var interpolated = (p2.Time - p1.Time).Multiply(fraction);

        return p1.Time.Add(interpolated);
    }
}