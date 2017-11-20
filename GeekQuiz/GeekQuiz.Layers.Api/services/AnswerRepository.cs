using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using GeekQuiz.Core;

namespace GeekQuiz.Layers.Api.services
{
    public class AnswerRepository : IRepository<TriviaAnswer>
    {
        private readonly TriviaContext _db;

        public AnswerRepository(TriviaContext db)
        {
            _db = db;
        }

        public IEnumerable<TriviaAnswer> GetItems()
        {
            return _db.TriviaAnswers;
        }

        public TriviaAnswer GetItemById(int id)
        {
            return _db.TriviaAnswers.Find(id);
        }

        public void Update(TriviaAnswer item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(TriviaAnswer item)
        {
            throw new NotImplementedException();
        }

        public async Task<TriviaOption> IsAnswerTrue(TriviaAnswer answer)
        {
            
            var selectedOption = await _db.TriviaOptions.FirstOrDefaultAsync(o => o.Id == answer.OptionId && o.QuestionId == answer.QuestionId);

            return selectedOption;
        }
    }
}