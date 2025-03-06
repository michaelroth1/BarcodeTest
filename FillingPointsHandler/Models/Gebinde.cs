namespace FillingPointsHandler.Models;

public class Gebinde
{
    public string Material { get; set; } = "";
    public string Batch { get; set; } = "";
    public DateTime InsertionTime { get; set; }
    public DateTime? EndTime { get; set; }
    public double TotalAmount { get; set; }
    public int FillingPointId { get; set; }
    public int PhaseId { get; set; } = -1;
    public string CRID { get; set; } = "";
    public double UsedAmount { get; set; } = 0d; //verbrauchte Menge
}