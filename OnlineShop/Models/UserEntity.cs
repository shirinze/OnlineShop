using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public class UserEntity
{
    public int UserEntityId { get; set; }
    [Required]
    public string FirstName { get; set; } =string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; }= string.Empty;    
    public bool IsActive { get; set; }

    public static UserEntity Create(string firstName,string lastName,string phone)
    {
        return new UserEntity(firstName, lastName, phone);
    }

    public void Update(string firstName,string lastName,string phone,bool isActive)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhone(phone);
        SetIsActive(isActive);
    }
    public UserEntity(string firstName, string lastName, string phone)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhone(phone);
        SetIsActive(true);
    }

    private void SetPhone(string phone)
    {

        if (string.IsNullOrEmpty(phone))
            throw new ArgumentNullException(nameof(phone));

        Phone = phone;
    }

    private void SetLastName(string lastName)
    {

        if (string.IsNullOrEmpty(lastName))
            throw new ArgumentNullException(nameof(lastName));
        LastName = lastName;
    }

    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new ArgumentNullException(nameof(firstName));
        FirstName = firstName;
    }
    private void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

}
