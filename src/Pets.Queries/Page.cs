using System;
using System.Collections.Generic;

namespace Pets.Queries
{
    public sealed class Page<T> where T : class
    {
        public Int64 Total { get; set; }

        public Int32 Offset { get; set; }

        public Int32 Limit { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}