using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceDemo.Service;

namespace WorkerServiceDemo.CronService
{
    public class MainCronService : IHostedService
    {
        /* Expression
            * * * * * *
            | | | | | |
            | | | | | +--- day of week (0 - 6) (Sunday=0)
            | | | | +----- month (1 - 12)
            | | | +------- day of month (1 - 31)
            | | +--------- hour (0 - 23)
            | +----------- min (0 - 59)
            +------------- sec (0 - 59)
         */
        readonly ServiceN1 _serviceN1;
        readonly ServiceN2 _serviceN2;

        List<TaskService<bool>> tasks;

        public MainCronService(ServiceN1 serviceN1, ServiceN2 serviceN2)
        {
            _serviceN1 = serviceN1;
            _serviceN2 = serviceN2;

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
            };
        }

        public void Run(TaskService<bool> task, CancellationToken cancellationToken)
        {
            CrontabSchedule _schedule = CrontabSchedule.Parse(task.Expression, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            var _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {

                    Debug.WriteLine($"Debug start: ${task.Name}");
                    if (task.Fork)
                    {
                        new System.Threading.Thread(() =>
                        {
                            task.Func();
                        }).Start();
                    }
                    else
                    {
                        task.Func();
                    }

                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                Thread.Sleep(1500);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var task in tasks)
            {
                _ = Task.Run(() => this.Run(task, cancellationToken), cancellationToken);
            }
            await Task.Delay(1000);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Debug Stop");
            await Task.CompletedTask;
        }
    }

    public class TaskService<T>
    {
        public string Expression { get; set; }
        public string Name { get; set; }
        public bool Fork { get; set; }

        public Func<bool> Func { get; set; }
    }
}
