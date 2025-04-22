namespace OnlineShop.DTOs;

public class CreateOrUpdateUserDto
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Phone { get; set; }
    public bool IsActive { get; set; }

}
