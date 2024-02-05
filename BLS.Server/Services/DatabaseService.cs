using BLS.Cloud.Models;
using Dapper;
using DevExpress.XtraReports.FavoriteProperties;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;

namespace BLS.Server.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IMemoryCache _cache;

        private SqlConnection _connection;

        public DatabaseService(ILogger<DatabaseService> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
            _connection = new("");
            _connection.Open();
        }

        public async Task<IReadOnlyList<BehaviourScaleItem>> GetUserBehaviourScaleItemsAsync(string userId)
        {
            return await _connection.QueryAsync<BehaviourScaleItem>("SELECT * FROM BehaviourScaleItems WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId }).ToList();
        }

        public async Task<IReadOnlyList<BehaviourScale>> GetUserBehaviourScalesAsync(string userId)
        {
            return await _connection.QueryAsync<BehaviourScale>("SELECT * FROM BehaviourScales WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId }).ToList();
        }

        public async Task<IReadOnlyList<IChooseChartItem>> GetUserIChooseChartItemsAsync(string userId)
        {
            return await _connection.QueryAsync<IChooseChartItem>("SELECT * FROM IChooseChartItems WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId }).ToList();
        }

        public async Task<IReadOnlyList<IChooseChart>> GetUserIChooseChartsAsync(string userId)
        {
            return await _connection.QueryAsync<IChooseChart>("SELECT * FROM IChooseCharts WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId }).ToList();
        }

        public async Task<int> UpdateBehaviourScaleAsync(BehaviourScale item)
        {
            var scale = await _connection.QueryFirstAsync<BehaviourScale>("SELECT * FROM BehaviourScales WHERE Id = @Id", new { Id = item.Id });
            var param = new { Id = item.Id, UserId = item.UserID, Description = "", FabicExample = false, Archived = item.Archived };
            return await _connection.ExecuteAsync(scale is null ? 
                "INSERT INTO BehaviourScales (Id, Name, Description, FabicExample, Archived, UserID) VALUES (@Id, @Name, @Description, @FabicExample, @Archived, @UserID)" :
                "UPDATE BehaviourScales SET Name = @Name, Archived = @Archived WHERE Id = @Id", param);
        }

        public async Task UpdateBehaviourScaleItemAsync(BehaviourScaleItem item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateIChooseChartAsync(IChooseChart item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateIChooseChartItemAsync(IChooseChartItem item)
        {
            throw new NotImplementedException();
        }
    }
}
