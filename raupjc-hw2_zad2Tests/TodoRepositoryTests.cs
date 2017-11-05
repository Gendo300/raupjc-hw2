using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using raupjc_hw2_zad2;
using GenericList;

namespace raupjc_hw2_zad2Tests
{
    public delegate bool ContainsCollegeFilter(TodoItem item);

    [TestClass()]
    public class TodoRepositoryTests
    {
        private const int TestSize = 20;
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TodoRepositoryTest()
        {
           var goodRepo= new TodoRepository();
           var alsoGoodRepo= new TodoRepository(5);
           var badRepo=new TodoRepository(-1);
           var alsoBadRepo=new TodoRepository(0);
        }

        [TestMethod()]
        public void TodoRepositoryTest1()
        {
            
        }


        [TestMethod()]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddTest()
        {
            var todoRepo=new TodoRepository(TestSize);
            var exampleItem =new TodoItem("Test example");
            todoRepo.Add(exampleItem);
            todoRepo.Add(exampleItem);

        }

        [TestMethod()]
        public void GetTest()
        {
            String expected = "todo5";
            String actual;
            var todoRepo=new TodoRepository(TestSize);
            var todoArray=new TodoItem[TestSize];
            for(int i=0; i<TestSize; i++)
            {
                todoArray[i]=new TodoItem("todo"+i);
                todoRepo.Add(todoArray[i]);
            }
            actual = todoRepo.Get(todoArray[5].Id).Text;
            Assert.AreEqual(expected,actual);

        }

        [TestMethod()]
        public void GetAllTest()
        {
            TodoItem item;
            var todoRepo = new TodoRepository(TestSize);
            List<TodoItem> todoListAdded = new List<TodoItem>(TestSize);
            List<TodoItem> todoListGot = new List<TodoItem>(TestSize);
            for (int i = 0; i < TestSize; i++)
            {
                item = new TodoItem("item" + i);
                todoListAdded.Add(item);
                todoRepo.Add(item);
            }
            todoListAdded = todoListAdded.OrderBy(todo => todo.DateCreated).ToList();
            todoListGot = todoRepo.GetAll();
            CollectionAssert.AreEqual(todoListGot,todoListAdded);
        }

        [TestMethod()]
        public void GetCompletedTest()
        {
            TodoItem item;
            List<TodoItem> expected=new List<TodoItem>(TestSize);
            List<TodoItem> got=new List<TodoItem>(TestSize);
            TodoRepository todoRepo=new TodoRepository(TestSize);

            for (int i = 0; i < TestSize; i++)
            {
                item=new TodoItem("todo"+ i);
                todoRepo.Add(item);

                if (i % 2 == 0)
                {
                    todoRepo.MarkAsCompleted(item.Id);
                    expected.Add(todoRepo.Get(item.Id));
                }
            }
            got = todoRepo.GetCompleted();
            CollectionAssert.AreEqual(expected, got);
        }

        [TestMethod()]
        public void GetFilteredTest()
        {
            List<TodoItem> expected=new List<TodoItem>(TestSize);
            List<TodoItem> got=new List<TodoItem>(TestSize);
            TodoItem todoItem;
            TodoRepository todoRepo=new TodoRepository(TestSize);

            todoItem= new TodoItem("grocery");
            todoRepo.Add(todoItem);

            todoItem= new TodoItem("College homework");
            todoRepo.Add(todoItem);
            expected.Add(todoItem);

            todoItem= new TodoItem("House cleaning");
            todoRepo.Add(todoItem);

            todoItem=new TodoItem("College test");
            expected.Add(todoItem);
            todoRepo.Add(todoItem);

            got = todoRepo.GetFiltered(ContainsCollegeFilter);
            CollectionAssert.AreEqual(expected,got);

        }

        [TestMethod()]
        public void MarkAsCompletedTest()
        {
            TodoRepository todoRepo=new TodoRepository(TestSize);
            for (int i = 0; i < TestSize; i++)
            {
                todoRepo.Add(new TodoItem("item" + i));
            }
            todoRepo.MarkAsCompleted(todoRepo.GetAll()[4].Id);
            if (!todoRepo.GetAll()[4].IsCompleted)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoveTest()
        {
            TodoItem item;
            List<TodoItem> expectedItems=new List<TodoItem>(TestSize);
            List<TodoItem> gotItems=new List<TodoItem>(TestSize);
            TodoRepository todoRepo = new TodoRepository(TestSize);
            for (int i = 0; i < TestSize; i++)
            {
                item = new TodoItem("item" + i);
                todoRepo.Add(item);
                if (i < 2 && i > 5)
                {
                    expectedItems.Add(item);
                }
            }
            todoRepo.Remove(todoRepo.GetAll()[2].Id);
            todoRepo.Remove(todoRepo.GetAll()[3].Id);
            todoRepo.Remove(todoRepo.GetAll()[4].Id);
            todoRepo.Remove(todoRepo.GetAll()[5].Id);
            CollectionAssert.AreEqual(expectedItems,gotItems);
            
        }

        [TestMethod()]
        public void UpdateTest()
        {
            TodoItem item;
            TodoItem expected;
            TodoItem got;
            TodoRepository todoRepo=new TodoRepository(TestSize);
            item=new TodoItem("old");
            todoRepo.Add(item);
            item.Text = "new";
            expected = item;
            todoRepo.Update(item);
            got = todoRepo.Get(item.Id);
            Assert.AreEqual(got,expected);
        }

        static bool ContainsCollegeFilter(TodoItem todoItem)
        {
            if (todoItem.Text.ToLower().Contains("college"))
            {
                return true;
            }
            return false;
        }
    }
}