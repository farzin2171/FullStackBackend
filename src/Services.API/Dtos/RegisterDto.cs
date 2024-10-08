namespace Services.API.Dtos;

public class RegisterDto : LoginDto
{
    public string Email { get; set; } = string.Empty;
}

