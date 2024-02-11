using BLS.Cloud.Models;
using Dapper;
using DevExpress.XtraReports.FavoriteProperties;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;
using Z.Dapper.Plus;

namespace BLS.Server.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;

        private SqlConnection _connection;

        public DatabaseService(ILogger<DatabaseService> logger, IConfiguration configuration)
        {
            _logger = logger;

            string? connection = configuration.GetConnectionString("Server") ?? throw new ArgumentNullException("Missing the server connection string");
            _connection = new(connection); 
            _connection.Open();
            
        }

        public async Task<IReadOnlyList<BehaviourScaleItem>> GetUserBehaviourScaleItemsAsync(string userId)
        {
            return (await _connection.QueryAsync<BehaviourScaleItem>("SELECT * FROM BehaviourScaleItems WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId })).ToList();
        }

        public async Task<IReadOnlyList<BehaviourScale>> GetUserBehaviourScalesAsync(string userId)
        {
            return (await _connection.QueryAsync<BehaviourScale>("SELECT * FROM BehaviourScales WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId })).ToList();
        }

        public async Task<IReadOnlyList<IChooseChartItem>> GetUserIChooseChartItemsAsync(string userId)
        {
            return (await _connection.QueryAsync<IChooseChartItem>("SELECT * FROM IChooseChartItems WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId })).ToList();
        }

        public async Task<IReadOnlyList<IChooseChart>> GetUserIChooseChartsAsync(string userId)
        {
            return (await _connection.QueryAsync<IChooseChart>("SELECT * FROM IChooseCharts WHERE UserID = @UserId order by UpdatedAt desc", new { UserId = userId })).ToList();
        }

        public async Task<int> UpdateBehaviourScaleAsync(BehaviourScale item)
        {
            var scale = await _connection.QueryFirstOrDefaultAsync<BehaviourScale>("SELECT * FROM BehaviourScales WHERE Id = @Id", new { Id = item.Id });
            var param = new { Id = item.Id, UserId = item.UserID, Description = "", FabicExample = false, Archived = item.Archived, Name = item.Name };
            return await _connection.ExecuteAsync(scale is null ? 
                "INSERT INTO BehaviourScales (Id, Name, Description, FabicExample, Archived, UserID) VALUES (@Id, @Name, @Description, @FabicExample, @Archived, @UserID)" :
                "UPDATE BehaviourScales SET Name = @Name, Archived = @Archived WHERE Id = @Id", param);
        }

        public async Task<int> UpdateBehaviourScaleItemAsync(BehaviourScaleItem item)
        {
            var scale = await _connection.QueryFirstOrDefaultAsync<BehaviourScaleItem>("SELECT * FROM BehaviourScaleItems WHERE Id = @Id", new { Id = item.Id });
            var param = new { Id = item.Id, Name = item.Name, UserId = item.UserID, Archived = item.Archived, BehaviourScale = item.BehaviourScale, BehaviourScaleLevel = item.BehaviourScaleLevel, BehaviourScaleType = item.BehaviourScaleType };
            return await _connection.ExecuteAsync(scale is null ?
                "INSERT INTO BehaviourScaleItems (Id, Name, Archived, UserID, BehaviourScale, BehaviourScaleLevel, BehaviourScaleType) VALUES (@Id, @Name, @Archived, @UserID, @BehaviourScale, @BehaviourScaleLevel, @BehaviourScaleType)" :
                "UPDATE BehaviourScaleItems SET Name = @Name, Archived = @Archived WHERE Id = @Id", param);
        }

        public async Task UpdateBehaviourScaleItemsAsync(IEnumerable<BehaviourScaleItem> items)
        {
            foreach (var item in items)
                await UpdateBehaviourScaleItemAsync(item);
            // await _connection.BulkActionAsync(x => x.BulkInsert<BehaviourScaleItem>(items));
        }

        public async Task UpdateBehaviourScalesAsync(IEnumerable<BehaviourScale> items)
        {
            foreach (var item in items)
                await UpdateBehaviourScaleAsync(item);
            // await _connection.BulkActionAsync(x => x.BulkInsert<BehaviourScale>(items));
        }

        public async Task<int> UpdateIChooseChartAsync(IChooseChart item)
        {
            var scale = await _connection.QueryFirstOrDefaultAsync<IChooseChart>("SELECT * FROM IChooseCharts WHERE Id = @Id", new { Id = item.Id });
            var param = new { Id = item.Id, UserId = item.UserID, Description = "", FabicExample = false, Archived = item.Archived, Name = item.Name };
            return await _connection.ExecuteAsync(scale is null ?
                "INSERT INTO IChooseCharts (Id, Name, FabicExample, Archived, UserID) VALUES (@Id, @Name, @FabicExample, @Archived, @UserID)" :
                "UPDATE IChooseCharts SET Name = @Name, Archived = @Archived WHERE Id = @Id", param);
        }

        public async Task<int> UpdateIChooseChartItemAsync(IChooseChartItem item)
        {
            var scale = await _connection.QueryFirstOrDefaultAsync<IChooseChartItem>("SELECT * FROM IChooseChartItems WHERE Id = @Id", new { Id = item.Id });
            var param = new { Id = item.Id, UserId = item.UserID, ItemText = item.ItemText, Archived = item.Archived, IChooseChart = item.IChooseChart, ChartOption = item.ChartOption, ChartType = item.ChartType };
            return await _connection.ExecuteAsync(scale is null ?
                "INSERT INTO IChooseChartItems (Id, ItemText, Archived, UserID, IChooseChart, ChartOption, ChartType) VALUES (@Id, @ItemText, @Archived, @UserID, @IChooseChart, @ChartOption, @ChartType)" :
                "UPDATE IChooseChartItems SET ItemText = @ItemText, Archived = @Archived WHERE Id = @Id", param);
        }

        public async Task UpdateIChooseChartItemsAsync(IEnumerable<IChooseChartItem> items)
        {
            // await _connection.BulkActionAsync(x => x.BulkInsert<IChooseChartItem>(items));
            foreach (var item in items)
                await UpdateIChooseChartItemAsync(item);
        }

        public async Task UpdateIChooseChartsAsync(IEnumerable<IChooseChart> items)
        {
            //await _connection.BulkActionAsync(x => x.BulkInsert<IChooseChart>(items));
            foreach (var item in items)
                await UpdateIChooseChartAsync(item);
        }
    }
}
