namespace AuthApi.Dtos;

public class ResponseErrorDto
{
    public int Status { get; set; }
    public string Description { get; set; }
    public List<string> ErrorMsgs { get; set; }
}