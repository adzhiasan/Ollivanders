namespace Ollivanders;

public class Mage
{
    public Mage(string name, DateOnly dateOfBirth)
    {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Date of birth cannot be in the future", nameof(dateOfBirth));

        Name = name;
        DateOfBirth = dateOfBirth;
    }
    
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public DateOnly DateOfBirth { get; init; }
}
