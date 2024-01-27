﻿using Fabic.Core.Enumerations;
using Fabic.iOS.Controllers;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using System;

namespace Fabic.Core.Models
{
    /// <summary>
    /// Describes a particular item on a particular Behaviour Scale
    /// </summary>
    public class BehaviourScaleItem : Object
    {
        [PrimaryKey]
        [Unique]
        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        public string Id { get; set; }//get { if (id == "") { id = new Guid().ToString(); } return id; } }

        [JsonProperty(PropertyName = "behaviourScale")]
        /// <summary>
        /// The related Behaviour Scale
        /// </summary>
        public string BehaviourScale { get; set; }

        [JsonProperty(PropertyName = "name")]
        /// <summary>
        /// The text of the particular Behaviour Scale Item
        /// </summary>
        public string Name { get; set; }

        [JsonProperty(PropertyName = "behaviourScaleLevel")]
        /// <summary>
        /// The Level on the Scale this Item relates to
        /// </summary>
        public int BehaviourScaleLevel { get; set; }

        [JsonProperty(PropertyName = "behaviourScaleType")]
        /// <summary>
        /// The Type of Element on the Scale is Item relates to - be it Body or Life
        /// </summary>
        public int BehaviourScaleType { get; set; }

        [JsonProperty(PropertyName = "archived")]
        /// <summary>
        /// Whether or not this particular Behaviour Scale Item is deleted or not
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


        public BehaviourScaleItem()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
            Archived = false;
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public BehaviourScaleItem(string behaviourScale, string name, int behaviourScaleLevel, BehaviourScaleItemType itemType)
        {
            BehaviourScale = behaviourScale;
            Name = name;
            BehaviourScaleLevel = behaviourScaleLevel;
            BehaviourScaleType = (int)itemType;
            Archived = false;
            Migrated = false;
            UserID = SecurityController.CurrentUser != null ? SecurityController.CurrentUser.UserID : "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }
    }
}