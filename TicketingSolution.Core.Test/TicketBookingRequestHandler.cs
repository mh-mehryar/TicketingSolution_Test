namespace TicketingSolution.Core
{
    internal class TicketBookingRequestHandler
    {
        public TicketBookingRequestHandler()
        {
        }

        internal ServiceBookingResult BookingService(TicketBookingRequest bookingRequest)
        {
            return new ServiceBookingResult
            {
                Name = bookingRequest.Name,
                Family = bookingRequest.Family,
                Email = bookingRequest.Email
            };
        }

        
    }
}