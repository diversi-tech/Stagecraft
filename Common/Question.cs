﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public DateTime ?CreatedAt { get; set; }
        public List<Answer> ?Answers { get; set; }
    }
}
