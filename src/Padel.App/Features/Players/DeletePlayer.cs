using Ardalis.Result;
using Marten;
using Padel.App.Entities;
using Serilog;

namespace Padel.App.Features.Players;

public class DeletePlayer(IDocumentStore documentStore)
{
    public async Task<Result> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            using var session = documentStore.LightweightSession();
            session.Delete<Player>(id);
            await session.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while deleting the player");
            return Result.CriticalError("An error occurred while deleting the player");
        }
    }
}
