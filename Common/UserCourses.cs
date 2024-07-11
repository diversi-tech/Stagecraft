using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserCourses
    {
        public int userCourseId { get; set; }
        public int userId { get; set; }
        public int coursesId { get; set; }
        public int progress { get; set; }
        public bool isApproved { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime apdatedAt { get; set; }

    }
}
