//using Microsoft.AspNetCore.Mvc;
//using Stockable_Backend.Model;
//using Stockable_Backend.Repository.IRepositories;
//using Stockable_Backend.ViewModel;

//namespace Stockable_Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PartsRequestController : ControllerBase
//    {
//        private readonly IPartsRequestRepository _repository;

//        public PartsRequestController(IPartsRequestRepository repository)
//        {
//            _repository = repository;
//        }

//        //Create partsRequest
//        [HttpPost]
//        [Route("AddPartsRequest")]
//        public async Task<IActionResult> AddPartsRequest(PartsRequestViewModal partsRequest)
//        {
//            try
//            {
//                var result = await _repository.AddPartsRequestAsync(partsRequest);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to add Parts Request: Stock or purchase order was not found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status500InternalServerError);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Get all partsRequestes
//        [HttpGet]
//        [Route("GetAllPartsRequest")]
//        public async Task<IActionResult> GetAllPartsRequestes()
//        {
//            try
//            {
//                PartsRequest[] results = await _repository.GetAllPartsRequestAsync();
//                if (results.Length == 0)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, results);
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Get a partsRequest
//        [HttpGet]
//        [Route("GetPartsRequest/{purchaseOrderId}")]
//        public async Task<IActionResult> GetPartsRequest(int purchaseOrderId)
//        {
//            try
//            {
//                List<PartsRequest> result = await _repository.GetPartsRequestAsync(purchaseOrderId);

//                if (result == null)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status200OK, result);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        // Search partsRequest by printer serial number
//        [HttpGet]
//        [Route("SearchPartsRequest/{searchString}")]
//        public async Task<IActionResult> SearchPartsRequest(string searchString)
//        {
//            try
//            {
//                List<PartsRequest> results = await _repository.SearchPartsRequestAsync(searchString);
//                return Ok(results);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        //Update partsRequest
//        [HttpPut]
//        [Route("UpdatePartsRequest/{purchaseOrderId}")]
//        public async Task<IActionResult> UpdatePartsRequest(int purchaseOrderId, PartsRequestViewModal partsRequest)
//        {
//            try
//            {
//                var result = await _repository.UpdatePartsRequestAsync(purchaseOrderId, partsRequest);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK);
//                }
//                else if (result == 404)
//                {
//                    return StatusCode(StatusCodes.Status404NotFound, "Failed to update Parts Request. Parts Request not be found");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status501NotImplemented, "Failed to update Parts Request. The Stock or Purchase Order could not be found");
//                }

//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }


//        // Delete partsRequest
//        [HttpDelete]
//        [Route("DeletePartsRequest/{purchaseOrderId}")]
//        public async Task<IActionResult> DeletePartsRequest(int purchaseOrderId)
//        {
//            try
//            {
//                var result = await _repository.DeletePartsRequestAsync(purchaseOrderId);
//                if (result == 200)
//                {
//                    return StatusCode(StatusCodes.Status200OK, "Successfully deleted Parts Request");
//                }
//                else
//                {
//                    return StatusCode(StatusCodes.Status404NotFound);
//                }
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }
//}
