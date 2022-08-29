using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Messages; 

public class MessagesService : IMessagesService {
    private readonly ForumDbContext dbContext;

    public MessagesService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Message>> GetLastMessages(int messageCount) {
        return dbContext.Messages
            .OrderByDescending(m => m.SendDate)
            .Take(messageCount)
            .OrderBy(m => m.SendDate)
            .Include(m => m.Author);
    }

    public async Task<Message> SendAsync(string userId, string text) {
        Message message = new() {
            AuthorId = userId,
            Text = text,
            SendDate = DateTime.Now,
            IsDeleted = false,
        };
        await dbContext.Messages.AddAsync(message);
        await dbContext.SaveChangesAsync();

        return message;
    }

    public async Task DeleteAsync(int id) {
        Message? message = await dbContext.Messages.FindAsync(id);
        if (message == null) {
            return;
        }
        message.IsDeleted = true;
        dbContext.Update(message);
        await dbContext.SaveChangesAsync();
    }
}