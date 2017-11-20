using System;
using System.Collections.Generic;
using System.Data.Entity;
using GeekQuiz.Core;

namespace GeekQuiz.Layers.Api.services
{
    public class OptionRepository : IRepository<TriviaOption>
    {
        private readonly TriviaContext _db;

        public OptionRepository(TriviaContext db)
        {
            _db = db;
        }

        public IEnumerable<TriviaOption> GetItems()
        {
            return _db.TriviaOptions;
        }

        public TriviaOption GetItemById(int id)
        {
            return _db.TriviaOptions.Find(id);
        }

        public void Create(TriviaOption item)
        {
            throw new NotImplementedException();
        }

        public void Update(TriviaOption item)
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
    }
}