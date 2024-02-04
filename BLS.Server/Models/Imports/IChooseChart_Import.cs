using BLS.Cloud.Models;
using Newtonsoft.Json;
using System;

namespace Fabic.Core.Models.Imports
{
    /// <summary>
    /// The parent class that defines a particular I Choose Chart and its core properties
    /// </summary>
    public class IChooseChart_Import
    {

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

        ///// <summary>
        ///// Associated Items
        ///// </summary>
        //public List<IChooseChartItem> Items
        //{
        //    get; set;
        //}

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

        public string Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IChooseChart_Import()
        {
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public IChooseChart ConvertToAPIModel()
        {
            IChooseChart chart = new IChooseChart();
            chart.Id = Id;
            chart.Name = Name;
            chart.Description1 = Description1;
            chart.Description2 = Description2;
            chart.Archived = Archived;
            chart.CreatedAt = CreatedAt;
            chart.UpdatedAt = UpdatedAt;
            chart.FabicExample = FabicExample;
            chart.UserID = UserID;
            return chart;
        }
    }
}
