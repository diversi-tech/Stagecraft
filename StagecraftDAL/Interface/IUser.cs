using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IUser
    {
        double GetUserProgress(int userId, int courseId);
    }
}
