using Data.Models;

namespace Services.Messages; 

public interface IMessagesService {
    Task<IEnumerable<Message>> GetLastMessages(int messageCount);

    Task<Message> SendAsync(string userId, string text);

    Task DeleteAsync(int id);
}