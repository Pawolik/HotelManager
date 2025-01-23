using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManager.Data;
using HotelManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Services
{
    public class HotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Metoda 1: Dodaje nowego klienta
        public async Task<int> AddClientAsync(string firstName, string lastName, DateOnly dateOfBirth)
        {
            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client.ClientId;
        }

        // Metoda 2: Zwraca imię i nazwisko klienta
        public async Task<string> GetClientNameAsync(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) throw new Exception("Client not found");

            return $"{client.FirstName} {client.LastName}";
        }

        // Metoda 3: Zwraca liczbę niepełnoletnich klientów
        public async Task<int> GetUnderageClientsCountAsync()
        {
            var today = DateTime.Today;
            return await _context.Clients.CountAsync(c => (today.Year - c.DateOfBirth.Year) < 18);
        }

        // Metoda 4: Dodaje nowy pokój
        public async Task<int> AddRoomAsync(int floor, double area, bool hasLargeBed)
        {
            var room = new Room
            {
                Floor = floor,
                Area = area,
                HasLargeBed = hasLargeBed
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room.RoomId;
        }

        // Metoda 5: Zwraca powierzchnię pokoju
        public async Task<double> GetRoomAreaAsync(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) throw new Exception("Room not found");

            return room.Area;
        }

        // Metoda 6: Zwraca liczbę pokoi z dużym łóżkiem na piętrze
        public async Task<int> GetRoomsWithLargeBedCountAsync(int floor)
        {
            return await _context.Rooms.CountAsync(r => r.Floor == floor && r.HasLargeBed);
        }

        // Metoda 7: Tworzy i zapisuje nową rezerwację
        public async Task<int> CreateReservationAsync(int clientId, int roomId, DateTime date)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) throw new Exception("ClientNotFoundException");

            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) throw new Exception("RoomNotFoundException");

            var isReserved = await _context.Reservations.AnyAsync(r => r.RoomId == roomId && r.Date == date);
            if (isReserved) throw new Exception("RoomReservedException");

            var reservation = new Reservation
            {
                ClientId = clientId,
                RoomId = roomId,
                Date = date
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation.ReservationId;
        }

        // Metoda 8: Sprawdza, czy pokój jest zarezerwowany w danym dniu
        public async Task<bool> IsRoomReservedAsync(int roomId, DateTime date)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) throw new Exception("RoomNotFoundException");

            return await _context.Reservations.AnyAsync(r => r.RoomId == roomId && r.Date == date);
        }

        // Metoda 9: Zwraca liczbę niepotwierdzonych rezerwacji w danym dniu
        public async Task<int> GetUnconfirmedReservationsCountAsync(DateTime date)
        {
            return await _context.Reservations.CountAsync(r => r.Date.Date == date.Date && !r.IsConfirmed);
        }

        // Metoda 10: Zwraca unikalne identyfikatory pokoi zarezerwowanych przez klienta
        public async Task<List<int>> GetRoomsReservedByClientAsync(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) throw new Exception("ClientNotFoundException");

            return await _context.Reservations
                .Where(r => r.ClientId == clientId)
                .Select(r => r.RoomId)
                .Distinct()
                .ToListAsync();
        }
    }
}
