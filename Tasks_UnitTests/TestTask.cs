using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

using Tasks;

//TODO: Setup MSFT Fakes to better test underlying pieces and isolate dependencies
namespace Tasks_UnitTests
{
    [TestClass]
    public class TestTask
    {
        #region Tests for GetSubTasks
        [TestMethod]
        public void GetSubTasks_0()
        {
            Task task = GetDummyTaskList(1).ElementAt<Task>(0);
            List<Task> subTasks = task.GetSubTasks();
            Assert.AreEqual(subTasks.Count, 0);
        }

        [TestMethod]
        public void GetSubTasks_1()
        {
            Task task = GetDummyTaskList(1, 1).ElementAt<Task>(0);
            List<Task> subTasks = task.GetSubTasks();
            Assert.AreEqual(subTasks.Count, 1);
        }

        [TestMethod]
        public void GetSubTasks_5()
        {
            Task task = GetDummyTaskList(1, 5).ElementAt<Task>(0);
            List<Task> subTasks = task.GetSubTasks();
            Assert.AreEqual(subTasks.Count, 5);
        }

        [TestMethod]
        public void GetSubTasks_1_1()
        {
            Task task = GetDummyTaskList(1, 1, 1).ElementAt<Task>(0);
            List<Task> subTasks = task.GetSubTasks();
            Assert.AreEqual(subTasks.Count, 2);
        }

        [TestMethod]
        public void GetSubTasks_5_5()
        {
            Task task = GetDummyTaskList(1, 5, 5).ElementAt<Task>(0);
            List<Task> subTasks = task.GetSubTasks();
            Assert.AreEqual(subTasks.Count, 30); //5 level1 subtasks + 5*5 level2 subtasks = 30
        }
        #endregion

        #region Tests for GetParentTasks
        [TestMethod]
        public void GetParentTasks_0()
        {
            Task task = GetDummyTaskList(1).ElementAt<Task>(0);
            List<Task> parentTasks = task.GetParentTasks();
            Assert.AreEqual(parentTasks.Count, 0);
        }

        [TestMethod]
        public void GetParentTasks_1()
        {
            Task task = GetDummyTaskList(1, 1).ElementAt<Task>(0);
            Task childTask = task.SubTasks.ElementAt<Task>(0);
            List<Task> parentTasks = childTask.GetParentTasks();
            Assert.AreEqual(parentTasks.Count, 1);
        }

        [TestMethod]
        public void GetParentTasks_2()
        {
            Task task = GetDummyTaskList(1, 1, 1).ElementAt<Task>(0);
            Task childTask = task.SubTasks.ElementAt<Task>(0).SubTasks.ElementAt<Task>(0);
            List<Task> parentTasks = childTask.GetParentTasks();
            Assert.AreEqual(parentTasks.Count, 2);
        }
        #endregion

        //produces on demand task lists with up to 2 levels of subtask
        private static List<Task> GetDummyTaskList(int l0 = 0, int l1 = 0, int l2 = 0)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < l0; i++)
            {
                Task level0 = new Task("Task " + i, DateTime.Now, null);
                for (int j = 0; j < l1; j++)
                {
                    Task level1 = new Task("SubTask " + j, DateTime.Now, level0);
                    for (int k = 0; k < l2; k++)
                    {
                        Task level2 = new Task("SubSubTask " + k, DateTime.Now, level1);
                        level1.SubTasks.Add(level2);
                    }
                    level0.SubTasks.Add(level1);
                }
                tasks.Add(level0);
            }
            return tasks;
        }
    }

    [TestClass]
    public class TestTaskList
    {
        #region Test AddTask
        [TestMethod]
        public void AddTask_Root()
        {
            string name = "Root Task";
            TaskList taskList = new TaskList();
            taskList.Reset();

            Task level0_task = new Task(name, DateTime.MaxValue, null);
            taskList.AddTask(level0_task);

            ApplicationDataContainer TaskContainer = taskList.GetTaskContainer();
            ApplicationDataContainer taskContainer = TaskContainer.Containers[level0_task.Id];

            CheckContainerMatch(level0_task, taskContainer);
        }

        [TestMethod]
        public void AddTask_Level1()
        {
            string name = "Level 1 Task";
            TaskList taskList = new TaskList();
            taskList.Reset();

            //Parent Task
            Task level0_task = new Task("Root Task", DateTime.MaxValue, null);
            taskList.AddTask(level0_task);

            //SubTask
            Task level1_task = new Task(name, DateTime.MaxValue, level0_task);
            taskList.AddTask(level1_task);

            ApplicationDataContainer TaskContainer = taskList.GetTaskContainer();
            
            //Ensure the parent container is still valid
            ApplicationDataContainer level0TaskContainer = TaskContainer.Containers[level0_task.Id];
            CheckContainerMatch(level0_task, level0TaskContainer);

            //Check the child container
            ApplicationDataContainer level1TaskContainer = level0TaskContainer.Containers[level1_task.Id];
            CheckContainerMatch(level1_task, level1TaskContainer);
        }
        #endregion

        #region Test DeleteTask
        [TestMethod]
        public void DeleteTask_Root()
        {
            TaskList taskList = new TaskList();
            taskList.Reset();

            Task level0_task = new Task("Level 0 Task", DateTime.MaxValue, null);
            taskList.AddTask(level0_task);

            taskList.DeleteTask(level0_task);

            bool exists = CheckContainerExists(level0_task, taskList.GetTaskContainer());
            Assert.IsFalse(exists);
        }

        public void DeleteTask_Level1()
        {
            string name = "Level 1 Task";
            TaskList taskList = new TaskList();
            taskList.Reset();

            //Parent Task
            Task level0_task = new Task("Root Task", DateTime.MaxValue, null);
            taskList.AddTask(level0_task);

            //SubTask
            Task level1_task = new Task(name, DateTime.MaxValue, level0_task);
            taskList.AddTask(level1_task);

            taskList.DeleteTask(level1_task);

            bool exists = CheckContainerExists(level1_task, taskList.GetTaskContainer());
            Assert.IsFalse(exists);
        }
        #endregion

        #region EditTask
        [TestMethod]
        public void EditTask_Root()
        {
            string name = "Root Task";
            TaskList taskList = new TaskList();
            taskList.Reset();

            Task level0_task = new Task(name, DateTime.MaxValue, null);
            taskList.AddTask(level0_task);
            ApplicationDataContainer taskContainer = taskList.GetTaskContainer().Containers[level0_task.Id];

            Task new_level0_task = new Task(name, DateTime.MinValue, null);
            taskList.EditTask(level0_task, new_level0_task);

            CheckContainerMatch(new_level0_task, taskContainer);
        }

        [TestMethod]
        public void EditTask_Level1()
        {
            string name = "Level 1 Task";
            TaskList taskList = new TaskList();
            taskList.Reset();

            //Parent Task
            Task level0_task = new Task("Root Task", DateTime.MaxValue, null);
            taskList.AddTask(level0_task);

            //SubTask
            Task level1_task = new Task(name, DateTime.MaxValue, level0_task);
            taskList.AddTask(level1_task);

            //New SubTask
            Task new_level1_task = new Task(name, DateTime.MinValue, level0_task);
            taskList.EditTask(level1_task, new_level1_task);

            ApplicationDataContainer TaskContainer = taskList.GetTaskContainer();

            //Ensure the parent container is still valid
            ApplicationDataContainer level0TaskContainer = TaskContainer.Containers[level0_task.Id];
            CheckContainerMatch(level0_task, level0TaskContainer);

            //Check that the child container has been updated
            ApplicationDataContainer level1TaskContainer = level0TaskContainer.Containers[level1_task.Id];
            CheckContainerMatch(new_level1_task, level1TaskContainer);
        }
        
        #endregion

        private void CheckContainerMatch(Task task, ApplicationDataContainer taskContainer)
        {
            Assert.AreEqual(task.Name, taskContainer.Values["Name"]);
            Assert.AreEqual(task.Date.ToString(), taskContainer.Values["Date"]);
            Assert.AreEqual(task.Level, taskContainer.Values["Level"]);
        }

        private bool CheckContainerExists(Task task, ApplicationDataContainer taskContainer)
        {
            try
            {
                ApplicationDataContainer container = taskContainer.CreateContainer(task.Id, ApplicationDataCreateDisposition.Existing);
            }
            catch (System.Exception)
            {
                //Exception means the container does not exist
                return false;
            }
            return true;
        }
    }
}
 