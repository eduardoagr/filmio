namespace filmio.Model {

    public class ErrorResponse {
        public ErrorDetails? Error { get; set; }
    }

    public class ErrorDetails {
        public int Code { get; set; }
        public string? Message { get; set; }
        public Error[]? Errors { get; set; }
    }

    public class Error {
        public string? Message { get; set; }
        public string? Domain { get; set; }
        public string? Reason { get; set; }
    }
}
