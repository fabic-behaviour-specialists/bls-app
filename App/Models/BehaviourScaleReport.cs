using System;
using System.Collections.Generic;

namespace Fabic.Core.Models
{
    /// <summary>
    /// The parent class that defines a particular Behaviour Scale and its core properties
    /// </summary>
    public class BehaviourScaleReport : BehaviourScale
    {
        public List<BehaviourScaleItem> Items { get; set; }

        public BehaviourScaleReport()
        {
            Migrated = false;
            UserID = "";
            FabicExample = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Items = new List<BehaviourScaleItem>();
        }
    }
}
