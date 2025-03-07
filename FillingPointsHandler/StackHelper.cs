using System.Xml;
using System.Xml.Serialization;
using FillingPointsHandler.Extensions;
using FillingPointsHandler.Models;

namespace FillingPointsHandler;

public class StackHelper //Äquivalent zur PI-SDK
{
    public static Gebinde TryGetNext(string id, DateTime date)
    {
        var ts = StackHelper.TryGet(id);

        return ts.GetNext(date)!;
    }

    public static List<Gebinde> TryGetLastUnprocessed(string id, DateTime date, string crid, uint phaseId)
    {
        var ts = StackHelper.TryGet(id);

        return ts.GetLastUnprocessed(date, crid, phaseId);
    }

    public static Stack TryGetByTimeSpan(string id, DateTime start, DateTime end)
    {
        var ts = StackHelper.TryGet(id);

        return ts.GetBetween(start, end);
    }

    public static Stack TryGet(string id)
    {
        try
        {
            return StackHelper.Get(id);
        }
        catch (Exception)
        {
            return new Stack()
            {
                Id = id,
                GebindeStack = [],
            };
        }
    }

    public static Stack Get(string id)
    {
        var ser = new XmlSerializer(typeof(Stack));

        if (File.Exists($@"Data/{id}.xml"))
        {
            using XmlReader reader = XmlReader.Create($@"Data/{id}.xml");
            var ts = (Stack)ser.Deserialize(reader)!;
            return ts;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    public static void Update(Stack timeSeries)
    {
        //File anlegen
        var ser = new XmlSerializer(typeof(Stack));
        TextWriter writer = new StreamWriter($@"Data/{timeSeries.Id}.xml");
        ser.Serialize(writer, timeSeries);
        writer.Close();
    }
}