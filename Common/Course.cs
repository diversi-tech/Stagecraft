using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Course
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string NumberOfChapters { get; set; }
        public int Price { get; set; }
        public List<Recommendation> Recommendations { get; set; }




        public string Description { get; set; }
    }
}
