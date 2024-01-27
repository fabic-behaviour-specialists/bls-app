using Fabic.Core.Enumerations;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;

namespace Fabic.Core.Models
{
    /// <summary>
    /// Describes a particular item on a particular I Choose Chart
    /// </summary>
    public class FabicIChooseChartItem
    {
        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        public FabicIChooseChartItem()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        /// <param name="parentChartID">ID of parent chart</param>
        /// <param name="itemText">The description text associated with the item</param>
        /// <param name="chartOption">Which option the item relates to</param>
        /// <param name="itemType">Whether it is a behaviour or an outcome</param>
        public FabicIChooseChartItem(string parentChart, string itemText, IChooseChartOption chartOption, IChooseChartItemType itemType)
        {
            IChooseChart = parentChart;
            ItemText = itemText;
            ChartOption = (int)chartOption;
            ChartType = (int)itemType;
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        [PrimaryKey]
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

        /// <summary>
        /// Words that are to be highlighted
        /// </summary>
        [Ignore]
        public List<ItemHighlight> Keywords
        {
            get; set;
        }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }

        [Version]
        public string Version { get; set; }

        public IChooseChartItem ConvertToIChooseChartItem()
        {
            IChooseChartItem item = new IChooseChartItem();
            item.Archived = this.Archived;
            item.ChartOption = this.ChartOption;
            item.ChartType = this.ChartType;
            item.IChooseChart = this.IChooseChart;
            item.ItemText = this.ItemText;
            item.Id = this.Id;
            item.Migrated = this.Migrated;
            item.Keywords = this.Keywords;
            item.UserID = this.UserID;
            item.Version = this.Version;
            return item;
        }
    }
}
