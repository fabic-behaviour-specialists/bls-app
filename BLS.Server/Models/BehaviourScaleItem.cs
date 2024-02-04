using BLS.Cloud.Enumerations;
using System;

namespace BLS.Cloud.Models
{
    /// <summary>
    /// Describes a particular item on a particular Behaviour Scale
    /// </summary>
    public class BehaviourScaleItem : BaseModel
    {
        /// <summary>
        /// The related Behaviour Scale
        /// </summary>
        public string BehaviourScale { get; set; }

        /// <summary>
        /// The text of the particular Behaviour Scale Item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Level on the Scale this Item relates to
        /// </summary>
        public int BehaviourScaleLevel { get; set; }

        /// <summary>
        /// The Type of Element on the Scale is Item relates to - be it Body or Life
        /// </summary>
        public int BehaviourScaleType { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale Item is deleted or not
        /// </summary>
        public bool Archived { get; set; }

        public BehaviourScaleItem()
        {
            Id = Guid.NewGuid().ToString();
            Archived = false;
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
        }

        public BehaviourScaleItem(string behaviourScale, string name, int behaviourScaleLevel, BehaviourScaleItemType itemType)
        {
            Id = Guid.NewGuid().ToString();
            BehaviourScale = behaviourScale;
            Name = name;
            BehaviourScaleLevel = behaviourScaleLevel;
            BehaviourScaleType = (int)itemType;
            Archived = false;
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
        }
    }
}