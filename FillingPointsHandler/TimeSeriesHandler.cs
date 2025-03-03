using FillingPointsHandler.Models;
using System.Xml;
using System.Xml.Serialization;

namespace FillingPointsHandler;

class TimeSeriesHandler
{
    public static TimeSeries Get(string id)
    {
        var ser = new XmlSerializer(typeof(TimeSeries));

        if (File.Exists($@"Data/{id}.xml"))
        {
            using XmlReader reader = XmlReader.Create($@"Data/{id}.xml");
            var ts = (TimeSeries)ser.Deserialize(reader)!;
            return ts;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    public static void Create(TimeSeries timeSeries)
    {
        //File anlegen
        var ser = new XmlSerializer(typeof(TimeSeries));
        TextWriter writer = new StreamWriter($@"Data/{timeSeries.Id}.xml");
        ser.Serialize(writer, timeSeries);
        writer.Close();
    }

    public static void Update(TimeSeries timeSeries)
    {
        //File anlegen
        var ser = new XmlSerializer(typeof(TimeSeries));
        TextWriter writer = new StreamWriter($@"Data/{timeSeries.Id}.xml");
        ser.Serialize(writer, timeSeries);
        writer.Close();
    }


}