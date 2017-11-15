using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;
using GeekQuiz.Core;




namespace GeekQuiz.Api.Controllers
{
    // [Authorize]
    
    public class TriviaController : ApiController
    {
        private readonly TriviaContext _db;



        public TriviaController(TriviaContext db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._db.Dispose();
            }

            base.Dispose(disposing);
        }
      
        private async Task<TriviaQuestion> NextQuestionAsync(string userId)
        {
            var lastQuestionId = await this._db.TriviaAnswers
                .Where(a => a.UserId == userId)
                .GroupBy(a => a.QuestionId)
                .Select(g => new { QuestionId = g.Key, Count = g.Count() })
                .OrderByDescending(q => q.Count)
                .ThenByDescending(q => q.QuestionId)
                .Select(q => q.QuestionId)
                .FirstOrDefaultAsync();

            var questionsCount = await this._db.TriviaQuestions.CountAsync();

            var nextQuestionId = (lastQuestionId % questionsCount) + 1;
            return await this._db.TriviaQuestions.Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == nextQuestionId);
        }
        // GET api/Trivia
        [ResponseType(typeof(TriviaQuestion))]
        public async Task<IHttpActionResult> Get()
        {
            var userId = User.Identity.Name;
           // var userId = "test@mail.ru";
            TriviaQuestion nextQuestion = await this.NextQuestionAsync(userId);
            
            if (nextQuestion == null)
            {
                return this.NotFound();
            }

            return this.Ok(nextQuestion);
        }
        private async Task<bool> StoreAsync(TriviaAnswer answer)
        {
            this._db.TriviaAnswers.Add(answer);

            await this._db.SaveChangesAsync();
            var selectedOption = await this._db.TriviaOptions.FirstOrDefaultAsync(o => o.Id == answer.OptionId
                                                                                      && o.QuestionId == answer.QuestionId);

            return selectedOption.IsCorrect;
        }
        [ResponseType(typeof(TriviaAnswer))]
        public async Task<IHttpActionResult> Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            answer.UserId = User.Identity.Name;

            var isCorrect = await this.StoreAsync(answer);
            return this.Ok<bool>(isCorrect);
        }
    }
}
