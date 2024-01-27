using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;

namespace Fabic.Core.Models
{
    public class FabicIChooseChart
    {
        [PrimaryKey]
        /// <summary>
        /// Primary Unique Identifier of the Behaviour Scale
        /// </summary>
        public string Id { get; set; }//get { if (id == "") { id = new Guid().ToString(); } return id; } }

        [JsonProperty(PropertyName = "name")]
        /// <summary>
        /// The Name of the I Choose Chart
        /// </summary>
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description1")]
        /// <summary>
        /// Description of the NEW SKILLS
        /// </summary>
        public string Description1 { get; set; }

        [JsonProperty(PropertyName = "description2")]
        /// <summary>
        /// Description of the Possible Reactions
        /// </summary>
        public string Description2 { get; set; }

        /// <summary>
        /// Words that are to be highlighted in description 1
        /// </summary>
        [Ignore]
        public List<ItemHighlight> Keywords1
        {
            get; set;
        }

        /// <summary>
        /// Words that are to be highlighted in description 2
        /// </summary>
        [Ignore]
        public List<ItemHighlight> Keywords2
        {
            get; set;
        }

        [JsonProperty(PropertyName = "fabicExample")]
        /// <summary>
        /// Whether or not this particular I Choose Chart is a core Fabic Example or not
        /// </summary>
        public bool FabicExample { get; set; }

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

        [Version]
        public string Version { get; set; }

        [CreatedAt]
        public DateTime CreatedAt { get; set; }

        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }

        public FabicIChooseChart()
        {
            Migrated = false;
            UserID = "";
            FabicExample = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public IChooseChart ConvertToIChooseChart()
        {
            IChooseChart icc = new IChooseChart();
            icc.Archived = this.Archived;
            icc.Keywords1 = this.Keywords1;
            icc.Keywords2 = this.Keywords2;
            icc.Description1 = this.Description1;
            icc.Description2 = this.Description2;
            icc.FabicExample = true;
            icc.Id = this.Id;
            icc.Migrated = this.Migrated;
            icc.Name = this.Name;
            icc.UserID = "";
            icc.Version = this.Version;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            return icc;
        }
    }
}
