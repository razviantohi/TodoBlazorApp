﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.API.DataProvider.Entities;
using Todo.Shared;

namespace Todo.API.DataProvider
{
    public class TodosDatabaseProvider : ITodosProvider
    {
        private readonly TodoDbContext context;

        public TodosDatabaseProvider(TodoDbContext context)
        {
            this.context = context;
        }

        public Task<TodoItemModel[]> GetTodos()
        {
            return context.TodoItems.Select(item => MappingUtils.ModelFromEntity(item)).ToArrayAsync();
        }

        public async Task Add(TodoItemModel item)
        {
            item.Id = Guid.NewGuid();

            context.TodoItems.Add(new TodoItem
            {
                PublicId = item.Id.Value,
                IsDone = item.IsDone,
                Title = item.Title,
                Description= item.Description,
                Deadline= item.Deadline,
            });

            await context.SaveChangesAsync();
        }

        public async Task Edit(TodoItemModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("item");
            }

            var entity = await context.TodoItems.FirstOrDefaultAsync(e => e.PublicId == model.Id);
            if (entity == null)
            {
                //TODO: use Result instead of throwing exception
                //https://josef.codes/my-take-on-the-result-class-in-c-sharp/
                throw new Exception($"Cannot find todo with id {model.Id}");
            }
            entity.Title = model.Title;
            entity.IsDone = model.IsDone;
            entity.Description= model.Description;
            entity.Deadline = model.Deadline;
            await context.SaveChangesAsync();
        }

        public async Task Remove(Guid todoId)
        {
            var entity = await context.TodoItems.FirstOrDefaultAsync(item => item.PublicId == todoId);
            if (entity == null)
            {
                //TODO: use Result instead of throwing exception
                //https://josef.codes/my-take-on-the-result-class-in-c-sharp/
                throw new Exception($"Cannot find todo with id {todoId}");
            }
            context.TodoItems.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TodoItemModel> GetTodo(Guid todoId)
        {
            var entity = await context.TodoItems.FirstOrDefaultAsync(item => item.PublicId == todoId);
            if (entity == null)
            {
                //TODO: use Result instead of throwing exception
                //https://josef.codes/my-take-on-the-result-class-in-c-sharp/
                throw new Exception($"Cannot find todo with id {todoId}");
            }

            return MappingUtils.ModelFromEntity(entity);
        }
    }
}
