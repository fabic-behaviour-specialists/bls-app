using Fabic.Core.Enumerations;
using Fabic.Core.Helpers;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.Core.Models
{
    /// <summary>
    /// Describes a particular item on a particular I Choose Chart
    /// </summary>
    public class IChooseChartItem
    {
        /// <summary>
        /// Initialises a new I Choose Chart Item
        /// </summary>
        public IChooseChartItem()
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
        public IChooseChartItem(string parentChart, string itemText, IChooseChartOption chartOption, IChooseChartItemType itemType)
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

        [JsonIgnore]
        private List<ItemHighlight> _Keywords = new List<ItemHighlight>();
        [JsonIgnore]
        /// <summary>
        /// Words that are to be highlighted
        /// </summary>
        public List<ItemHighlight> Keywords
        {
            get { return _Keywords; }
            set
            {
                _Keywords = value;
                if (_Keywords == null) { _Keywords = new List<ItemHighlight>(); }
                else
                {
                    _MutableText = new NSMutableAttributedString(ItemText, UIFont.SystemFontOfSize(9));
                    foreach (ItemHighlight highlight in Keywords)
                    {
                        List<int> substrings = ItemText.AllIndexesOf(highlight.ItemText);
                        if (substrings.Count > 0)
                        {
                            UIStringAttributes stringAttributes = new UIStringAttributes();
                            if (highlight.Italics)
                                stringAttributes.TextEffect = NSTextEffect.LetterPressStyle;
                            if (highlight.WithColour)
                                stringAttributes.ForegroundColor = UIColor.Black.FabicColour((FabicColour)highlight.FabicColour);
                            if (highlight.Bold)
                                stringAttributes.Font = UIFont.SystemFontOfSize(9, UIFontWeight.Bold);
                            NSAttributedString s = new NSAttributedString(ItemText.Substring(substrings[0], highlight.ItemText.Length), stringAttributes);
                            foreach (int i in substrings)
                            {
                                _MutableText.Replace(new NSRange(i, highlight.ItemText.Length), s);
                            }
                        }
                    }
                }
            }
        }
        [JsonIgnore]
        private NSMutableAttributedString _MutableText;
        public NSMutableAttributedString MutableText
        {
            get { return _MutableText; }
        }

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }
    }
}
