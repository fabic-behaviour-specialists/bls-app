using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLS.Cloud.Models
{
    /// <summary>
    /// The parent class that defines a particular I Choose Chart and its core properties
    /// </summary>
    public class IChooseChart : BaseModel
    {
        /// <summary>
        /// The Name of the I Choose Chart
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the NEW SKILLS
        /// </summary>
        public string Description1 { get; set; }

        /// <summary>
        /// Description of the Possible Reactions
        /// </summary>
        public string Description2 { get; set; }

        [NotMapped]
        [JsonIgnore]
        /// <summary>
        /// Words that are to be highlighted in description 1
        /// </summary>
        public List<ItemHighlight> Keywords1
        {
            get; set;
        }

        [NotMapped]
        [JsonIgnore]
        /// <summary>
        /// Words that are to be highlighted in description 2
        /// </summary>
        public List<ItemHighlight> Keywords2
        {
            get; set;
        }

        /// <summary>
        /// Whether or not this particular I Choose Chart is a core Fabic Example or not
        /// </summary>
        public bool FabicExample { get; set; }

        /// <summary>
        /// Whether or not this particular I Choose Chart is deleted or not
        /// </summary>
        public bool Archived { get; set; }


        public IChooseChart()
        {
            Id = Guid.NewGuid().ToString();
            Migrated = false;
            UserID = string.Empty;
        }
    }
}