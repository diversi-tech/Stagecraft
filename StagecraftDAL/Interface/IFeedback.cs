using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IFeedback 
    {
        List<Feedback> GetFeedbackByUserCourseClass( int UserId, int CourseId);
    }
}
