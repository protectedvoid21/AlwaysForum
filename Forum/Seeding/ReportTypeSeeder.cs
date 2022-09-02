using Data;
using Services.ReportTypes;

namespace AlwaysForum.Seeding; 

public class ReportTypeSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        var reportTypesService = serviceProvider.GetRequiredService<IReportTypesService>();

        string[] existingReportTypeNames = dbContext.ReportTypes.Select(reportType => reportType.Name).ToArray();
        IEnumerable<string> missingReportTypes = GlobalConstants.RequiredReportTypes.Except(existingReportTypeNames);

        foreach (string reportTypeName in missingReportTypes) {
            await reportTypesService.AddAsync(reportTypeName);
        }
    }
}