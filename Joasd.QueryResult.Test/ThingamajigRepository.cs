using Joasd.QueryResult.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joasd.QueryResult.Test
{
    public class ThingamajigRepository
    {
        public QueryResult<Thingamajig> Get()
        {
            return QueryResult<Thingamajig>.Create(new Thingamajig
            {
                Name = "Doowikie"
            });
        }
    }
}
