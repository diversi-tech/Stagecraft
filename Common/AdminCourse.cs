using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AdminCourse
    {
        public int courses_id { get; set; }
        public string courses_name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public int price { get; set; }
        public int Several_chapters { get; set; }
        public TimeOnly? Length { get; set; }
        public int numberOfViewers { get; set; }


    }
}
