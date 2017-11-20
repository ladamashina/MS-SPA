using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using GeekQuiz.Core;

namespace GeekQuiz.Layers.Api.services
{
    public class QuestionRepository : IRepository<TriviaQuestion>
    {
        private readonly TriviaContext _db;

        public QuestionRepository(TriviaContext db)
        {
            _db = db;
        }

        public IEnumerable<TriviaQuestion> GetItems()
        {
            return _db.TriviaQuestions;
        }

        public TriviaQuestion GetItemById(int id)
        {
            return _db.TriviaQuestions.Find(id);
        }

        public void Create(TriviaQuestion item)
        {
            throw new NotImplementedException();
        }

        public void Update(TriviaQuestion item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
        public  Task<TriviaQuestion> NextQuestion()
        {
            var s = new Random().Next(1, 44);
            return _db.TriviaQuestions.Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == s); 

        }
    }
}