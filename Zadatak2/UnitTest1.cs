using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadatak1;
using System.Collections.Generic;
using System.Linq;

namespace Zadatak2
{
    [TestClass]
    public class TodoRepositoryTests
    {
        // Add()
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            TodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }






        // Get()
        [TestMethod]
        public void GetExistingItemWillReturnItem()
        {
            ITodoRepository repository = new TodoRepository();
            var expectedItem = new TodoItem("abc");
            repository.Add(expectedItem);
            var actualItem = repository.Get(expectedItem.Id);
            Assert.AreEqual(expectedItem, actualItem);
        }

        [TestMethod]
        public void GetExistingItemWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            repository.Add(insertedItem);
            Guid newId;
            do
            {
                newId = new Guid();
            } while (newId.Equals(insertedItem.Id));

            var actualItem = repository.Get(newId);
            Assert.AreEqual(null, actualItem);
        }






        //Remove()
        [TestMethod]
        public void RemoveExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            var insertedItem2 = new TodoItem("abcd");
            repository.Add(insertedItem);
            //  repository.Add(insertedItem2);
            int initialSize = repository.GetAll().Count;

            bool expected = true;
            bool actual = repository.Remove(insertedItem.Id);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(1, initialSize - repository.GetAll().Count);

        }

        [TestMethod]
        public void RemoveNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            // var insertedItem2 = new TodoItem("abcd");
            repository.Add(insertedItem);
            //  repository.Add(insertedItem2);
            int initialSize = repository.GetAll().Count;

            Guid newId;
            do
            {
                newId = new Guid();
            } while (newId.Equals(insertedItem.Id));

            bool expected = false;
            bool actual = repository.Remove(newId);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, initialSize - repository.GetAll().Count);

        }





        //Update()
        [TestMethod]
        public void UpdateNonExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            repository.Add(insertedItem);
            int initialSize = repository.GetAll().Count;

            TodoItem insertedItem2;
            do
            {
                insertedItem2 = new TodoItem("abc");
            } while (insertedItem2.Id.Equals(insertedItem.Id));

            repository.Update(insertedItem2);
            Assert.AreEqual(1, repository.GetAll().Count - initialSize);
            Assert.IsTrue(repository.GetAll().Any(el => el.Id.Equals(insertedItem2.Id)));

        }

        [TestMethod]
        public void UpdateExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            repository.Add(insertedItem);
            int initialSize = repository.GetAll().Count;

            insertedItem.Text = ("abcd");
            repository.Update(insertedItem);

            Assert.AreEqual(0, repository.GetAll().Count - initialSize);
            Assert.IsTrue(repository.GetAll()
                                    .Where(el => el.Id.Equals(insertedItem.Id))
                                    .FirstOrDefault()
                                    .Text
                                    .Equals(insertedItem.Text));


        }






        //MarkAsCompleted()
        [TestMethod]
        public void MarkAsCompletedTest()
        {
            ITodoRepository repository = new TodoRepository();
            var insertedItem = new TodoItem("abc");
            repository.Add(insertedItem);

            repository.MarkAsCompleted(insertedItem.Id);
            Assert.IsTrue(repository.GetAll()
                                   .Where(el => el.Id.Equals(insertedItem.Id))
                                   .FirstOrDefault()
                                   .IsCompleted
                                   .Equals(insertedItem.IsCompleted));
        }





        // GetAll()
        [TestMethod]
        public void GetAllWhenEmpty()
        {
            ITodoRepository repository = new TodoRepository();
            List<TodoItem> list = repository.GetAll();
            Assert.IsFalse(list.Any());
        }


        [TestMethod]
        public void GetAllTest()
        {
            int listSize = 10;
            ITodoRepository repository = new TodoRepository();
            List<TodoItem> itemList = new List<TodoItem>();
            for (int i = 0; i < listSize; i++)
            {
                TodoItem newItem;
                itemList.Add(newItem = new TodoItem(String.Format("{0}", i)));
                repository.Add(newItem);
            }

            List<TodoItem> newItemList = repository.GetAll();
            Assert.AreEqual(itemList.Count, newItemList.Count);
            itemList = itemList.OrderByDescending(i => i.DateCreated).ToList();

            bool theSame = true;
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemList[i].Id.Equals(newItemList[i].Id))
                {
                    theSame = false;
                }
            }
            Assert.IsTrue(theSame);
        }


        // GetCompleted()
        [TestMethod]
        public void GetCompletedTest()
        {
            int listSize = 10;
            ITodoRepository repository = new TodoRepository();
            List<TodoItem> itemList = new List<TodoItem>();

            for (int i = 0; i < listSize; i++)
            {
                TodoItem newItem;
                itemList.Add(newItem = new TodoItem(String.Format("{0}", i)));
                repository.Add(newItem);
            }

            repository.MarkAsCompleted(itemList[2].Id);
            repository.MarkAsCompleted(itemList[4].Id);

            List<TodoItem> newItemList = repository.GetCompleted();
            newItemList.OrderBy(el => el.Id);
            itemList.OrderBy(el => el.Id);
            Assert.AreEqual(2, newItemList.Count);

            Assert.IsTrue(newItemList[0].Id == itemList[2].Id
                            && newItemList[1].Id == itemList[4].Id);


        }




        //GetActive
        [TestMethod]
        public void GetActiveTest()
        {
            int listSize = 10;
            ITodoRepository repository = new TodoRepository();
            List<TodoItem> itemList = new List<TodoItem>();

            for (int i = 0; i < listSize; i++)
            {
                TodoItem newItem;
                itemList.Add(newItem = new TodoItem(String.Format("{0}", i)));
                repository.Add(newItem);
            }

            repository.MarkAsCompleted(itemList[2].Id);
            repository.MarkAsCompleted(itemList[4].Id);

            List<TodoItem> newItemList = repository.GetActive();
            newItemList.OrderBy(el => el.Id);
            itemList.OrderBy(el => el.Id);
            Assert.AreEqual(8, newItemList.Count);
        }
    }
}