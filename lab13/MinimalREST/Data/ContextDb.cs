using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class ContextDb : DbContext
    {
        public ContextDb (DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<Measurement> Measurement { get; set; } = default!;
    }
