using FillingPointsHandler.Models;

namespace FillingPointsHandler.FillingPointTypes;

public class StackedController(string id) : FillingPoint(id)
{
    
    public override bool AllowScanning => true;

    public override void Open()
    {
        //nicht nötig -> es kann jederzeit ein Sack aufgegeben werden 
    }

    public override void Close()
    {
        //nicht nötig -> es kann jederzeit ein Sack aufgegeben werden 
    }


}