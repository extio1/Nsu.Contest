namespace Nsu.Contest.Entity;

public class Wishlist(Employee forEmployee, Employee[] desiredEmployees)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Employee ForEmployee { get; set; } = forEmployee;
    public Employee[] DesiredEmployees { get; set; } = desiredEmployees;
};
