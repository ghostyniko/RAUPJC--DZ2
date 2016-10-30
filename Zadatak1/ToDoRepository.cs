using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new List<TodoItem>();
            }
        }




        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(el => el.Id.Equals(todoId))
                                        .FirstOrDefault();
        }




        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException();
                  
            if (_inMemoryTodoDatabase.Where(el => el.Id.Equals(todoItem.Id))
                                     .FirstOrDefault()==null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
               throw new DuplicateTodoItemException (String.Format("duplicate id: {0}",todoItem.Id));
                //throw new DuplicateTodoItemException();

            }
        }




        public bool Remove(Guid todoId)
        {
            var element = _inMemoryTodoDatabase.Where(el => el.Id.Equals(todoId))
                                              .FirstOrDefault();
            if (element == null) return false;

            Console.WriteLine(_inMemoryTodoDatabase.Count);
            Console.WriteLine (_inMemoryTodoDatabase.Remove(element));
            Console.WriteLine(element.Id);
            Console.WriteLine(todoId);
            Console.WriteLine(_inMemoryTodoDatabase.Count);

            return true;
        }




        public void Update(TodoItem todoItem)
        {
            var element= _inMemoryTodoDatabase.Where(el => el.Id.Equals(todoItem.Id))
                                             .FirstOrDefault();
            if (element==null) Add(todoItem);
            else
            {
                element.Update(todoItem);
            }
        }




        public bool MarkAsCompleted(Guid todoId)
        {
            var element = _inMemoryTodoDatabase.Where(el => el.Id.Equals(todoId))
                                             .FirstOrDefault();
            if (element == null) return false;
            element.IsCompleted = true;
            return true;
        }
       

        //Creates a new TodoItem object referenced by "temp".
        //The new object is a clone of the object "i"

        Func<TodoItem, TodoItem> createNewElement = (TodoItem i) =>
        {
            TodoItem temp = new TodoItem("");
            temp.Update(i);
            return temp;

        };


        public List<TodoItem> GetAll()
        {
            List<TodoItem> newList = _inMemoryTodoDatabase.Select(createNewElement)
                                                          .OrderByDescending(i => i.DateCreated)
                                                          .ToList();

            return newList;                              
        }


        public List<TodoItem> GetCompleted()
        {

            List<TodoItem> newList = _inMemoryTodoDatabase.Where(i => i.IsCompleted == true)
                                                          .Select(createNewElement)
                                                          .ToList();
            return newList;
        }



        public List<TodoItem> GetActive()
        {
            List<TodoItem> newList = _inMemoryTodoDatabase.Where(i => i.IsCompleted == false)
                                                         .Select(createNewElement)
                                                         .ToList();
            return newList;
        }



        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            List<TodoItem> newList = _inMemoryTodoDatabase.Where(filterFunction)
                                                         .Select(createNewElement)
                                                         .ToList();
            return newList;
        }

    }
}

