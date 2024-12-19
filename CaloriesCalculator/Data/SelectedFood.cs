public class SelectedFood
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double CaloriesPer100g { get; set; }
    public double Quantity { get; set; }
    public double TotalCalories { get; set; }

    public Food Food { get; set; }
    public int FoodId { get;  set; }
}