using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Blogpost> CreateAsync(Blogpost blogpost)
        {
            await dbContext.Blogposts.AddAsync(blogpost);
            await dbContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<IEnumerable<Blogpost>> GetAllAsync()
        {
            var response = await dbContext.Blogposts.ToListAsync();
            return response;
        }
    }
}
