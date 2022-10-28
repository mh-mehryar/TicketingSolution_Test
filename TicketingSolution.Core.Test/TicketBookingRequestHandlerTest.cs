using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Core;
using Xunit.Sdk;

namespace TicketingSolution.Core
{
    public class Ticket_Booking_Request_Handler_Test
    {
        private readonly TicketBookingRequestHandler _handler;

        public Ticket_Booking_Request_Handler_Test()
        {
            _handler = new TicketBookingRequestHandler();
        }
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

            //var Handler = new TicketBookingRequestHandler();

            //Act
            ServiceBookingResult Result = _handler.BookService(BookingRequest);

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

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            //var Handler = new TicketBookingRequestHandler();
            //Assert.Throws<ArgumentNullException>(() => Handler.BookService(null));
            var exception = Should.Throw<ArgumentNullException>(() => _handler.BookService(null));
            exception.ParamName.ShouldBe("bookingRequest");
        }


    }
}
