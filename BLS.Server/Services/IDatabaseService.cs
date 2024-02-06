using BLS.Cloud.Models;

namespace BLS.Server.Services
{
    public interface IDatabaseService
    {
        public Task<IReadOnlyList<BehaviourScale>> GetUserBehaviourScalesAsync(string userId);
        public Task<IReadOnlyList<BehaviourScaleItem>> GetUserBehaviourScaleItemsAsync(string userId);
        public Task<IReadOnlyList<IChooseChart>> GetUserIChooseChartsAsync(string userId);
        public Task<IReadOnlyList<IChooseChartItem>> GetUserIChooseChartItemsAsync(string userId);

        public Task<int> UpdateBehaviourScaleAsync(BehaviourScale item);
        public Task<int> UpdateBehaviourScaleItemAsync(BehaviourScaleItem item);
        public Task<int> UpdateIChooseChartAsync(IChooseChart item);
        public Task<int> UpdateIChooseChartItemAsync(IChooseChartItem item);

        public Task UpdateBehaviourScalesAsync(IEnumerable<BehaviourScale> items);
        public Task UpdateBehaviourScaleItemsAsync(IEnumerable<BehaviourScaleItem> items);
        public Task UpdateIChooseChartsAsync(IEnumerable<IChooseChart> items);
        public Task UpdateIChooseChartItemsAsync(IEnumerable<IChooseChartItem> items);
    }
}
