using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyChat.Models;

namespace MyChat.Controllers;

public class ChatController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly MyChatContext _context;
    private readonly ILogger<ChatController> _logger;

    public ChatController(UserManager<User> userManager, MyChatContext context, ILogger<ChatController> logger)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var topics = await _context.ForumTopics
            .Include(t => t.User)
            .OrderByDescending(t => t.CreatedAt) 
            .ToListAsync();

        return View(topics); 
    }
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ForumTopic topic)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                topic.CreatedAt = DateTime.UtcNow; 
                topic.UserId = user.Id;

                _context.Add(topic);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                return Unauthorized(); 
            }
        }

        return View(topic); 
    }

    [Route("Chat/Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var topic = await _context.ForumTopics
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
        {
            _logger.LogWarning($"Topic with ID {id} not found.");
            return NotFound();
        }

        return View(topic);
    }



}
