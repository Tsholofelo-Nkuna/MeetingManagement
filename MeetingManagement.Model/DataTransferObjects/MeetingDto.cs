using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManagement.Model.DataTransferObjects
{
    public class MeetingDto
    {
        public MeetingType Type { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public long Id { get; set; } = 0;

        public IEnumerable<long> Items { get; set; }
    }
}
