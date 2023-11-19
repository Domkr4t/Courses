using Site.Backend.DAL.Interfaces;
using Site.Domain.Course.Entity;

namespace Site.Backend.DAL.Repositories
{
    public class CourseRepository : IBaseRepository<CourseEntity>
    {
        private readonly AppDbContext _appDbContext;

        public CourseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(CourseEntity entity)
        {
            await _appDbContext.Courses.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(CourseEntity entity)
        {
            _appDbContext.Courses.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<CourseEntity> GetAll()
        {
            return _appDbContext.Courses;
        }

        public async Task<CourseEntity> Update(CourseEntity entity)
        {
            _appDbContext.Courses.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Attach(CourseEntity entity)
        {
            _appDbContext.Courses.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
