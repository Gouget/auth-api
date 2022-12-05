namespace auth_api.Dtos.Payloads;

public class UserPayload
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; } //PF & PJ
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public string? CorporateName { get; set; }
    public string? FantasyName { get; set; }
    public string Password { get; set; }
}
