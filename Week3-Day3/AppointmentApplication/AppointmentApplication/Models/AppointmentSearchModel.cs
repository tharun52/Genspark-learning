using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApplication.Models
{
    public class AppointmentSearchModel
    {
        public int? Id { get; set; }
        public string? PatientName { get; set; }
        public Range<DateTime>? AppointmentDateRange { get; set; }
        public Range<int>? Age { get; set; }


        public class Range<T>
        {
            public T? MinVal { get; set; }
            public T? MaxVal { get; set; }
        }
    }
}
