using Math;

namespace FillingHandlerTests;

[TestClass]
public sealed class MathTests
{
    [TestMethod]
    public void ScaleHelperTests()
    {
        DateTime? start;
        double delta;
        DateTime? end;
        var points = new List<Point>
        {
            new() { Time = DateTime.Parse("2024-03-05T12:00"), Value = 0.0 }, //0kg
            new() { Time = DateTime.Parse("2024-03-05T13:00"), Value = 100.0 }, //100kg
        };

        delta = 50d; // Ziel: 50 kg

        end = CounterHelper.FindEndTime(points, delta);

        Assert.AreEqual(DateTime.Parse("2024-03-05T12:30"), end);

        start = DateTime.Parse("2024-03-05T12:30");
        delta = 50d; // Ziel: 50 kg

        end = CounterHelper.FindEndTime(points, delta, start);

        Assert.AreEqual(DateTime.Parse("2024-03-05T13:00"), end);

        start = DateTime.Parse("2024-03-05T12:15:00");
        delta = 50d; // Ziel: 50 kg

        end = CounterHelper.FindEndTime(points, delta, start);

        Assert.AreEqual(DateTime.Parse("2024-03-05T12:45"), end);
    }
}