using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Jeyasuriyaa200423994.Models;

namespace Jeyasuriyaa200423994.Controllers
{
    public class ToDoListController : Controller
    {
        
        string connectionString = @"server=(LocalDB)\MSSQLLocalDB;Initial Catalog=ToDoList;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblList = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT * FROM [dbo].[ToDoList]", sqlCon);
                sqlData.Fill(dtblList);
            
            }

            return View(dtblList);
        }


        // GET: ToDoList/Create
        public ActionResult Create()
        {
            return View(new ToDoListModel());
        }

        // POST: ToDoList/Create
        [HttpPost]
        public ActionResult Create(ToDoListModel toDoListModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO [dbo].[ToDoList] values (@todo_item,@todo_description)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@todo_item", toDoListModel.todo_item);
                sqlCmd.Parameters.AddWithValue("@todo_description", toDoListModel.todo_description);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: ToDoList/Edit/5
        public ActionResult Edit(int id)
        {
            ToDoListModel toDoListModel = new ToDoListModel();
            DataTable dtblTodo = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM [dbo].[ToDoList] WHERE todo_id = @ToDoId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ToDoId", id);
                sqlDa.Fill(dtblTodo);
            }
            if (dtblTodo.Rows.Count == 1)
            {
                toDoListModel.todo_id = Convert.ToInt32(dtblTodo.Rows[0][0].ToString());
                toDoListModel.todo_item = dtblTodo.Rows[0][1].ToString();
                toDoListModel.todo_description = dtblTodo.Rows[0][2].ToString(); ;
                return View(toDoListModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: ToDoList/Edit/5
        [HttpPost]
        
        public ActionResult Edit(ToDoListModel toDoListModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE [dbo].[ToDOList] SET todo_item = @todo_item, todo_description = @todo_description WHERE todo_id = @todoId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@todo_item", toDoListModel.todo_item);
                sqlcmd.Parameters.AddWithValue("@todo_description", toDoListModel.todo_description);
                sqlcmd.Parameters.AddWithValue("@todoId", toDoListModel.todo_id);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: ToDoList/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM [dbo].[ToDoList] WHERE todo_id = @ToDoId ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@ToDoId", id);
                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

    }
}