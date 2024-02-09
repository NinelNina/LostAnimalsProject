namespace LostAnimals.Services.Logger
{
    using Serilog.Events;
    using System;

    public class AppLogger : IAppLogger
    {
        private readonly Serilog.ILogger logger;

        private string _systemName = "LostAnimals";

        public AppLogger(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        private string ConstructMessageTemplate(string messageTemplate, object module = null)
        {
            var moduleName = module?.GetType().Name;

            if (string.IsNullOrEmpty(moduleName))
            {
                return $"[{_systemName}] {messageTemplate}";
            }
            else
            {
                return $"[{_systemName}:{module}] {messageTemplate}";
            }
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            logger?.Debug(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Debug(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Debug(ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            logger?.Error(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Error(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Error(ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger?.Error(exception, ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Error(Exception exception, object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Error(exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            logger?.Fatal(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Fatal(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Fatal(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger?.Fatal(exception, ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Fatal(Exception exception, object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Fatal(exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            logger?.Information(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Information(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Information(ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            logger?.Verbose(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Verbose(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Verbose(ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            logger?.Warning(ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Warning(object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Warning(ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            logger?.Write(level, ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Write(LogEventLevel level, object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Write(level, ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger?.Write(level, exception, ConstructMessageTemplate(messageTemplate), propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, object module, string messageTemplate, params object[] propertyValues)
        {
            logger?.Write(level, exception, ConstructMessageTemplate(messageTemplate, module), propertyValues);
        }
    }
}
