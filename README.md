# WorkerServiceDemo
WorkerService + Net Core 3 (C#)

Use: NCrontab 
link: https://github.com/atifaziz/NCrontab

Add Task Simple

 tasks = new List<TaskService<bool>>()
{
    new TaskService<bool>()
    {
        Expression = "*/3 * * * * *",
        Name = "service 1",
        Fork = false,
        Func = _serviceN1.DoWork
    },
    new TaskService<bool>()
    {
        Expression = "*/5 * * * * *",
        Name = "service 2",
        Fork = false,
        Func = _serviceN1.DoWork
    },
    new TaskService<bool>()
    {
        Expression = "*/1 * * * * *",
        Name = "service 3",
        Fork = false,
        Func = _serviceN2.DoWork
    }
