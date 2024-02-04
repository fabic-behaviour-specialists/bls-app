using BLS.Cloud.Enumerations;
using System;

namespace BLS.Cloud.Models
{
    public class ItemHighlight : BaseModel
    {
        public string ItemText
        {
            get; set;
        }
        public bool Italics
        {
            get; set;
        }
        public bool Bold
        {
            get; set;
        }
        public bool WithColour
        {
            get; set;
        }
        public string IChooseChartItem
        {
            get; set;
        }
        public string IChooseChart
        {
            get; set;
        }
        public int FabicColour
        {
            get; set;
        }
        public int ItemType
        {
            get; set;
        }

        public ItemHighlight()
        {

        }

        public ItemHighlight(string itemText, FabicColour colour, bool italics = false, bool bold = false, bool withColour = true, int itemType = 1)
        {
            Id = Guid.NewGuid().ToString();
            ItemText = itemText;
            Italics = italics;
            Bold = bold;
            WithColour = withColour;
            ItemType = itemType;
            FabicColour = (int)colour;
        }
    }
}