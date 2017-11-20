using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GeekQuiz.Core;
using GeekQuiz.Layers.Api.operators;

namespace GeekQuiz.Api.Controllers
{
    // [Authorize]
    
    public class TriviaController : ApiController
    {
        private QuizOperator _qo;

        public TriviaController(QuizOperator qo)
        {
            _qo = qo;
        }



        // GET api/Trivia

        //[ResponseType(typeof(TriviaQuestion))]
        public async Task<IHttpActionResult>Get()
        {

            var nextQuestion =  await _qo.GetNextPunktAsync();

            if (nextQuestion == null)
            {
                return this.NotFound();
            }

            return  Ok(nextQuestion);
        }
        private async Task<bool> StoreAsync(TriviaAnswer answer)
        {
            //this._qr.TriviaAnswers.Add(answer);

            //await this._db.SaveChangesAsync();
            //var selectedOption = await this._db.TriviaOptions.FirstOrDefaultAsync(o => o.Id == answer.OptionId
            //                                                                          && o.QuestionId == answer.QuestionId);

            //return selectedOption.IsCorrect;

            return true;
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
