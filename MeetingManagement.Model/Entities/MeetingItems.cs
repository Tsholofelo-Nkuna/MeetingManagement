using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagement.Model.Entities
{
    [Table("X_MeetingItems")]
    public class MeetingItems: BaseEntity<long>
    {
        public long MeetingId { get; set; }

        public long MeetingItemId { get; set; }

    }
}
