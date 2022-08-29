using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Messages;
using Xunit;

namespace Tests; 

public class MessagesServiceTests {
    private readonly ForumDbContext dbContext;
    private readonly MessagesService messagesService;
    
    public MessagesServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        dbContext = new ForumDbContext(options);

        messagesService = new MessagesService(dbContext);
    }

    [Fact]
    public async Task Send_Message_MessageIsSeenInDatabase() {
        await messagesService.SendAsync("1", "Sample text");

        Message message = await dbContext.Messages.FirstAsync();

        Assert.Single(dbContext.Messages);
        Assert.Equal("1", message.AuthorId);
        Assert.Equal("Sample text", message.Text);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 3)]
    public async Task Get_Messages_ReturnsSpecifiedCountOfMessages(int wantedMessageCount, int expectedCount) {
        List<Message> messageList = new();

        for (int i = 0; i < expectedCount; i++) {
            messageList.Add(new Message {
                AuthorId = "1",
                SendDate = DateTime.Now,
                Text = i.ToString(),
            });
        }

        await dbContext.AddRangeAsync(messageList);
        await dbContext.SaveChangesAsync();

        IEnumerable<Message> messages = await messagesService.GetLastMessages(wantedMessageCount);

        Assert.Equal(expectedCount, messages.Count());
    }

    [Fact]
    public async Task Remove_Message_MessageIsMarkedAsDeleted() {
        await messagesService.SendAsync("1", "Sample text");

        Message message = await dbContext.Messages.FirstAsync();

        Assert.False(message.IsDeleted);

        await messagesService.DeleteAsync(message.Id);

        Assert.True(message.IsDeleted);
    }
}