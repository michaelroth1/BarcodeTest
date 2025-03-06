namespace FillingPointsHandler.Models;

public class Gebinde
{
    public string Materaial { get; set; } = "";
    public string Batch { get; set; } = "";
    public DateTime Time { get; set; }
    public double Amount { get; set; }
    public int FillingPointId { get; set; }
    public int PhaseId { get; set; } = -1;
    public string CRID { get; set; } = "";
}