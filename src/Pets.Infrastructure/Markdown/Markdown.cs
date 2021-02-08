using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MarkdownSharp;

using Microsoft.Extensions.Caching.Memory;

namespace Pets.Infrastructure.Markdown
{
    public sealed class Markdown : IMarkdown
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Object _locker = new();

        private readonly Regex _instagramRegex = new(@"(!inst\([a-z/_0-9.:]+\))",
            RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

        private readonly Regex _linkRegex = new(@"(!:(http(s?):\/\/[a-z./_&=\-+а-я#?]+):([#!?.,а-яa-z\- \w]+):!)",
            RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

        public Markdown(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<String> Parse(String? md)
        {
            if (md is null)
                return "";

            var key = $"MD_{md.GetHashCode()}";
            // ReSharper disable once InconsistentlySynchronizedField
            if (_memoryCache.TryGetValue(key, out String? html))
                return html;

            lock (_locker)
            {
                var mdProcessor = new MarkdownSharp.Markdown(new MarkdownOptions { });
                var prep = _instagramRegex.Replace(md, "<br>instagram integration block<br>");
                prep = _linkRegex.Replace(prep, "[$4]($2)");
                var result = mdProcessor.Transform(prep);
                _memoryCache.Set(key, result, TimeSpan.FromHours(2));
                return result;
            }
        }
    }
}