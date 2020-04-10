using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Jeyasuriyaa200423994.Models
{
    public class ToDoListModel
    {
        public int todo_id { get; set; }
        [DisplayName("To-Do")]
        public string todo_item { get; set; }
        [DisplayName("Description")]
        public string todo_description { get; set; }
    }
}
