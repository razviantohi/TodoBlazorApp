using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.API.DataProvider.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public bool IsDone { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
