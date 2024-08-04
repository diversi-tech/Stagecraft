//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Common;
//using StagecraftDAL.Interface;
//namespace StagecraftDAL.Services
//{
//    public class ForumService : IForum
//    {
//        private List<Question> _questions = new List<Question>();
//        private List<Answer> _answers = new List<Answer>();
//        public List<Answer> AddAnswer(Answer answer)
//        {
//            //SqlParameter param1 = new SqlParameter("@AnswerId", answer.AnswerId);
//            SqlParameter param2 = new SqlParameter("@QuestionId", answer.QuestionId);
//            SqlParameter param3 = new SqlParameter("@Text", answer.Text);
//            SqlParameter param4 = new SqlParameter("@CreatedAt",answer.CreatedAt);


//            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("AddAnswer", param2, param3, param4);
//            Console.WriteLine(t);
//            return t;
//        }

//        public bool AddQuestion(Question question)
//        { 
//            SqlParameter param2 = new SqlParameter("@Text", question.Text);
//            SqlParameter param3 = new SqlParameter("@CreatedAt", question.CreatedAt);


//           DataAccess.ExecuteStoredProcedure("AddQuestion", param2, param3);

//            return true;
//        }

//        public List<Answer> DeleteAnswer(int id)
//        {
//            SqlParameter param1 = new SqlParameter("@AnswerId", id);
//            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("DeleteAnswer", param1);
//            return t;
//        }

//        public List<Question> DeleteQuestion(int id)
//        {
//            SqlParameter param1 = new SqlParameter("@QuestionId", id);
//            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("DeleteQuestion", param1);
//            return t;
//        }

//        public List<Answer> GetAllAnswers()
//        {
//            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("GetAllAnswers", null);
//            return t;
//        }

//        public List<Question> GetAllQuestions()
//        {
//            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("GetAllQuestions", null);
//            return t;
//        }

//        public List<Answer> UpdateAnswer(int id, Answer answer)
//        {
//            SqlParameter param1 = new SqlParameter("@AnswerId", id);
//            SqlParameter param2 = new SqlParameter("@QuestionId", answer.QuestionId);
//            SqlParameter param3 = new SqlParameter("@Text", answer.Text);
//            SqlParameter param4 = new SqlParameter("@CreatedAt", answer.CreatedAt);

//            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("UpdateAnswer", param1, param2, param3, param4);
//            return t;
//        }

//        public List<Question> UpdateQuestion(int id, Question question)
//        {
//            SqlParameter param1 = new SqlParameter("@QuestionId", id);
//            SqlParameter param2 = new SqlParameter("@Text", question.Text);
//            SqlParameter param3 = new SqlParameter("@CreatedAt", question.CreatedAt);

//            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("UpdateQuestion", param1, param2, param3);
//            return t;
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using StagecraftDAL.Interface;

namespace StagecraftDAL.Services
{
    public class ForumService : IForum
    {
        private List<Question> _questions = new List<Question>();
        private List<Answer> _answers = new List<Answer>();

        public bool AddAnswer(Answer answer)
        {
            SqlParameter param2 = new SqlParameter("@QuestionId", answer.QuestionId);
            SqlParameter param3 = new SqlParameter("@Text", answer.Text);
            SqlParameter param4 = new SqlParameter("@CreatedAt", answer.CreatedAt);

            DataAccess.ExecuteStoredProcedure("AddAnswer", param2, param3, param4);          
            return true;
        }

        public bool AddQuestion(Question question)
        {
            SqlParameter param2 = new SqlParameter("@Text", question.Text);
            SqlParameter param3 = new SqlParameter("@CreatedAt", question.CreatedAt);

            DataAccess.ExecuteStoredProcedure("AddQuestion", param2, param3);

            return true;
        }

        public List<Answer> DeleteAnswer(int id,Answer answer)
        {
            SqlParameter param1 = new SqlParameter("@AnswerId", id);
            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("DeleteAnswer", param1);
            return t;
        }

        public List<Question> DeleteQuestion(int id)
        {
            SqlParameter param1 = new SqlParameter("@QuestionId", id);
            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("DeleteQuestion", param1);
            return t;
        }

        public List<Answer> GetAllAnswers()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("GetAllAnswers", null);
            return t;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            SqlParameter param = new SqlParameter("@QuestionId", questionId);

            var answers = DataAccess.ExecuteStoredProcedure<List<Answer>>("GetAnswersByQuestionId", param);

            return answers;
        }


        public List<Question> GetAllQuestions()
        {
            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("GetAllQuestions", null);
            return t;
        }

        public List<Answer> UpdateAnswer(int id, Answer answer)
        {
            SqlParameter param1 = new SqlParameter("@AnswerId", id);
            SqlParameter param2 = new SqlParameter("@QuestionId", answer.QuestionId);
            SqlParameter param3 = new SqlParameter("@Text", answer.Text);
            SqlParameter param4 = new SqlParameter("@CreatedAt", answer.CreatedAt);

            var t = DataAccess.ExecuteStoredProcedure<List<Answer>>("UpdateAnswer", param1, param2, param3, param4);
            return t;
        }

        public List<Question> UpdateQuestion(int id, Question question)
        {
            SqlParameter param1 = new SqlParameter("@QuestionId", id);
            SqlParameter param2 = new SqlParameter("@Text", question.Text);
            SqlParameter param3 = new SqlParameter("@CreatedAt", question.CreatedAt);

            var t = DataAccess.ExecuteStoredProcedure<List<Question>>("UpdateQuestion", param1, param2, param3);
            return t;
        }
    }
}
