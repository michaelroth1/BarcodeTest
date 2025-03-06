using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using FillingPointsHandler.Models;
using Math;

namespace FillingPointsHandler.Handler;

public class StackedWithScaleHandler(string _id, string _crid, uint _phaseId) : IScaleHandler
{
    public List<Gebinde> Calc(List<TimeSeriesPoint> scaleValues)
    {
        var start = scaleValues.First().Time;
        var end = scaleValues.Last().Time;

        var stack = StackHelper
            .TryGetByTimeSpan(_id, start, end)
            .Stack;

        var unprocessed = StackHelper.TryGetLastUnprocessed(_id, start, _crid, _phaseId);
        stack.InsertRange(0, unprocessed); //Letzte nicht verarbeitete Gebinde VOR Start hinzufügen        
        stack.Add(StackHelper.TryGetNext(_id, end)); //Nächstes Gebinde NACH Ende hinzufügen

        var startValue = unprocessed.Sum(g => g.Amount);

        var points = scaleValues.Select(tsp => new Point() { Time = tsp.Time, Value = tsp.Value }).ToList();

        for (int i = 0; i < stack.Count; i++)
        {
            var deltaValue = stack[i].Amount;



            var date = CounterHelper.FindEndTime(points, deltaValue, start);
            //zuerst schrittweise integrieren bis zur Schranke T1 -> somit weiß ich wann der Sack leer ist

            //Hier muss eine neue Liste erstellt werden

            start = date.Value;
        }

        return null;
    }
}