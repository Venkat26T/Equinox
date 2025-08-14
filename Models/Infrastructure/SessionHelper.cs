
using Equinox.Models.DomainModels;
using System.Globalization;
using System.Text.Json;

namespace Equinox.Models.Infrastructure
{
    public class SessionHelper
    {
        private readonly ISession _session;

        private const string BookingKey = "Bookings";
        private const string ClubFilterKey = "SelectedClub";
        private const string CategoryFilterKey = "SelectedCategory";
        private const string CartKey = "CartKey";

        public SessionHelper(ISession session)
        {
            _session = session;
        }

        public List<int> GetBookings() =>
            _session.GetObjectFromJson<List<int>>(BookingKey) ?? new List<int>();

        public void SetBookings(List<int> bookings) =>
            _session.SetObjectAsJson(BookingKey, bookings);

        public string GetSelectedClub() =>
            _session.GetString(ClubFilterKey) ?? "All";

        public void SetSelectedClub(string club) =>
            _session.SetString(ClubFilterKey, club);

        public string GetSelectedCategory() =>
            _session.GetString(CategoryFilterKey) ?? "All";

        public void SetSelectedCategory(string category) =>
            _session.SetString(CategoryFilterKey, category);
        public List<Booking> Bookings
        {
            get
            {
                var data = _session.GetString(CartKey);
                return string.IsNullOrEmpty(data)
                    ? new List<Booking>()
                    : JsonSerializer.Deserialize<List<Booking>>(data) ?? new List<Booking>();
            }
            set
            {
                _session.SetString(CartKey, JsonSerializer.Serialize(value));
            }
        }
    }
}
