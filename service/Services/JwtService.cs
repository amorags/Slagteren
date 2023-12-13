namespace service.Services;

public class JwtService
{
    private readonly JwtOptions _options;

    public JwtService(JwtOptions options)
    {
        _options = options;
    }

    public string IssueToken(SessionData data)
    {
        throw new NotImplementedException();
    }

    public SessionData ValidateAndDecodeToken(string token)
    {
        throw new NotImplementedException();
    }
}