using BLS.Cloud.Models;

namespace BLS.Server.Services
{
    public interface IDatabaseService
    {
        public Task<IReadOnlyList<BehaviourScale>> GetUserBehaviourScalesAsync(string userId);
        public Task<IReadOnlyList<BehaviourScaleItem>> GetUserBehaviourScaleItemsAsync(string userId);
        public Task<IReadOnlyList<IChooseChart>> GetUserIChooseChartsAsync(string userId);
        public Task<IReadOnlyList<IChooseChartItem>> GetUserIChooseChartItemsAsync(string userId);

        public Task UpdateBehaviourScaleAsync(BehaviourScale item);
        public Task UpdateBehaviourScaleItemAsync(BehaviourScaleItem item);
        public Task UpdateIChooseChartAsync(IChooseChart item);
        public Task UpdateIChooseChartItemAsync(IChooseChartItem item);
    }
}
