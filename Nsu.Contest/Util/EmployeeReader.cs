namespace Nsu.Contest.Util;

using Nsu.Contest.Entity;

public class EmployeeReader
{
    public IEnumerable<Employee> ReadEmployee(string path)
    {
        var empls = new List<Employee>();
        using (var input = new StreamReader(File.OpenRead(path)))
        {
            while (!input.EndOfStream)
            {
                var line = input.ReadLine().Split(';', 2);
                var name = line[1];
                var id = Int32.Parse(line[0]);
                empls.Add(new Employee(id, name));
            }
        }
        return empls;
    }
}

