namespace Building_Permit_Monitor.JSON_Objects
{
    public class OAuthToken
    {
        public OAuthToken(TokenValue Value, int Status, string Message)
        {
            _Value = Value;
            StatusCode = Status;
            StatusMessage = Message;
        }

        public string Token { get { return _Value.Token; } }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        private TokenValue _Value { get; set; }
    }

    public class TokenValue
    {
        public string Token { get; set; }
    }
}
