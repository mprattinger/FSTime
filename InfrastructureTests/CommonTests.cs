namespace InfrastructureTests;

public class CommonTests
{
    [Fact]
    public void CheckPaths()
    {
        var p = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        Console.WriteLine(p);
    }
}