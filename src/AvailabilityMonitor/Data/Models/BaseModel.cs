using System;

namespace AvailabilityMonitor.Data.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }
    }
}
