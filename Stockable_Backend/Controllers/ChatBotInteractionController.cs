using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Repository.IRepositories;
using Stockable_Backend.ViewModel;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotInteractionController : ControllerBase
    {
        private readonly IChatBotInteractionRepository _repository;

        public ChatBotInteractionController(IChatBotInteractionRepository repository)
        {
            _repository = repository;
        }

        //Create ChatBotInteraction
        [HttpPost]
        [Route("AddChatBotInteraction")]
        public async Task<IActionResult> AddChatBotInteraction(ChatBotInteractionViewModal ChatBotInteraction)
        {
            try
            {
                var result = await _repository.AddChatBotInteractionAsync(ChatBotInteraction);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Added ChatBotInteraction");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured, please try again");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Get all ChatBotInteractions
        [HttpGet]
        [Route("GetAllChatBotInteraction")]
        public async Task<IActionResult> GetAllChatBotInteraction()
        {
            try
            {
                var results = await _repository.GetAllChatBotInteractionsAsync();
                if (results.Length == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Get a ChatBotInteraction
        [HttpGet]
        [Route("GetChatBotInteraction/{ChatBotInteractionId}")]
        public async Task<IActionResult> GetChatBotInteraction(int ChatBotInteractionId)
        {
            try
            {
                var result = await _repository.GetChatBotInteractionAsync(ChatBotInteractionId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }

        //Update ChatBotInteraction
        [HttpPut]
        [Route("UpdateChatBotInteraction/{ChatBotInteractionId}")]
        public async Task<IActionResult> UpdateChatBotInteraction(int ChatBotInteractionId, ChatBotInteractionViewModal ChatBotInteraction)
        {
            try
            {
                var result = await _repository.UpdateChatBotInteractionAsync(ChatBotInteractionId, ChatBotInteraction);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully updated ChatBotInteraction");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update ChatBotInteraction. ChatBotInteraction not be found");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }


        // Delete ChatBotInteraction
        [HttpDelete]
        [Route("DeleteChatBotInteraction/{ChatBotInteractionId}")]
        public async Task<IActionResult> DeleteChatBotInteraction(int ChatBotInteractionId)
        {

            try
            {
                var result = await _repository.DeleteChatBotInteractionAsync(ChatBotInteractionId);
                if (result == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted ChatBotInteraction");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured on the server, please try again later");
            }
        }
    }
}
