using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ServerJava
{
    internal class Connector: DbContext
    {
        public Connector()
            : base("Cars")
        {}
        public DbSet<Cars> Cars { get; set; }
    }
}
