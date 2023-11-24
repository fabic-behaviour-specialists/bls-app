using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models
{
    /// <summary>
    /// The parent class that defines a particular Video and its core properties
    /// </summary>
    public class FabicVideo
    {
        //string id = "";

        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        public string Id { get; set; }// get { if (id == "") { id = new Guid().ToString(); } return id; } }

        [JsonProperty(PropertyName = "name")]
        /// <summary>
        /// The Title of the Video
        /// </summary>
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        /// <summary>
        /// Description of the Behaviour Scale and what it relates to
        /// </summary>
        public string Description { get; set; }

        [JsonProperty(PropertyName = "aboutFabic")]
        /// <summary>
        /// Whether or not this particular Video is about Fabic or not
        /// </summary>
        public bool AboutFabic { get; set; }

        [JsonProperty(PropertyName = "archived")]
        /// <summary>
        /// Whether or not this particular Behaviour Scale is deleted or not
        /// </summary>
        public bool Archived { get; set; }

        [JsonProperty(PropertyName = "url")]
        /// <summary>
        /// The video URL
        /// </summary>
        public string URL { get; set; }

        [JsonProperty(PropertyName = "imageFilePath")]
        /// <summary>
        /// Not used
        /// </summary>
        public string ImageFilePath { get; set; }

        [JsonProperty(PropertyName = "imageData")]
        /// <summary>
        /// Not used
        /// </summary>
        public byte[] ImageData { get; set; }

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }

        public FabicVideo()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
