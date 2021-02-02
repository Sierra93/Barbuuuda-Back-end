using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.Logger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Logger
{
    /// <summary>
    /// Класс описывает логгер приложения.
    /// </summary>
    public class Logger
    {
        ApplicationDbContext _db;
        LoggerEntity _logger;

        public Logger(ApplicationDbContext db, string typeException, string exception, string stackTrace)
        {
            _db = db;
            _logger = new LoggerEntity()
            {
                TypeException = typeException,
                Exception = exception,
                StackTrace = stackTrace,
                Date = DateTime.Now
            };
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Critical.
        /// </summary>
        public async Task LogCritical()
        {
            _logger.LogLvl = LogLvl.CRITICAL;
            await _db.Logs.AddAsync(_logger);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Information.
        /// </summary>
        public async Task LogInformation()
        {
            _logger.LogLvl = LogLvl.INFORMATION;
            await _db.Logs.AddAsync(_logger);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Error.
        /// </summary>
        public async Task LogError()
        {
            _logger.LogLvl = LogLvl.ERROR;
            await _db.Logs.AddAsync(_logger);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Метод пишет лог в базу с типом Warning.
        /// </summary>
        public async Task LogWarning()
        {
            _logger.LogLvl = LogLvl.WARNING;
            await _db.Logs.AddAsync(_logger);
            await _db.SaveChangesAsync();
        }
    }
}
