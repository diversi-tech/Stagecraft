using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;
using System.Data.SqlClient;

namespace StagecraftApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForumController : ControllerBase
    {
        private readonly IForum _forum;
        public ForumController(IForum forumService)
        {
            _forum = forumService;   
        }
        [HttpGet("GetAllQuestions")]
        public ActionResult<IEnumerable<Question>> GetAllQuestions()
        {
            var question = _forum.GetAllQuestions();
            return Ok(question);
        }
        [HttpPost("AddQuestion")]
        public ActionResult<string> AddQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var t= _forum.AddQuestion(question);
            return Ok(t);
            //return CreatedAtAction(nameof(AddQuestion), new { id = question.QuestionId }, question);

        }
        [HttpPut("UpdateQuestion/{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedQuestion = _forum.UpdateQuestion(id, question);
            if (updatedQuestion == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("DeleteQuestion/{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            var deletedQuestion = _forum.DeleteQuestion(id);
            if (deletedQuestion == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }
            return Ok(deletedQuestion);
        }
        [HttpGet("GetAllAnswers")]
        public ActionResult<IEnumerable<Answer>> GetAllAnswers()
        {
            var question = _forum.GetAllAnswers();
            return Ok(question);
        }
        //[HttpPost("AddAnswer")]
        //public ActionResult<Answer> AddAnswer([FromBody] Answer answer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var newAnswer = _forum.AddAnswer(answer);
        //    return CreatedAtAction(nameof(AddQuestion), new { id = answer.QuestionId }, answer);

        //}
        [HttpGet("GetAnswersByQuestionId/{questionId}")]
        public ActionResult<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId)
        {
            var answers = _forum.GetAnswersByQuestionId(questionId);
            return Ok(answers);
        }

        //[HttpPost("AddAnswer/{questionId}")]
        //public ActionResult<Answer> AddAnswer(int questionId, [FromBody] Answer answer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    answer.QuestionId = questionId;
        //    var newAnswer = _forum.AddAnswer(answer);
        //    return CreatedAtAction(nameof(AddAnswer), new { id = newAnswer.id }, newAnswer);
        //}
        //[HttpPost("AddAnswer/{questionId}")]
        //public ActionResult<Answer> AddAnswer(int questionId, [FromBody] Answer answer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    answer.QuestionId = questionId;
        //    var newAnswer = _forum.AddAnswer(answer);
        //    return CreatedAtAction(nameof(AddAnswer), new { id = newAnswer.AnswerId }, newAnswer);
        //}
        [HttpPost("AddAnswer/{questionId}")]
        public ActionResult<string> AddAnswer(int questionId ,[FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            answer.QuestionId = questionId;
            var newAnswer = _forum.AddAnswer(answer);
            //return CreatedAtAction(nameof(AddAnswer), new { id = newAnswer.AnswerId }, newAnswer);
            return Ok(newAnswer);
            //return CreatedAtAction(nameof(AddQuestion), new { id = question.QuestionId }, question);

        }


        [HttpPut("UpdateAnswer/{id}")]
        public IActionResult UpdateAnswer(int id, [FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedAnswer = _forum.UpdateAnswer(id, answer);
            if (updatedAnswer == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost("DeleteAnswer/{id}")]
        public IActionResult DeleteAnswer1(int id,Answer answer)
        {
            var deletedAnswer = _forum.DeleteAnswer(id,answer);
            if (deletedAnswer == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }
            return Ok(deletedAnswer);
        }

    }

}
