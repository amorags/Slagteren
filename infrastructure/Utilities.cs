namespace infrastructure;

public class Utilities
{
    // get the environment variable
    
    private static readonly Uri Uri = new Uri(Environment.GetEnvironmentVariable("pgconn")!);

    // format the PostGre connection string
    
    public static readonly string
        ProperlyFormattedConnectionString = string.Format(
            "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=2;",
            Uri.Host,
            Uri.AbsolutePath.Trim('/'),
            Uri.UserInfo.Split(':')[0],
            Uri.UserInfo.Split(':')[1],
            Uri.Port > 0 ? Uri.Port : 5432);
    
}
