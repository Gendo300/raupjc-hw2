using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak3;

namespace raupjc_hw2_zad2
{   
    /// <summary>
    /// Class that encapsulates all the logic for accessing TodoItems
    /// </summary>
    public class TodoRepository : ITodoRepository
    {

        /// <summary>
        /// Repository does not fetch todoItems from the actual database,
        /// it uses in memory storage for this exercise;
        /// </summary>
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository()
        {
            _inMemoryTodoDatabase=new GenericList<TodoItem>();
        }

        public TodoRepository(int initialSize)
        {
            _inMemoryTodoDatabase=new GenericList<TodoItem>(initialSize);
        }

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if (!_inMemoryTodoDatabase.Contains(todoItem))
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                throw new DuplicateTodoItemException("duplicate id:" + todoItem.Id);
            }
            return this.Get(todoItem.Id);
        }

        public TodoItem Get(Guid todoId)
        {
            var todoItem = (_inMemoryTodoDatabase.Where(item => item.Id == todoId))
                                                      .FirstOrDefault();
            return todoItem;
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            List<TodoItem> list = _inMemoryTodoDatabase.Where(item => item.IsCompleted).ToList();
            return list;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            var list = _inMemoryTodoDatabase.Where(item => filterFunction(item)).OrderBy(item=>item.DateCreated).ToList();
            return list;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            return this.Get(todoId).MarkAsCompleted();
        }

        public bool Remove(Guid todoId)
        {
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public TodoItem Update(TodoItem todoItem)
        {
            var todoItemNew = (_inMemoryTodoDatabase.Where(item => item.Id == todoItem.Id)).FirstOrDefault();
            if (todoItemNew == null)
            {
                return this.Add(todoItem);
            }
            else
            {
                todoItemNew = todoItem;
                return todoItemNew;
            }
        }
    }
}
