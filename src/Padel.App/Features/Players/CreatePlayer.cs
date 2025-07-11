using Ardalis.Result;
using Marten;
using Serilog;
using Padel.App.Entities;

namespace Padel.App.Features.Players;

public record CreatePlayerRequest(string Name);

public class CreatePlayer(IDocumentStore documentStore)
{
    public async Task<Result> ExecuteAsync(CreatePlayerRequest request, CancellationToken ct = default)
    {
        try
        {
            var session = documentStore.LightweightSession();
            var player = new Player { Id = Guid.NewGuid(), Name = request.Name };
            session.Store(player);
            await session.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while creating the player");
            return Result.Error("An error occurred while creating the player");
        }
    }

}
