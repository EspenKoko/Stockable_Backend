using Stockable_Backend.Model;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IChatBotInteractionRepository
    {
        //BotInteraction
        Task<ChatBotInteraction> GetChatBotInteractionAsync(int ChatBotInteractionId);
        Task<ChatBotInteraction[]> GetAllChatBotInteractionsAsync();
        Task<int> AddChatBotInteractionAsync(ChatBotInteractionViewModal ChatBotInteraction);
        Task<int> UpdateChatBotInteractionAsync(int BotInteractionId, ChatBotInteractionViewModal ChatBotInteraction);
        Task<int> DeleteChatBotInteractionAsync(int BotInteractionId);
        //Task<List<ChatBotInteraction>> SearchBotInteractionAsync(string searchString);
    }
}
