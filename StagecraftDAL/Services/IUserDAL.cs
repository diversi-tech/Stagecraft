using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Services
{
    public interface IUserDAL
    {
        int GetUserProgress(int userId, int courseId);
    }
}
