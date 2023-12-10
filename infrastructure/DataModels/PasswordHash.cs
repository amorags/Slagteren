namespace infrastructure.DataModels;

public class PasswordHash
{
   public int Customer_Id { get; set; }
   public required string Hash { get; set; }
   public required string Salt { get; set; }
   public required string Algorithm { get; set; }
}