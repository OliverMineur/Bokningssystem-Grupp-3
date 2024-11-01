using System;

namespace Bokningssystem
{
    internal class Lokal
    {
        public string RoomType { get; set; } = ""; // "Sal" or "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; private set; } = false; // Only modified by class methods

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

        public void Avboka()
        {
            // Bu
            IsBooked = false; // Reset booking status
        }
    }
}
