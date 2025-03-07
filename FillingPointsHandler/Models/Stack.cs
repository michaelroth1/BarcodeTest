namespace FillingPointsHandler.Models;

public class Stack
{
    public string Id { get; set; } = null!;

    public List<Gebinde> GebindeStack { get; set; } = [];

    public void InsertOrUpdateGebinde(Gebinde gebinde)
    {
        var exist = this.GebindeStack.SingleOrDefault(g => g.InsertionTime == gebinde.InsertionTime);

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
        this.GebindeStack.Add(gebinde);

        this.Sort();
    }

    private void UpdateGebinde(Gebinde gebinde)
    {
        var exist = this.GebindeStack.Single(g => g.InsertionTime == gebinde.InsertionTime);

        this.GebindeStack.Remove(exist);

        InsertGebinde(gebinde);

        this.Sort();
    }

    private void Sort()
    {
        this.GebindeStack = this.GebindeStack
            .OrderBy(g => g.InsertionTime)
            .ToList();
    }
}