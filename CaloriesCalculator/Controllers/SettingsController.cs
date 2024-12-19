using CaloriesCalculator.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class SettingsController : Controller
{
    private readonly ILogger<SettingsController> _logger;
    private readonly ApplicationDbContext _context;

    public SettingsController(ILogger<SettingsController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateGoal(int weeklyGoal)
    {
        if (weeklyGoal <= 0)
        {
            ModelState.AddModelError("", "Целта трябва да е положително число.");
            return RedirectToAction("Progres", "User");
        }

        var userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Account");
        }

        var userSettings = await _context.UserSettings
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (userSettings != null)
        {
            userSettings.WeeklyTargetCalories = weeklyGoal;
        }
        else
        {
            userSettings = new UserSettings
            {
                UserId = userId,
                WeeklyTargetCalories = weeklyGoal,
                TargetCalories = 2000
            };

            _context.UserSettings.Add(userSettings);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Progres", "User");
    }

    [HttpPost]
    public async Task<IActionResult> AddCalories(DateTime date, int calories)
    {
        var userId = GetUserId();

        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Account");
        }

        if (calories <= 0)
        {
            ModelState.AddModelError("", "Калориите трябва да бъдат положителни.");
            return RedirectToAction("Progres", "User");
        }

        var entry = await _context.ProgressEntries
            .FirstOrDefaultAsync(p => p.Date == date && p.UserId == userId);

        if (entry == null)
        {
            entry = new ProgressEntry
            {
                UserId = userId,
                Date = date,
                Calories = calories
            };
            _context.ProgressEntries.Add(entry);
        }
        else
        {
            entry.Calories += calories;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Progres", "User");
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
