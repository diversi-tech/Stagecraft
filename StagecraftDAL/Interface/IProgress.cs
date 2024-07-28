using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IProgressService
    {
        int UpdateUserProgress(int userId, int courseId, int classId);
    }
}
