using StratApiX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratApiX.Domain.Interfaces
{
    public interface IHttpCommand
    {
        Task Execute(TestCase testCase);
    }
}
