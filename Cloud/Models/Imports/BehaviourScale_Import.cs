﻿using BLS.Cloud.Models;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models.Imports
{
    /// <summary>
    /// The parent class that defines a particular Behaviour Scale and its core properties
    /// </summary>
    public class BehaviourScale_Import
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

        public string Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public BehaviourScale_Import()
        {
            Migrated = false;
            UserID = "";
            FabicExample = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public BehaviourScale ConvertToAPIModel()
        {
            BehaviourScale scale = new BehaviourScale();
            scale.Id = Id;
            scale.Name = Name;
            scale.Description = Description;
            scale.Archived = Archived;
            scale.CreatedAt = CreatedAt;
            scale.UpdatedAt = UpdatedAt;
            scale.FabicExample = FabicExample;
            scale.UserID = UserID;
            return scale;
        }
    }
}
