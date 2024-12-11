namespace DateAppAPI.Errors;

public class ApiExceptions(int sttCode, string errMsg, string? details){
    public int SttCode { get; set; } = sttCode;
    public string ErrMSg { get; set; } = errMsg;
    public string? Details { get; set; } = details;

}