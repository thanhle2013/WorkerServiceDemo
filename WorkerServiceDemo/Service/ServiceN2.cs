using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceDemo.Service
{
    public class ServiceN2
    {
        ILogger<ServiceN2> _logger;
        public ServiceN2(ILogger<ServiceN2> logger)
        {
            _logger = logger;
        }

        public bool DoWork()
        {
            //_logger.LogInformation($"Service starts. {DateTime.Now.ToString("F")}");
            int i = 1;
            while (i < 100)
            {
                _logger.LogInformation($"ServiceN2: " + i);
                i++;
                Thread.Sleep(1000);
            }
            _logger.LogInformation($"ServiceN2: Done!!!!!!!!!!!");
            return true;
        }

       
    }
}
