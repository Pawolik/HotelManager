using System.ComponentModel.DataAnnotations;

namespace  HotelManager.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room? Room { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;
    }
}
