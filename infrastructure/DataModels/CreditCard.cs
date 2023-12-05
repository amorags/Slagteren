namespace infrastructure.DataModels;

public class CreditCard
{
    public int Card_Id { get; set; }
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationDate { get; set; }
    public int Ccv { get; set; }
    
}