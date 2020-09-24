namespace EFCoreData.Models
{
    public class SportPlayer
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}