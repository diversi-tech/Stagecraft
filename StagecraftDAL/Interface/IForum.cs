using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IForum
    {
        //List<Answer> GetAllAnswers();
        //List<Answer> AddAnswer(Answer answer);
        //List<Answer> UpdateAnswer(int id, Answer answer);
        //List<Answer> DeleteAnswer(int id);
        //List<Question> GetAllQuestions();
        //bool AddQuestion(Question question);
        //List<Question> UpdateQuestion(int id,Question question);
        //List<Question> DeleteQuestion(int id);
        List<Answer> GetAllAnswers();
        List<Answer> GetAnswersByQuestionId(int questionId); // New method
        bool AddAnswer(Answer answer);
        List<Answer> UpdateAnswer(int id, Answer answer);
        List<Answer> DeleteAnswer(int id, Answer answer);
        List<Question> GetAllQuestions();
        bool AddQuestion(Question question);
        List<Question> UpdateQuestion(int id, Question question);
        List<Question> DeleteQuestion(int id);

    }
}
