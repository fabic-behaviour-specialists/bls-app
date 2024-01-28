using BLS.Cloud.Models;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models.Imports
{
    /// <summary>
    /// Describes a particular item on a particular I Choose Chart
    /// </summary>
    public class IChooseChartItem_Import
    {
        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        public IChooseChartItem_Import()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        public string Id { get; set; }//get { if (id == "") { id = new Guid().ToString(); } return id; } }

        [JsonProperty(PropertyName = "iChooseChart")]
        /// <summary>
        /// The related I Choose Chart
        /// </summary>
        public string IChooseChart { get; set; }

        [JsonProperty(PropertyName = "itemText")]
        /// <summary>
        /// The text of the particular I Choose Chart Item
        /// </summary>
        public string ItemText { get; set; }

        [JsonProperty(PropertyName = "chartOption")]
        /// <summary>
        /// The Chart Option on the Chart this Item relates to (1 or 2)
        /// </summary>
        public int ChartOption { get; set; }

        [JsonProperty(PropertyName = "chartType")]
        /// <summary>
        /// The Type of Element on the Scale is Item relates to - be it Behaviour or Outcome
        /// </summary>
        public int ChartType { get; set; }

        [JsonProperty(PropertyName = "archived")]
        /// <summary>
        /// Whether or not this particular I Choose Chart is deleted or not
        /// </summary>
        public bool Archived { get; set; }

        [JsonProperty(PropertyName = "migrated")]
        /// <summary>
        /// Whether or not this particular Behaviour Scale has been migrated to the new system yet
        /// </summary>
        public bool Migrated { get; set; }

        [JsonProperty(PropertyName = "userID")]
        /// <summary>
        /// The ID of the associated user
        /// </summary>
        public string UserID { get; set; }

        public string Version { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IChooseChartItem ConvertToAPIModel()
        {
            IChooseChartItem item = new IChooseChartItem();
            item.Id = Id;
            item.IChooseChart = IChooseChart;
            item.ChartOption = ChartOption;
            item.ChartType = ChartType;
            item.ItemText = ItemText;
            item.CreatedAt = CreatedAt;
            item.UpdatedAt = UpdatedAt;
            item.Archived = Archived;
            item.UserID = UserID;
            return item;
        }
    }
}
