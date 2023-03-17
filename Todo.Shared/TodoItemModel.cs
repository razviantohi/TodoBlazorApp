using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared
{
    public class TodoItemModel
    {
        public TodoItemModel()
        {
        }

        public TodoItemModel(Guid id, string title = "", bool isDone = false, string description = "", DateTime deadline = default)
        {
            Id = id;
            Title = title;
            IsDone = isDone;
            Description = description;
            Deadline = deadline;
        }

        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Please make the title shorter")]
        public string Title { get; set; }

        public bool IsDone { get; set; }

        [MaxLength(1000, ErrorMessage = "Maximum 1000 Characters")]
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
