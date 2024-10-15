using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.HazemFady.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository.Identity.Contexts
{
    public class StoreIdentityDbContext:IdentityDbContext<APPUser>
    {

        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options):base(options) 
        {
            
        }
    }
}
