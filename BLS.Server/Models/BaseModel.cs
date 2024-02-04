using Microsoft.Azure.Mobile.Server;

namespace BLS.Cloud.Models
{
    public abstract class BaseModel : EntityData
    {
        /// <summary>
        /// The ID of the associated user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale has been migrated to the new system yet
        /// </summary>
        public bool Migrated { get; set; }
    }
}