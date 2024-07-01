using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserCourses
    {
        public int user_course_id { get; set; }
        public int user_id { get; set; }
        public int courses_id { get; set; }
        public int progress { get; set; }
        public bool is_approved { get; set; }
        public DateTime created_at { get; set; }
        public DateTime apdated_at { get; set; }

    }
}
