using book_editor.data.DB.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.Utility
{
    public interface IRepository<T> : IDisposable
         where T : BaseTable
    {

        IQueryable<T> GetCollection(); // получение всех объектов
        T Get(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void HardDelete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
        Task SaveAsync();// сохранение изменений асинхронно
        IQueryable<T> GetCollectionWithDeleted();//Получения всех записей 
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void Delete(IEnumerable<T> entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);
    }

}
