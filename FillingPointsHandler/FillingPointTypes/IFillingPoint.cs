using FillingPointsHandler.Models;

namespace FillingPointsHandler.FillingPointTypes;

public interface IFillingPoint
{
    void Open(); //Erlaubt das Aufgeben von Gebinden auf der Aufgabestelle

    void Close(); //Beednet das Aufgeben von Gebinden auf der Aufgabestelle

    bool AllowScanning { get; }

    void RequestScan(Gebinde scan);
}