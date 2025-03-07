using FillingPointsHandler.Helpers;
using FillingPointsHandler.Models;
using Math;
using PimsMock.Models;

namespace FillingPointsHandler.Handler;

public class StackedWithScaleHandler() : IFillingPointHandler, IScaleHandler
{
    public IPimsGetter ScaleValueGetter { get; set; } = null!;

    public IStackGetter StackGetter { get; set; } = null!;
       
    public List<Gebinde> Calc(DateTime start, DateTime end)
    {
        var scaleValues = ScaleValueGetter.GetScaleValues();
        
        var stack = StackGetter.GetStack();

        return Calc(start, stack, scaleValues);
    }

    private static List<Gebinde> Calc(DateTime start, Stack stack, List<TimeSeriesPoint> scaleValues)
    {
        var points = scaleValues
            .Select(tsp => new Point() { Time = tsp.Time, Value = tsp.Value })
            .ToList()
            .InsertPointByDateTime(start); //Preparing

        var result = new List<Gebinde>();

        for (int i = 0; i < stack.GebindeStack.Count; i++)
        {
            var totalAmount = stack.GebindeStack[i].TotalAmount;

            var deltaTime = points.GetDeltaTime(totalAmount, start);

            var deltaAmount = points.GetDeltaAmount(start, start + deltaTime);

            //zuerst schrittweise integrieren bis zur Schranke T1 -> somit weiß ich wann der Sack leer ist
            result.Add(new Gebinde()
            {
                TotalAmount = totalAmount,
                Batch = stack.GebindeStack[i].Batch,
                InsertionTime = start,
                EndTime = start + deltaTime,
                FillingPointId = stack.GebindeStack[i].FillingPointId,
                Material = stack.GebindeStack[i].Material,
                CRID = stack.GebindeStack[i].CRID,
                PhaseId = stack.GebindeStack[i].PhaseId,
                UsedAmount = deltaAmount,
            });

            start += deltaTime;
        }

        return result;
    }
}