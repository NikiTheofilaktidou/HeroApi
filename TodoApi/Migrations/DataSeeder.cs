using TodoApi.Models;

namespace TodoApi.Migrations
{
    public class DataSeeder
    {
        private readonly TodoContext todoContext;
        public DataSeeder(TodoContext todoContext)
        {
            this.todoContext = todoContext;
        }
        public void Seed()
        {
            if (!todoContext.TodoItems.Any())
            {
                var todoItems = new List<TodoItem>()
                {
                        new TodoItem()
                        {
                            Id=1,
                            Name = "Walk the dog",
                            IsComplete = true

                        },
                        new TodoItem()
                        {
                            Id=2,
                            Name = "Go to the store",
                            IsComplete = false
                        },
                        new TodoItem()
                        {
                            Id=3,
                            Name = "Go jogging",
                            IsComplete = true
                        },
                        new TodoItem()
                        {
                            Id=4,
                            Name = "Wash the car",
                            IsComplete = false
                        },
                        new TodoItem()
                        {
                            Id=5,
                            Name = "Buy toys for children",
                            IsComplete = true
                        },
                        new TodoItem()
                        {
                            Id=6,
                            Name = "Go to movies",
                            IsComplete = false
                        }
                };
                todoContext.TodoItems.AddRange(todoItems);
                todoContext.SaveChanges();
            }
        }
    }
}
