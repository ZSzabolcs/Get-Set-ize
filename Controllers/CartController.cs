namespace proba1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proba1.Data;
using proba1.DTOs;
using proba1.Models;

[Route("api/[controller]")]
[ApiController]
    public class CartController: ControllerBase
    {
    private readonly AppDbContext _context;
    public CartController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
    {
        var result = await _context.CartItems
            .Include(ci => ci.Customer)
            .Include (ci => ci.Product)
            .Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                CustomerName = ci.Customer.Name, 
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity,
            })
            .ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemDto>> GetCartItem(int id)
    {
        var cartItem = await _context.CartItems
            .Include(ci => ci.Customer)
            .Include(ci => ci.Product)
            .Where(ci => ci.Id == id)
            .Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                CustomerName = ci.Customer.Name,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity,
            })
            .FirstOrDefaultAsync();

        if (cartItem == null)
        {
            return NotFound();
        }

        return Ok(cartItem);
    }

    [HttpPost]
    public async Task<ActionResult<CartItemDto>> PostCartItem(AddCartItemDto cartDto)
    {
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CustomerId == cartDto.CustomerId && ci.ProductId == cartDto.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += cartDto.Quantity;
        }
        else
        {
            var newItem = new CartItem
            {
                CustomerId = cartDto.CustomerId,
                ProductId = cartDto.ProductId,
                Quantity = cartDto.Quantity
            };
            _context.CartItems.Add(newItem);
        }

        await _context.SaveChangesAsync();

        var itemIdToReturn = existingItem?.Id ?? _context.CartItems.Local.Last().Id;
        
        var updatedItem = await _context.CartItems
            .Include(ci => ci.Customer)
            .Include(ci => ci.Product)
            .Where(ci => ci.Id == itemIdToReturn)
            .Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                CustomerName = ci.Customer.Name,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            })
            .FirstOrDefaultAsync();

        if (updatedItem == null) return NotFound();

        return CreatedAtAction(nameof(GetCartItem), new { id = updatedItem.CartItemId }, updatedItem);
    }
}