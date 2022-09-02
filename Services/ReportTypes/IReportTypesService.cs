using Data.Models;

namespace Services.ReportTypes; 

public interface IReportTypesService {
    Task AddAsync(string name);

    Task<ReportType> GetAsync(int id);

    Task<IEnumerable<ReportType>> GetAllAsync();

    Task UpdateAsync(int id, string name);

    Task DeleteAsync(int id);
}