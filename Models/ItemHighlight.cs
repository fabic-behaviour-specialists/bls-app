using Fabic.Data.Enums;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models
{
    public class ItemHighlight
    {
        /// <summary>
        /// Primary Unique Identifier of the Item Highlight
        /// </summary>
        public string Id { get; set; }//get { if (id == "") { id = new Guid().ToString(); } return id; } }
        [JsonProperty(PropertyName = "itemText")]
        public string ItemText
        {
            get; set;
        }
        [JsonProperty(PropertyName = "italics")]
        public bool Italics
        {
            get; set;
        }
        [JsonProperty(PropertyName = "bold")]
        public bool Bold
        {
            get; set;
        }
        [JsonProperty(PropertyName = "withColour")]
        public bool WithColour
        {
            get; set;
        }
        [JsonProperty(PropertyName = "iChooseChartItem")]
        public string IChooseChartItem
        {
            get; set;
        }
        [JsonProperty(PropertyName = "iChooseChart")]
        public string IChooseChart
        {
            get; set;
        }
        [JsonProperty(PropertyName = "fabicColour")]
        public int FabicColour
        {
            get; set;
        }
        [JsonProperty(PropertyName = "itemType")]
        public int ItemType
        {
            get; set;
        }

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }

        public ItemHighlight()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public ItemHighlight(string itemText, FabicColour colour, bool italics = false, bool bold = false, bool withColour = true, int itemType = 1)
        {
            ItemText = itemText;
            Italics = italics;
            Bold = bold;
            WithColour = withColour;
            ItemType = itemType;
            FabicColour = (int)colour;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
