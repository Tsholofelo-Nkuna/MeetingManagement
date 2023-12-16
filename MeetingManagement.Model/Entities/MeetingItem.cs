namespace MeetingManagement.Model.Entities
{
    public class MeetingItem: BaseEntity<long>
    {
        public string Name { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

    }
}
