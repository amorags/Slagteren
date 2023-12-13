namespace api.TransferModels;

public class CreateUserRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string address { get; set; }
    public int Zip { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int Phone { get; set; }
}