using FinalProj.DAL.Repository.Interfaces;
using FinalProj.Domain.Models.Entities.Logs;
using System.Diagnostics.CodeAnalysis;

namespace FinalProj.API.Logs
{
    public class DbLogger : ILogger
    {
        /// <summary>  
        /// Instance of <see cref="DbLoggerProvider" />.  
        /// </summary>  
        private readonly DbLoggerProvider _dbLoggerProvider;
        private readonly IServiceScopeFactory _scopeFactory;
        /// <summary>  
        /// Creates a new instance of <see cref="FileLogger" />.  
        /// </summary>  
        /// <param name="fileLoggerProvider">Instance of <see cref="FileLoggerProvider" />.</param>  
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider, IServiceScopeFactory scopeFactory)
        {
            _dbLoggerProvider = dbLoggerProvider;
            _scopeFactory = scopeFactory;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>  
        /// Whether to log the entry.  
        /// </summary>  
        /// <param name="logLevel"></param>  
        /// <returns></returns>  
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>  
        /// Used to log the entry.  
        /// </summary>  
        /// <typeparam name="TState"></typeparam>  
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>  
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>  
        /// <param name="state">The event's state.</param>  
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>  
        /// <param name="formatter">A delegate that formats </param>  
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.  
                return;
            }

            using var serviceScope = _scopeFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;
            // resolve scoped service, for example db context: 
            var logRepository = provider.GetRequiredService<IBaseAsyncRepository<Log>>();

            var threadId = Thread.CurrentThread.ManagedThreadId; // Get the current thread ID to use in the log file.   

            Log log = new Log()
            {
                LogLevel = logLevel.ToString(),
                ThreadId = threadId,
                EventId = eventId.Id,
                EventName = eventId.Name,
                Message = formatter(state, exception),
                ExceptionMessage = exception?.Message,
                ExceptionSource = exception?.Source,
                ExceptionStackTrace = exception?.StackTrace,
            };

            logRepository.Create(log);
        }
    }
}