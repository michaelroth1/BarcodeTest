using FillingPointsHandler.Models;
using Math;
using PimsMock;
using PimsMock.Models;

namespace FillingPointsHandler.Handler;

public class StackedWithScaleHandler(string _id, string _crid, uint _phaseId) : IScaleHandler
{
    public List<Gebinde> Calc(DateTime start, DateTime end)
    {
        var stack = StackHelper
            .TryGetByTimeSpan(_id, start, end)
            .GebindeStack;

        var unprocessed = StackHelper.TryGetLastUnprocessed(_id, start, _crid, _phaseId);
        stack.InsertRange(0, unprocessed); //Letzte nicht verarbeitete Gebinde VOR Start hinzufügen        
                                           //stack.Add(StackHelper.TryGetNext(_id, end)); //Nächstes Gebinde NACH Ende hinzufügen

        var scaleValues = ScaleMock.ScaleValues; 
        var points = scaleValues.Select(tsp => new Point() { Time = tsp.Time, Value = tsp.Value }).ToList();
        //Preparing
        points.InsertPointByDateTime(start);
        points.InsertPointByDateTime(end);

        return Calc(stack, points);
    }

    public List<Gebinde> Calc(List<Gebinde> stack, List<Point> scaleValues)
    {
        var start = scaleValues.First().Time;

        var result = new List<Gebinde>();

        for (int i = 0; i < stack.Count; i++)
        {
            var totalAmount = stack[i].TotalAmount;

            var deltaTime = scaleValues.GetDeltaTime(totalAmount, start);

            var deltaAmount = scaleValues.GetDeltaAmount(start, start + deltaTime);

            //zuerst schrittweise integrieren bis zur Schranke T1 -> somit weiß ich wann der Sack leer ist
            result.Add(new Gebinde()
            {
                TotalAmount = totalAmount,
                Batch = stack[i].Batch,
                InsertionTime = start,
                EndTime = start + deltaTime,
                FillingPointId = stack[i].FillingPointId,
                Material = stack[i].Material,
                CRID = stack[i].CRID,
                PhaseId = stack[i].PhaseId,
                UsedAmount = deltaAmount,
            });

            start += deltaTime;
        }

        return result;
    }
}