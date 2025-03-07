using FillingPointsHandler.Helpers;
using FillingPointsHandler.Models;
using Math;

namespace FillingPointsHandler.Handler;

public interface IScaleHandler
{
    IPimsGetter ScaleValueGetter { set; }
    
    IStackGetter StackGetter { set; }
}