using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndoscopicSystem.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly int MaxRetries = 3;
        private static readonly TimeSpan RetryInterval = TimeSpan.FromSeconds(2);

        public static void RetryAction(Action action)
        {
            var retryCount = 0;
            while (true)
            {
                try
                {
                    action();
                    break;
                }
                catch (DbException ex) when (ex is SqlException || ex is EntityException)
                {
                    if (++retryCount >= MaxRetries)
                    {
                        throw;
                    }
                    Console.WriteLine("Retrying after {0} seconds...", RetryInterval.TotalSeconds);
                    System.Threading.Thread.Sleep(RetryInterval);
                }
            }
        }
    }
}
