namespace Nsu.Contest.Entity;

public class Wishlist
{
    public Wishlist() { DesiredEmployees = []; }
    internal Wishlist(Employee forEmployee, ICollection<int> desiredEmployees)
    {
        ForEmployee = forEmployee;
        DesiredEmployees = desiredEmployees;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Employee ForEmployee { get; set; }
    public int ForEmployeeId { get; set; }
    public ICollection<int> DesiredEmployees { get; set; }
};
