using System;
using System.Collections.Generic;

namespace Pets.Domain.Authentication
{
    /// <summary>
    /// Права доступа к API
    /// </summary>
    public record ScopeAction
    {
        /// <summary>
        /// Шаблоны URL на чтение данных
        /// </summary>
        public ICollection<String> ReadRequests { get; set; } = new List<String>();

        /// <summary>
        /// Шаблоны URL на изменение данных
        /// </summary>
        public ICollection<String> WriteRequests { get; set; } = new List<String>();
    }
}