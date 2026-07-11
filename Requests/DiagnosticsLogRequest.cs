using System.ComponentModel.DataAnnotations;

namespace FleetGuard.Requests
{
    public class DiagnosticLogRequest
    {
        [Required]
        public string LogLevel { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}