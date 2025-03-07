using Math;

namespace FillingHandlerTests;

[TestClass]
public sealed class MathTests
{
    [TestMethod]
    public void ScaleHelperTests()
    {
        DateTime start;
        double delta;
        TimeSpan? end;
        var points = new List<Point>
        {
            new() { Time = DateTime.Parse("2024-03-05T12:00"), Value = 0.0 }, //0kg
            new() { Time = DateTime.Parse("2024-03-05T13:00"), Value = 100.0 }, //100kg
        };

        delta = 50d; // Ziel: 50 kg
        start = points.First().Time;

        end = points.GetDeltaTime(delta);

        Assert.AreEqual(DateTime.Parse("2024-03-05T12:30"), start + end!.Value);

        start = DateTime.Parse("2024-03-05T12:30");
        delta = 50d; // Ziel: 50 kg

        end = points.GetDeltaTime(delta, start);

        Assert.AreEqual(DateTime.Parse("2024-03-05T13:00"), start + end!.Value);

        start = DateTime.Parse("2024-03-05T12:15:00");
        delta = 50d; // Ziel: 50 kg

        end = points.GetDeltaTime(delta, start);

        Assert.AreEqual(DateTime.Parse("2024-03-05T12:45"), start + end!.Value);

        
    }

    [TestMethod]
    public void ScaleHelperWithOneResetTest()
    {
        DateTime start;
        double delta;
        TimeSpan? end;

        start = DateTime.Parse("2024-03-05T12:00");
        delta = 150d; // Ziel: 50 kg

        //Rücksetzer testen
        var points = new List<Point>
        {
            new() { Time = DateTime.Parse("2024-03-05T12:00"), Value = 0.0 }, //0kg
            new() { Time = DateTime.Parse("2024-03-05T13:00"), Value = 100.0 }, //100kg
            new() { Time = DateTime.Parse("2024-03-05T13:01"), Value = 0 }, //100kg
            new() { Time = DateTime.Parse("2024-03-05T14:00"), Value = 0 }, //100kg
            new() { Time = DateTime.Parse("2024-03-05T15:00"), Value = 100.0 }, //100kg
        };

        end = points.GetDeltaTime(delta, start);

        Assert.AreEqual(DateTime.Parse("2024-03-05T14:30"), start + end!.Value);
    }
}