using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Sections;
using Xunit;

namespace Tests;

public class SectionsServiceTests {
    private readonly SectionsService sectionsService;
    private readonly ForumDbContext dbContext;

    public SectionsServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        dbContext = new ForumDbContext(options);

        sectionsService = new SectionsService(dbContext);
    }

    [Theory]
    [InlineData("SampleSection")]
    public async Task Add_NewSection_ShouldBeAddedInDatabase(string name) {
        await sectionsService.AddAsync(name);

        Assert.Single(dbContext.Sections);
    }

    [Fact]
    public async Task Get_AllSections_ReturnsAllSectionsList() {
        await dbContext.Sections.AddRangeAsync(
            new Section { Name = "Section1" },
            new Section { Name = "Section2" },
            new Section { Name = "Section3" });

        await dbContext.SaveChangesAsync();
        var sectionList = await sectionsService.GetAll();

        Assert.Equal(3, sectionList.Count());
    }

    [Theory]
    [InlineData("FirstName", "UpdatedName")]
    public async Task Update_CertainSection_DataIsChangedForSection(string previousName, string updateName) {
        await sectionsService.AddAsync(previousName);
        var section = await dbContext.Sections.FirstAsync();

        await sectionsService.UpdateAsync(section.Id, updateName);

        Assert.Equal(updateName, section.Name);
    }

    [Fact]
    public async Task Delete_Section_DatabaseDeletesSection() {
        await sectionsService.AddAsync("SectionName");
        Assert.Single(dbContext.Sections);

        var section = await dbContext.Sections.FirstAsync();
        await sectionsService.DeleteAsync(section.Id);

        Assert.Empty(dbContext.Sections);
    }
}