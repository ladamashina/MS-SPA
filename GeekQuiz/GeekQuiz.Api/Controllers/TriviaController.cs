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
        private QuizOperatorQuestion _qo;

        public TriviaController(QuizOperatorQuestion qo)
        {
            _qo = qo;
        }



        // GET api/Trivia

        
        public async Task<IHttpActionResult>Get()
        {

            var nextQuestion = await _qo.GetNextPunktAsync();

            if (nextQuestion == null)
            {
                return NotFound();
            }

            return  Ok(nextQuestion);
        }
        

       // [ResponseType(typeof(TriviaAnswer))]
        public async Task<IHttpActionResult> Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            answer.UserId = User.Identity.Name;

            var result = await _qo.AnswerCheker(answer);
            bool isCorrect = result.IsCorrect;
            return Ok(isCorrect);
        }
    }
}
