﻿using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Core;
using TicketingSolution.Core.DataService;
using TicketingSolution.Core.Domain;
using Xunit.Sdk;

namespace TicketingSolution.Core
{
    public class Ticket_Booking_Request_Handler_Test
    {
        private readonly TicketBookingRequestHandler _handler;
        private readonly TicketBookingRequest _request;
        private readonly Mock<ITicketBookingService> _ticketBookingServiceMock;
        private List<Ticket> _availableTickets;

        public Ticket_Booking_Request_Handler_Test()
        {

            //Arrange
            _request = new TicketBookingRequest
            {
                Name = "Test Name",
                Family = "Test Family",
                Email = "Test Email",
                Date = DateTime.Now
            };

            _availableTickets = new List<Ticket>() { new Ticket() { Id = 1 } };
            _ticketBookingServiceMock = new Mock<ITicketBookingService>();
            _ticketBookingServiceMock.Setup(q => q.GetAvailableTickets(_request.Date))
                .Returns(_availableTickets);
            _handler = new TicketBookingRequestHandler(_ticketBookingServiceMock.Object);
        }
        [Fact]
        public void Should_Return_Ticket_Booking_Response_With_Request_Values()
        {
            //Arrange//
            //var BookingRequest = new TicketBookingRequest
            //{
            //    Name = "Test Name",
            //    Family = "Test Family",
            //    Email = "Test Email"
            //};

            //var Handler = new TicketBookingRequestHandler();

            //Act
            ServiceBookingResult Result = _handler.BookService(_request);

            //Assert
            //Assert.NotNull(Result);
            //Assert.Equal(Result.Name, BookingRequest.Name);
            //Assert.Equal(Result.Family, BookingRequest.Family);
            //Assert.Equal(Result.Email, BookingRequest.Email);

            //Assert By Shouldly
            Result.ShouldNotBeNull();
            Result.Name.ShouldBe(_request.Name);
            Result.Family.ShouldBe(_request.Family);
            Result.Email.ShouldBe(_request.Email);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            //var Handler = new TicketBookingRequestHandler();
            //Assert.Throws<ArgumentNullException>(() => Handler.BookService(null));
            var exception = Should.Throw<ArgumentNullException>(() => _handler.BookService(null));
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Ticket_Booking_Request()
        {
            TicketBooking SavedBooking = null;
            _ticketBookingServiceMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
                .Callback<TicketBooking>(booking =>
                {
                    SavedBooking = booking;
                });
            _handler.BookService(_request);

            _ticketBookingServiceMock.Verify(x => x.Save(It.IsAny<TicketBooking>()),Times.Once());
            SavedBooking.ShouldNotBeNull();
            SavedBooking.Name.ShouldBe(_request.Name);
            SavedBooking.Family.ShouldBe(_request.Family);
            SavedBooking.Email.ShouldBe(_request.Email);
            SavedBooking.TicketId.ShouldBe(_availableTickets.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Ticket_Booking_Request_If_None_Available()
        {
            _availableTickets.Clear();
            _handler.BookService(_request);
            _ticketBookingServiceMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Never());

        }
    }
}
