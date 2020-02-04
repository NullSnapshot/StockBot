using System;
using System.Threading.Tasks;

using Logger;
using Logger.Service;

namespace Logger.Adapter.Handler
{
    /// <summary>
    ///     Standard interface used for all log handlers that interface with <c>LoggerService</c>
    /// </summary>
    interface ILogHandler<T>
    {
        /// <summary>
        ///     adapts Log types that are subscribed to this task.
        /// </summary>
        Task Log(T message);

        /// <summary>
        ///     adapted logs are pushed by this event, which should be subscribed to by <c>LoggerService</c>
        /// </summary>
        event Func<IStandardLog, Task> PushLog;
    }
}