namespace MeetingManagement.Model.Entities
{
    public class Meeting : BaseEntity<long>
    {
        public MeetingType Type { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
