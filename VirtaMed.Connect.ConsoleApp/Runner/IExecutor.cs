using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VirtaMed.Connect.ConsoleApp.Runner
{
    public interface IExecutor
    {
        Task Execute();
    }
}
