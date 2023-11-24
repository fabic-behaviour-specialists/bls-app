using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models
{
    public class FabicBehaviourScale
    {
        //string id = "";

        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        public string Id { get; set; }// get { if (id == "") { id = new Guid().ToString(); } return id; } }

        [JsonProperty(PropertyName = "name")]
        /// <summary>
        /// The Name of the Behaviour Scale
        /// </summary>
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        /// <summary>
        /// Description of the Behaviour Scale and what it relates to
        /// </summary>
        public string Description { get; set; }

        [JsonProperty(PropertyName = "fabicExample")]
        /// <summary>
        /// Whether or not this particular Behaviour Scale is a core Fabic Example or not
        /// </summary>
        public bool FabicExample { get; set; }

        [JsonProperty(PropertyName = "archived")]
        /// <summary>
        /// Whether or not this particular Behaviour Scale is deleted or not
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

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }

        public FabicBehaviourScale()
        {
            Migrated = false;
            UserID = "";
            FabicExample = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public BehaviourScale ConvertToBehaviourScale()
        {
            BehaviourScale bs = new BehaviourScale();
            bs.Archived = this.Archived;
            bs.Description = this.Description;
            bs.FabicExample = true;
            bs.Id = this.Id;
            bs.Migrated = this.Migrated;
            bs.Name = this.Name;
            bs.UserID = "";
            bs.Version = this.Version;

            return bs;
        }
    }
}
