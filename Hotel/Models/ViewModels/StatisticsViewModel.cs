using System.ComponentModel.DataAnnotations;

namespace HotelManager.Models.ViewModels
{
    public class StatisticsViewModel
    {

        [Display(Name = "Liczba nieletnich")]
        public int UnderageClientsCount { get; set; } = 0;
        [Display(Name ="Niepotwierdzone rezerwacje")]
        public List<UnConfirmedReservations> UnConfirmedReservations { get; set; } = new List<UnConfirmedReservations>();
        [Display(Name= "Rezerwacje danego klienta")]
        public List<ClientRoomsReservations> ClientRoomsReservations { get; set; } = new List<ClientRoomsReservations>();
        [Display(Name = "Liczba pokoi z dużym łóżkiem na danym piętrze")]

        public List<FloorLargeBeds> FloorLargeBeds { get; set; } = new List<FloorLargeBeds>();

    }
    public class UnConfirmedReservations
    {
        [Display(Name = "Dzień")]

        public DateTime Day { get; set; }
        [Display(Name = "Liczba niepotwierdzonych rezerwacji")]

        public int Count { get; set; }
    }
    public class ClientRoomsReservations
    {
        [Display(Name ="Klient")]
        public int ClientId { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name ="Pokoje")]
        public string Rooms { get; set; }
    }
    public class FloorLargeBeds
    {
        [Display(Name = "Piętro")]
        public int Floor { get; set; }
        [Display(Name = "Liczba pokoi z dużym łóżkiem")]
        public int CountRoomsWithLargeBed { get; set; } = 0;
    }
}
