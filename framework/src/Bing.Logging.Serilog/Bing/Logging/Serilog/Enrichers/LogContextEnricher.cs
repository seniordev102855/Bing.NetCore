﻿using Bing.Extensions;
using Serilog.Core;
using Serilog.Events;

namespace Bing.Logging.Serilog.Enrichers
{
    /// <summary>
    /// 日志上下文扩展属性
    /// </summary>
    public class LogContextEnricher : ILogEventEnricher
    {
        /// <summary>
        /// 日志上下文
        /// </summary>
        private LogContext _context;

        /// <summary>
        /// 日志上下文访问器
        /// </summary>
        private readonly ILogContextAccessor _logContextAccessor;

        /// <summary>
        /// 初始化一个 <see cref="LogContextEnricher"/>类型的实例
        /// </summary>
        /// <param name="logContextAccessor">日志上下文访问器</param>
        public LogContextEnricher(ILogContextAccessor logContextAccessor)
        {
            _logContextAccessor = logContextAccessor;
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        /// <param name="logEvent">日志事件</param>
        /// <param name="propertyFactory">日志事件属性工厂</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_logContextAccessor == null)
                return;
            _context = _logContextAccessor.Context;
            if (_context == null)
                return;
            AddDuration(logEvent, propertyFactory);
            AddTraceId(logEvent, propertyFactory);
            AddUserId(logEvent, propertyFactory);
            AddApplication(logEvent, propertyFactory);
            AddEnvironment(logEvent, propertyFactory);
            AddExtraData(logEvent, propertyFactory);
        }

        /// <summary>
        /// 移除默认设置的部分属性
        /// </summary>
        private void RemoveProperties(LogEvent logEvent)
        {
            logEvent.RemovePropertyIfPresent("ActionId");
            logEvent.RemovePropertyIfPresent("ActionName");
            logEvent.RemovePropertyIfPresent("RequestId");
            logEvent.RemovePropertyIfPresent("RequestPath");
            logEvent.RemovePropertyIfPresent("ConnectionId");
        }

        /// <summary>
        /// 添加执行持续时间
        /// </summary>
        private void AddDuration(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_context.Stopwatch == null)
                return;
            var property = propertyFactory.CreateProperty("Duration", _context.Stopwatch.Elapsed.Description());
            logEvent.AddOrUpdateProperty(property);
        }

        /// <summary>
        /// 添加跟踪号
        /// </summary>
        private void AddTraceId(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_context.TraceId))
                return;
            var property = propertyFactory.CreateProperty("TraceId", _context.TraceId);
            logEvent.AddOrUpdateProperty(property);
        }

        /// <summary>
        /// 添加用户标识
        /// </summary>
        private void AddUserId(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_context.UserId))
                return;
            var property = propertyFactory.CreateProperty("UserId", _context.UserId);
            logEvent.AddOrUpdateProperty(property);
        }

        /// <summary>
        /// 添加应用程序
        /// </summary>
        private void AddApplication(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_context.Application))
                return;
            var property = propertyFactory.CreateProperty("Application", _context.Application);
            logEvent.AddOrUpdateProperty(property);
        }

        /// <summary>
        /// 添加执行环境
        /// </summary>
        private void AddEnvironment(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_context.Environment))
                return;
            var property = propertyFactory.CreateProperty("Environment", _context.Environment);
            logEvent.AddOrUpdateProperty(property);
        }

        /// <summary>
        /// 添加扩展数据
        /// </summary>
        private void AddExtraData(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_context.Data.Count == 0)
                return;
            foreach (var item in _context.Data)
            {
                var property = propertyFactory.CreateProperty(item.Key, item.Value);
                logEvent.AddOrUpdateProperty(property);
            }
        }
    }
}
