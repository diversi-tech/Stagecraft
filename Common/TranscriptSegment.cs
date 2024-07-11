using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TranscriptSegment
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int Time { get; set; }  // Assuming Time is stored as an int in seconds
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
