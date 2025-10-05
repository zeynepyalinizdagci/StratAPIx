using Microsoft.Extensions.Logging;
using StratApiX.Domain.Entities;
using StratApiX.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratApiX.Services.Services
{
    internal class TestExecutor : ITestExecutor
    {
        private readonly IHttpCommandFactory _commandFactory;
        private readonly ILogger<TestExecutor> _logger;
        public TestExecutor(IHttpCommandFactory commandFactory, ILogger<TestExecutor> logger) { 
            _commandFactory = commandFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<TestCaseResult>> Run(TestSpec testSpec, CancellationToken cancellationToken)
        {
            var bag = new ConcurrentBag<TestCaseResult>();

            await Parallel.ForEachAsync(testSpec.TestCases, new ParallelOptions { MaxDegreeOfParallelism = 8, CancellationToken = cancellationToken },
                async (testCase, token) =>
                {
                    try
                    {
                        _logger.LogInformation("TestCase {name} is running", testCase.Name);
                        var command = _commandFactory.Create(testCase.Method);
                        var result = await command.Execute(testCase, token);
                        _logger.LogInformation("TestCase {name} is completed", testCase.Name);

                        bag.Add(result);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception occured {exception}", ex);                        
                    }
                });

            return bag.AsEnumerable();
        }
    }

    internal interface ITestExecutor
    {
        Task<IEnumerable<TestCaseResult>> Run(TestSpec testSpec, CancellationToken cancellationToken);
    }
}
