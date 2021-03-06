﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace LoggingAdvanced.Console
{
    public static class LoggerBuilderExtensions
    {
        /// <summary>Adds a console logger named 'ConsoleAdvanced' to the factory.</summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.</param>
        /// <param name="loggingSection"></param>
        public static ILoggingBuilder AddConsoleAdvanced(this ILoggingBuilder builder, 
            IConfiguration loggingSection)
        {
            if (loggingSection == null)
                throw new ArgumentNullException(nameof(loggingSection));

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider,
                    AdvancedConsoleLoggerProvider>());
                    
            //We can't read the result of logging.AddConfiguration mtd here, cause it registers internal classes (without public interfaces).
            //So, a configuration section must be passed to configure the logger.
            builder.Services.TryAdd(
                ServiceDescriptor.Singleton<IConsoleLoggerSettings>(
                    provider => new ConfigurationConsoleLoggerSettings(loggingSection)));

            return builder;
        }

        /// <summary>Adds a console logger named 'ConsoleAdvanced' to the factory.</summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.</param>
        public static ILoggingBuilder AddConsoleAdvanced(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, AdvancedConsoleLoggerProvider>());

            builder.Services.TryAdd(
                ServiceDescriptor.Singleton<IConsoleLoggerSettings>(provider => ConsoleLoggerSettings.Optimized));

            return builder;
        }

        /// <summary>Adds a console logger named 'ConsoleAdvanced' to the factory.</summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.</param>
        /// <param name="settings">The formatting settings, timezone, etc.</param>
        public static ILoggingBuilder AddConsoleAdvanced(this ILoggingBuilder builder,
            IConsoleLoggerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, AdvancedConsoleLoggerProvider>());

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton(provider => settings));

            return builder;
        }
    }
}