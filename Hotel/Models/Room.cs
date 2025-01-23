using System.ComponentModel.DataAnnotations;

namespace  HotelManager.Models
{
    public class Room
    {
        [Key]
        [Display (Name ="Numer pokoju")]
        public int RoomId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Piętro")]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Powierzchnia")]
        public double Area { get; set; }

        [Required]
        [Display(Name = "Czy ma duże łóżko?")]
        public bool HasLargeBed { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
