using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;
        private readonly IMapper _mapper;
        public ProductController(IProductServices productServices, IMapper mapper)
        {
            _services = productServices;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult> GetProductByIdAsync(int id)
        {
            var product = await _services.GetProductByIdAsync(id);
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult> GetProductsAsync([FromQuery] PaginationParams paginationParams)
        {
            var products = await _services.GetProductsAsync(paginationParams.Page, paginationParams.PageSize);
            var result = _mapper.Map<List<ProductDTO>>(products);
            return Ok(result);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var created = await _services.CreateProductAsync(_mapper.Map<Product>(createProductDTO));

            if (!created.Success)
            {
                return BadRequest(new
                {
                    Message = created.Message
                });
            }

            var result = _mapper.Map<ProductDTO>(created.Data);
            
            return CreatedAtAction("GetProductById", new { Id = result.Id }, result);
        }

        [HttpPost("buy")]
        public async Task<ActionResult> BuyProductAsync(string name, int quantity)
        {
            var saved = await _services.BuyProductAsync(name, quantity);

            if (!saved.Success)
            {
                return BadRequest(new 
                { 
                    Message = saved.Message 
                });
            }

            return Ok(new
            { 
                Message = saved.Message ,
                Data = _mapper.Map<ProductDTO>(saved.Data)
            });
        }

        [HttpGet("searchByCategory/{categoryId:int}", Name = "GetProductsForCategory")]
        public async Task<ActionResult> GetProductsForCategoryAsync(int categoryId)
        {
            var saved = await _services.GetProductsForCategoryAsync(categoryId);

            if (!saved.Success)
            {
                return BadRequest(new 
                { 
                    Message = saved.Message 
                });
            }

            return Ok(new
            { 
                Data = _mapper.Map<List<ProductDTO>>(saved.Data)
            });
        }
    }
}
