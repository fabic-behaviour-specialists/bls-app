using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLS.Cloud.Models
{
    /// <summary>
    /// The parent class that defines a particular Behaviour Scale and its core properties
    /// </summary>
    public class BehaviourScale : BaseModel
    {
        /// <summary>
        /// The Name of the Behaviour Scale
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the Behaviour Scale and what it relates to
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale is a core Fabic Example or not
        /// </summary>
        public bool FabicExample { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale is deleted or not
        /// </summary>
        public bool Archived { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale has been migrated to the new system yet
        /// </summary>
        public bool Migrated { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<BehaviourScaleItem> Items { get; set; }


        public BehaviourScale()
        {
            Id = Guid.NewGuid().ToString();
            Migrated = false;
            UserID = string.Empty;
            FabicExample = false;
            CreatedAt = DateTime.Now;
            Items = new();
        }
    }
}