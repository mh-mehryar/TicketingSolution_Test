using System.Xml.Linq;
using TicketingSolution.Core.DataService;
using TicketingSolution.Core.Domain;

namespace TicketingSolution.Core
{
    public class TicketBookingRequestHandler
    {
        private readonly ITicketBookingService _ticketBookingService;

        public TicketBookingRequestHandler(ITicketBookingService ticketBookingService)
        {
            _ticketBookingService = ticketBookingService;
        }

        public ServiceBookingResult BookService(TicketBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            //_ticketBookingService.Save(new TicketBooking
            //{
            //    Name = bookingRequest.Name,
            //    Family = bookingRequest.Family,
            //    Email = bookingRequest.Email
            //});

            var availableTickets = _ticketBookingService.GetAvailableTickets(bookingRequest.Date);

            if (availableTickets.Any())
            {
                var Ticket = availableTickets.First();
                var TicketBooking = CreatBookingObject<TicketBooking>(bookingRequest);
                TicketBooking.TicketId = Ticket.Id;
            _ticketBookingService.Save(TicketBooking);
            }


            //return new ServiceBookingResult
            //{
            //    Name = bookingRequest.Name,
            //    Family = bookingRequest.Family,
            //    Email = bookingRequest.Email
            //}; 
            return (CreatBookingObject<ServiceBookingResult>(bookingRequest));

        }

        private static TTicketBooking CreatBookingObject<TTicketBooking>(TicketBookingRequest bookingRequest) where TTicketBooking
            : ServiceBookingBase, new()
        {
            return new TTicketBooking
            {
                Name = bookingRequest.Name,
                Family = bookingRequest.Family,
                Email = bookingRequest.Email
            };


        }



    }
}