namespace  HotelManager.Models
{
    public class Hotel
    {
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
