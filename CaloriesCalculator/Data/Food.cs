using CaloriesCalculator.Data;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Calories { get; set; }


    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
