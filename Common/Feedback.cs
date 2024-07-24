using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class Feedback
    {
        public int user_id { get; set; }
        public int class_id { get; set; }
        public int courses_id { get; set; }

        public string file_path { get; set; }
        public string feedback_text { get; set; }



    }
}
