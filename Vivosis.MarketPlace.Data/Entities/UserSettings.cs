using System;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class UserSettings
    {
        public int Id { get; set; }
        public bool IsSynced { get; set; }
        public DateTime? LastSyncTime { get; set; }
        public int UserId { get; set; }
        public virtual SystemUser User { get; set; }
    }
}
