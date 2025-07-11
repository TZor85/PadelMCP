
using ModelContextProtocol.Server;
using Padel.App.Entities;
using Padel.App.Features.Players;
using System.ComponentModel;

namespace Padel.App.Tools;

[McpServerToolType]
public class PlayersTool
{
    [McpServerTool(Name = "CreatePlayer"), Description("Create a player.")]
    public static async Task<string> CreatePlayer(ILogger<PlayersTool> logger, CreatePlayer command, [Description("The player name")] string name)
    {
        var player = new CreatePlayerRequest(name);
        var result = await command.ExecuteAsync(player);

        logger.LogInformation("CreatePlayer result: {Error}", result.Errors.FirstOrDefault() ?? "No errors");

        if (result.IsSuccess)
            return $"Player created successfully";
        else
            return $"Error creating player: {result.Errors.FirstOrDefault() ?? "Unknown error"}";
    }

    [McpServerTool(Name = "GetPlayer"), Description("Get a player.")]
    public static async Task<Player?> GetPlayer(GetPlayer command, [Description("The player ID")] Guid id)
    {
        return await command.ExecuteAsync(id);
    }

    [McpServerTool(Name = "GetPlayers"), Description("Get all players.")]
    public static async Task<List<Player>> GetPlayers(GetPlayers command)
    {
        return await command.ExecuteAsync();
    }

    [McpServerTool(Name = "GetDeletedPlayers"), Description("Get deleted players.")]
    public static async Task<List<Player>> GetDeletedPlayers(GetDeletePlayers command, [Description("Optional: Filter deleted players after this date")] DateTime? deletedAfter = null)
    {
        return await command.ExecuteAsync(deletedAfter);        
    }

    [McpServerTool(Name = "DeletePlayer"), Description("Delete a player by ID.")]
    public static async Task<string> DeletePlayer(ILogger<PlayersTool> logger, DeletePlayer command, [Description("The player ID")] Guid id)
    {
        var result = await command.ExecuteAsync(id);
        logger.LogInformation("DeletePlayer result: {Error}", result.Errors.FirstOrDefault() ?? "No errors");

        if (result.IsSuccess)
            return $"Player deleted successfully";
        else
            return $"Error deleting player: {result.Errors.FirstOrDefault() ?? "Unknown error"}";
    }

    public static async Task<string> UndeletePlayer(ILogger<PlayersTool> logger, UndeletePlayer command, [Description("The player ID")] Guid id)
    {
        var result = await command.ExecuteAsync(id);
        logger.LogInformation("UndeletePlayer result: {Error}", result.Errors.FirstOrDefault() ?? "No errors");
        
        if (result.IsSuccess)
            return $"Player undeleted successfully";
        else
            return $"Error undeleting player: {result.Errors.FirstOrDefault() ?? "Unknown error"}";
    }

}
