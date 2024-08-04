using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;

namespace Common
{

    public class AdminCourse
    {
        public int? courses_id { get; set; }
        public string courses_name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public int price { get; set; }
        public int Several_chapters { get; set; }
        public string Length { get; set; }
        public int numberOfViewers { get; set; }
        public string? videoURL { get; set; }
        public string? taskFilesURL { get; set; }
        public IFormFile? videoFile { get; set; }
        public IFormFile? taskFile { get; set; }

        public AdminCourse()
        {
            courses_name = string.Empty;
            // אתחול השדה לערך ברירת מחדל שאינו null
        }

    }
}
