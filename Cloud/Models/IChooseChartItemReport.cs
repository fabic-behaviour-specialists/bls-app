using BLS.Cloud.Enumerations;
using System;

namespace BLS.Cloud.Models
{
    /// <summary>
    /// Describes a particular item on a particular I Choose Chart
    /// </summary>
    public class IChooseChartItemReport : BaseModel
    {
        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        public IChooseChartItemReport()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        /// <param name="parentChartID">ID of parent chart</param>
        /// <param name="itemText">The description text associated with the item</param>
        /// <param name="chartOption">Which option the item relates to</param>
        /// <param name="itemType">Whether it is a behaviour or an outcome</param>
        public IChooseChartItemReport(string parentChart, string itemText, IChooseChartOption chartOption, IChooseChartItemType itemType)
        {
            Id = Guid.NewGuid().ToString();
            IChooseChart = parentChart;
            ItemText = itemText;
            ChartOption = (int)chartOption;
            ChartType = (int)itemType;
            Migrated = false;
            UserID = string.Empty;
        }

        /// <summary>
        /// The related I Choose Chart
        /// </summary>
        public string IChooseChart { get; set; }

        /// <summary>
        /// The text of the particular I Choose Chart Item
        /// </summary>
        public string ItemText { get; set; }

        /// <summary>
        /// The Chart Option on the Chart this Item relates to (1 or 2)
        /// </summary>
        public int ChartOption { get; set; }

        /// <summary>
        /// The Type of Element on the Scale is Item relates to - be it Behaviour or Outcome
        /// </summary>
        public int ChartType { get; set; }

        /// <summary>
        /// Whether or not this particular I Choose Chart is deleted or not
        /// </summary>
        public bool Archived { get; set; }
    }
}