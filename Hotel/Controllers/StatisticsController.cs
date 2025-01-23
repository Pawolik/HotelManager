using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Models;
using HotelManager.Models.ViewModels;
using HotelManager.Services;

namespace HotelManager.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private HotelService? _hotelService;
        public HotelService HotelService
        {
            get
            {
                if (_hotelService == null)
                {
                    _hotelService = new HotelService(_context);
                }
                return _hotelService;
            }
        }
        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        public async Task<IActionResult> Index()
        {
            StatisticsViewModel model = new StatisticsViewModel();
            model.FloorLargeBeds = _context.Rooms.AsEnumerable().DistinctBy(x => x.Floor).Select(f => new FloorLargeBeds
            {
                Floor = f.Floor,
                CountRoomsWithLargeBed = HotelService.GetRoomsWithLargeBedCountAsync(f.Floor).GetAwaiter().GetResult()
            }).ToList();
            model.UnConfirmedReservations = _context.Reservations.AsEnumerable().DistinctBy(x => x.Date.Date).Select(f => new UnConfirmedReservations
            {
                Day = f.Date.Date,
                Count = HotelService.GetUnconfirmedReservationsCountAsync(f.Date.Date).GetAwaiter().GetResult()
            }).ToList();
            model.ClientRoomsReservations = _context.Clients.AsEnumerable().Select(f => new ClientRoomsReservations
            {
                ClientId = f.ClientId,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Rooms= string.Join(',', HotelService.GetRoomsReservedByClientAsync(f.ClientId).GetAwaiter().GetResult())
            }).ToList();
            model.UnderageClientsCount = await HotelService.GetUnderageClientsCountAsync();

            return View(model);
        }
    }

}
