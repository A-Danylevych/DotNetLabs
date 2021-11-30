using System.Collections.Generic;

namespace Models.Status
{
    public static class Status
    {
        public static Dictionary<StatusEnum, string> Statuses { get; } = new()
        {
            {StatusEnum.Available, "Available"},
            {StatusEnum.Booked, "Booked"},
            {StatusEnum.Sold, "Sold"},
        };
    }
}