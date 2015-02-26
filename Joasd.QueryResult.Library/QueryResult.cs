using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joasd.QueryResult.Library
{
    /// <summary>
    /// Wraps a result from a query to make it explicit for the consumer
    /// </summary>
    /// <typeparam name="T">Type of the result</typeparam>
    public class QueryResult<T>
        where T : class
    {
        private readonly T result;
        private readonly bool hasResult;
        
        private QueryResult(T resultToUse) 
        {
            hasResult = resultToUse != null;
            result = resultToUse;
        }

        /// <summary>
        /// Creates a new <see cref="QueryResult"/> QueryResult for a specific result object
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="result">The result</param>
        /// <returns>Newly created QueryResult</returns>
        public static QueryResult<T> Create<T>(T result)
            where T : class
        {
            return new QueryResult<T>(result);
        }

        /// <summary>
        /// Determines if a result is present
        /// </summary>
        public bool HasResult
        {
            get { return hasResult; }
        }

        /// <summary>
        /// Retrieves the result could be null so check should be made against <see cref="HasResult"/>
        /// </summary>
        public T Result
        {
            get { return result; }
        }

        /// <summary>
        /// Executes a supplied action if the result is present
        /// </summary>
        /// <param name="action">action to execute</param>
        public void Found(Action<T> action)
        {
            if (hasResult)
            {
                action(result);
            }
        }

        /// <summary>
        /// Executes a supplied action id the result is not present
        /// </summary>
        /// <param name="action">action to execute</param>
        public void Missing(Action action)
        {
            if (!hasResult)
            {
                action();
            }
        }
    }
}
