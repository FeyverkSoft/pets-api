using System;
using System.Threading.Tasks;

namespace Pets.Infrastructure.Markdown
{
    /// <summary>
    /// Интерфес для работы с Markdown, поддержвает кеширование 
    /// </summary>
    public interface IMarkdown
    {
        /// <summary>
        /// Распарсить md и вернуть как HTML
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        Task<String> Parse(String? md);
    }
}