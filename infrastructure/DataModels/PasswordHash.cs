namespace infrastructure.DataModels;

public class PasswordHash
{
   public int Password_Id { get; set; }
   public string Password_Hash { get; set; }
   public string Salt { get; set; }
   public string Algorithm { get; set; }
}