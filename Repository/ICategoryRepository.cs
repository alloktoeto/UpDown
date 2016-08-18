using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpDown.Repository
{
    interface ICategoryRepository<T>
    {
        List<T> GetCategories();
        T GetCategory(int id);
        void AddCategory(T obj);
        void UpdateCategory(T obj);
    }
}
