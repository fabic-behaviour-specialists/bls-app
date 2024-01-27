using Fabic.iOS.Controllers;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using System;

namespace Fabic.Core.Models
{
    /// <summary>
    /// The parent class that defines a particular Behaviour Scale and its core properties
    /// </summary>
    public class BehaviourScale
    {
        //string id = "";

        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        [PrimaryKey]
        [Unique]
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

        public BehaviourScale()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString(); 
            Migrated = false;
            UserID = SecurityController.CurrentUser != null ? SecurityController.CurrentUser.UserID : "";
            FabicExample = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
