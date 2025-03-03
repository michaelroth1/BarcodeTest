using System.Globalization;

namespace FillingPointsHandler.Models;

public class TimeSeries
{
    public string Id { get; set; } = null!;

    public List<Gebinde> Scans { get; set; } = [];

    public void InsertOrUpdateGebinde(Gebinde gebinde)
    {
        var exist = this.Scans.SingleOrDefault(g => g.Time == gebinde.Time);

        if (exist == null)
        {
            this.InsertGebinde(gebinde);
        }
        else
        {
            this.UpdateGebinde(gebinde);
        }
    }

    private void InsertGebinde(Gebinde gebinde)
    {
        this.Scans.Add(gebinde);

        this.Sort();
    }

    private void UpdateGebinde(Gebinde gebinde)
    {
        var exist = this.Scans.Single(g => g.Time == gebinde.Time);

        this.Scans.Remove(exist);

        InsertGebinde(gebinde);

        this.Sort();
    }

    private void Sort()
    {
        this.Scans = this.Scans
            .OrderBy(g => g.Time)
            .ToList();
    }
}