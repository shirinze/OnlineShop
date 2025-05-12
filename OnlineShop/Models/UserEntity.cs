using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public class UserEntity
{
    public int UserEntityId { get; set; }
    [Required]
    public string FirstName { get;private set; } =string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Phone { get; private set; }= string.Empty;    
    public string TrackingCode { get; private set; }= string.Empty;    
    public bool IsActive { get; set; }

    public static UserEntity Create(string firstName,string lastName,string trackingCode,string phone)
    {
        return new UserEntity(firstName, lastName,trackingCode ,phone);
    }

    public void Update(string firstName,string lastName,string phone,bool isActive)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhone(phone);
        SetIsActive(isActive);
    }
    public UserEntity(string firstName, string lastName,string trackingCode ,string phone)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhone(phone);
        SetTrackingCode(trackingCode);
        SetIsActive(true);
    }

    private void SetPhone(string phone)
    {
        Phone = phone;
    }

    private void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    private void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }
    private void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
    private void SetTrackingCode(string trackingCode)
    {
        TrackingCode = trackingCode;
    }

}
