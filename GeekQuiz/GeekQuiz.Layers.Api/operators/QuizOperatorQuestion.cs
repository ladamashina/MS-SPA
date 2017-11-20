using System.Threading;
using System.Threading.Tasks;
using GeekQuiz.Core;
using GeekQuiz.Layers.Api.services;

namespace GeekQuiz.Layers.Api.operators
{
    public class QuizOperatorQuestion 

    {
        private readonly AnswerRepository _answerRep;
        private readonly QuestionRepository _questionRep;
        public QuizOperatorQuestion(AnswerRepository answerRep, QuestionRepository questionRep)
        {
            _answerRep = answerRep;
            _questionRep = questionRep;
        }
        
        public async Task<TriviaQuestion> GetNextPunktAsync()
        {
            var a = await _questionRep.NextQuestion().ConfigureAwait(false);
            return a;
        }
        public async Task<TriviaOption> AnswerCheker(TriviaAnswer answer)
        {
            return await _answerRep.IsAnswerTrue(answer);
        }


        

    }
}