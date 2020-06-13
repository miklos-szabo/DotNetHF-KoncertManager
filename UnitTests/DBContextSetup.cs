using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.DAL;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    /**
     * ContextOptions egységes kiadására, ebből leszármazva, base konstruktort hívva lehet
     * többféle adatbázisból is használni.
     */
    public abstract class DBContextSetup
    {
        public DbContextOptions<ConcertManagerContext> ContextOptions { get; set; }

        protected DBContextSetup(DbContextOptions<ConcertManagerContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

    }
}
