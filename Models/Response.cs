using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models
{
    public class Response<T>
    {
        public Response(IEnumerable<T> results = null)
        {
            Results = results ?? Array.Empty<T>();
        }

        public IEnumerable<T> Results { get; set; }
    }
}
