using Data;
using Data.Models;

namespace Services.ReportTypes; 

public class ReportTypesService : IReportTypesService {
    private readonly ForumDbContext dbContext;

    public ReportTypesService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string name) {
        ReportType reportType = new() {
            Name = name
        };

        await dbContext.AddAsync(reportType);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ReportType> GetAsync(int id) {
        return await dbContext.ReportTypes.FindAsync(id);
    }

    public async Task<IEnumerable<ReportType>> GetAllAsync() {
        return dbContext.ReportTypes;
    }

    public async Task UpdateAsync(int id, string name) {
        ReportType reportType = await dbContext.ReportTypes.FindAsync(id);
        if (reportType == null) {
            return;
        }

        reportType.Name = name;
        dbContext.Update(reportType);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        ReportType reportType = await dbContext.ReportTypes.FindAsync(id);
        if(reportType == null) {
            return;
        }

        dbContext.Remove(reportType);
        await dbContext.SaveChangesAsync();
    }
}