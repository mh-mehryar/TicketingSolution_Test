using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.Core
{
    public class Ticket_Booking_Request_Handler_Test
    {
        [Fact]
        public void Should_Return_Ticket_Booking_Response_With_Request_Values()
        {
            //Arrange
            var BookingRequest = new TicketBookingRequest
            {
                Name = "Test Name",
                Family = "Test Family",
                Email = "Test Email"
            };

            var Handler = new TicketBookingRequestHandler();

            //Act
            ServiceBookingResult Result = Handler.BookingService(BookingRequest);

            //Assert
            //Assert.NotNull(Result);
            //Assert.Equal(Result.Name, BookingRequest.Name);
            //Assert.Equal(Result.Family, BookingRequest.Family);
            //Assert.Equal(Result.Email, BookingRequest.Email);

            //Assert By Shouldly
            Result.ShouldNotBeNull();
            Result.Name.ShouldBe(BookingRequest.Name);
            Result.Family.ShouldBe(BookingRequest.Family);
            Result.Email.ShouldBe(BookingRequest.Email);
        }


    }
}
