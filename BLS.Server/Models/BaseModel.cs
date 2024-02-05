namespace BLS.Cloud.Models
{
    public abstract class BaseModel 
    {
        /// <summary>
        /// The ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The ID of the associated user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Whether or not this particular Behaviour Scale has been migrated to the new system yet
        /// </summary>
        public bool Migrated { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}