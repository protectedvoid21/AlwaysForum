using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Seeding; 

public interface ISeeder {
    Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider);
}