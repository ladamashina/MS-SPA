using System.Collections.Generic;

namespace GeekQuiz.Layers.Api.services
{
    public interface IRepository<T1> where T1: class 
    {
        IEnumerable<T1> GetItems(); // получение всех объектов
        T1 GetItemById(int id); // получение одного объекта по id
        void Create(T1 item); // создание объекта
        void Update(T1 item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
