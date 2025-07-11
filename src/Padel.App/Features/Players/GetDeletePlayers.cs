using Ardalis.Result;
using Marten;
using Marten.Linq.SoftDeletes;
using Padel.App.Entities;
using Serilog;

namespace Padel.App.Features.Players
{
    public class GetDeletePlayers(IDocumentStore documentStore)
    {
        public async Task<Result<List<Player>>> ExecuteAsync(DateTime? deletedAfter = null, CancellationToken ct = default)
        {
            try
            {
                using var session = documentStore.QuerySession();
                var players = await session.Query<Player>()
                    .Where(m => m.MaybeDeleted())
                    .ToListAsync();

                return Result<List<Player>>.Success([.. players.Where(m => m.Deleted)]);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error retrieving deleted players");
                return Result.CriticalError("An error occurred while retrieving deleted players");
            }
        }
    }
}
