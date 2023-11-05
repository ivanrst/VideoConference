using System.ComponentModel.DataAnnotations;

namespace VideoConference.Entities
{
    public class Device
    {
        [Key]
        public string Id { get; set; }
        public string Model { get; set; }
        public string Room { get; set; }
        public string Organization { get; set; }
        public string DateAdded { get; set; }
    }
}
