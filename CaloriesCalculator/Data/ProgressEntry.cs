using Microsoft.AspNetCore.Identity;

namespace CaloriesCalculator.Data
{
    public class ProgressEntry
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Calories { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
