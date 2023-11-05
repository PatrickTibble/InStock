namespace InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState
{
    public class SessionStateResponse
    {
        public Guid CurrentSessionId { get; set; }
        public bool IsCurrentSessionActive { get; set; }
    }
}