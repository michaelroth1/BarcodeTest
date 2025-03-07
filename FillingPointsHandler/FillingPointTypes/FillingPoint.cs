using FillingPointsHandler.Models;

namespace FillingPointsHandler.FillingPointTypes;

public abstract class FillingPoint(string id) : IFillingPoint
{
    private readonly string _id = id;

    public virtual bool AllowScanning { get; set; }

    public virtual void Close()
    {
        AllowScanning = false;
    }

    public virtual void Open()
    {
        AllowScanning = true;
    }

    public void RequestScan(Gebinde gebinde)
    {
        if (AllowScanning)
        {
            Stack ts = StackHelper.TryGet(_id);

            ts.InsertOrUpdateGebinde(gebinde);

            StackHelper.Update(ts);
        }
        else
        {
            throw new Exception("Scanning not allowed");
        }
    }
}