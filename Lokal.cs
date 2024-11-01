using System;

namespace Bokningssystem
{
    internal class Lokal
    {
        public String RoomType { get; set; } = ""; // "Sal" or "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; private set; } = false; // Only modified by class methods
        public DateTime BookingStartTime { get; set; }
        public TimeSpan BookingDuration { get; set; }
        public String ClientName { get; set; }
        public int BookingID { get; set; }

        public bool Booked()
        {
            if (IsBooked)
            {
                return false; // Room is already booked
            }

            // Check capacity based on room type
            if ((RoomType == "Sal" && NumberOfChairs > 40) ||
                (RoomType == "Grupprum" && NumberOfChairs > 10))
            {
                return false; // Booking fails due to capacity limit
            }

            IsBooked = true; // Update booking status
            return true; // Booking successful
        }

        public void Unbook()
        {
            IsBooked = false; // Reset booking status
        }
        //Jag har ändrat här!!!
    }
}
