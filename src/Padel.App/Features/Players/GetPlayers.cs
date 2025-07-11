using Ardalis.Result;
using Marten;
using Padel.App.Entities;

namespace Padel.App.Features.Players;

public class GetPlayers(IDocumentStore documentStore)
{
    public async Task<Result<List<Player>>> ExecuteAsync(CancellationToken ct = default)
    {
        try
        {
            using var session = documentStore.LightweightSession();
            var players = await session.Query<Player>().ToListAsync(ct);
            return Result<List<Player>>.Success([.. players]);
        }
        catch (Exception ex)
        {
            Serilog.Log.Fatal(ex, "An error occurred while retrieving the players");
            return new List<Player>();
        }
    }
}
