using System.Globalization;

namespace FillingPointsHandler.Models;

public class TimeSeries
{
    public string Id { get; set; } = null!;

    public List<Gebinde> Stack { get; set; } = [];

    public void InsertOrUpdateGebinde(Gebinde gebinde)
    {
        var exist = this.Stack.SingleOrDefault(g => g.Time == gebinde.Time);

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
        this.Stack.Add(gebinde);

        this.Sort();
    }

    private void UpdateGebinde(Gebinde gebinde)
    {
        var exist = this.Stack.Single(g => g.Time == gebinde.Time);

        this.Stack.Remove(exist);

        InsertGebinde(gebinde);

        this.Sort();
    }

    private void Sort()
    {
        this.Stack = this.Stack
            .OrderBy(g => g.Time)
            .ToList();
    }
}