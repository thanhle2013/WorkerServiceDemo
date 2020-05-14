using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceDemo.Service
{
    public class ServiceN1
    {
        ILogger<ServiceN1> _logger;
        public ServiceN1(ILogger<ServiceN1> logger)
        {
            _logger = logger;
        }

        public bool DoWork()
        {
            //_logger.LogInformation($"Service starts. {DateTime.Now.ToString("F")}");
            Debug.WriteLine($"Debug start 111");
            return true;
        }

       
    }
}
