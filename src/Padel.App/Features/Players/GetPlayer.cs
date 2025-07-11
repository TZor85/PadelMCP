using Ardalis.Result;
using Marten;
using Padel.App.Entities;
using Serilog;

namespace Padel.App.Features.Players;

public class GetPlayer(IDocumentStore documentStore)
{
    public async Task<Player?> ExecuteAsync(Guid playerId, CancellationToken ct = default)
    {
        try
        {
            using var session = documentStore.LightweightSession();
            var player = await session.LoadAsync<Player>(playerId, ct);
            return player != null ? Result<Player?>.Success(player) : Result<Player?>.NotFound();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while retrieving the player");
            return null;
        }
    }
}
