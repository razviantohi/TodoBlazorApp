using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using Todo.Shared;

namespace TodoBlazorWasm.Components
{
    public partial class QuickAdd
    {
        [Required]
        public string Title { get; set; }

        [Parameter]
        public EventCallback<TodoItemModel> CloseEventCallback { get; set; }
        protected bool ShowModal { get; set; }

        public void Open()
        {
            ShowModal = true;
        }

        public void Close()
        {
            ShowModal = false;
        }

        public void OnCancelClicked()
        {
            Close();
        }

        public async void OnSaveClicked() 
        {
            var todoItem = new TodoItemModel()
            {
                Title = Title,
                IsDone = false
            };
            await CloseEventCallback.InvokeAsync(todoItem);
            Close();
        }
    }
}
