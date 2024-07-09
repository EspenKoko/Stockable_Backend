using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Repository
{
    public class ChatBotInteractionRepository : IChatBotInteractionRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChatBotInteractionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ChatBotInteraction
        //Get all ChatBotInteractions
        public async Task<ChatBotInteraction[]> GetAllChatBotInteractionsAsync()
        {
            IQueryable<ChatBotInteraction> query = _appDbContext.ChatBotInteractions;
            return await query.ToArrayAsync();
        }

        //Get ChatBotInteraction
        public async Task<ChatBotInteraction> GetChatBotInteractionAsync(int ChatBotInteractionId)
        {
            ChatBotInteraction ChatBotInteraction = await _appDbContext.ChatBotInteractions
                .FirstOrDefaultAsync(x => x.botInteractionId == ChatBotInteractionId);

            return ChatBotInteraction;
        }

        // Create ChatBotInteraction
        public async Task<int> AddChatBotInteractionAsync(ChatBotInteractionViewModal ChatBotInteraction)
        {
            try
            {
                ChatBotInteraction ChatBotInteractionAdd = new ChatBotInteraction
                {
                    type = ChatBotInteraction.type,
                    message = ChatBotInteraction.message,
                    date = ChatBotInteraction.date,
                };

                await _appDbContext.ChatBotInteractions.AddAsync(ChatBotInteractionAdd);
                await _appDbContext.SaveChangesAsync();

                return 200;

            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Update ChatBotInteraction
        public async Task<int> UpdateChatBotInteractionAsync(int ChatBotInteractionsId, ChatBotInteractionViewModal ChatBotInteraction)
        {
            try
            {
                ChatBotInteraction existingChatBotInteraction = await _appDbContext.ChatBotInteractions.FindAsync(ChatBotInteractionsId);
                if (existingChatBotInteraction == null)
                {
                    return 404;
                }

                existingChatBotInteraction.type = ChatBotInteraction.type;
                existingChatBotInteraction.message = ChatBotInteraction.message;
                existingChatBotInteraction.date = ChatBotInteraction.date;

                await _appDbContext.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        // Delete ChatBotInteraction
        public async Task<int> DeleteChatBotInteractionAsync(int ChatBotInteractionsId)
        {
            ChatBotInteraction attemptToFindInDb = await _appDbContext.ChatBotInteractions.FindAsync(ChatBotInteractionsId);
            if (attemptToFindInDb == null)
            {
                return 404;
            }

            _appDbContext.ChatBotInteractions.Remove(attemptToFindInDb);
            await _appDbContext.SaveChangesAsync();

            return 200;
        }

        //// Search ChatBotInteraction
        //public async Task<List<ChatBotInteraction>> SearchChatBotInteractionAsync(string searchString)
        //{
        //    List<ChatBotInteraction> ChatBotInteractions = await _appDbContext.ChatBotInteractions
        //        .Where(x => x.invoiceNumber.Contains(searchString)).ToListAsync();

        //    return ChatBotInteractions;
        //}
    }
}
