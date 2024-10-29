using AIInteriorDesignerWebApp.Enums;

namespace AIInteriorDesignerWebApp.Models
{
    public class DesignForm
    {
        public string? Id { get; set; }
        public RoomType RoomType { get; set; } = RoomType.LivingRoom;
        public Style Style { get; set; } = Style.Modern;
        public byte[]? ImageBytes { get; set; }
    }
}