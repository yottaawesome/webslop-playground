using System.ComponentModel.DataAnnotations;

namespace BlazorLearningSample.Models;

/// <summary>
/// Plain CLR model used by the Forms demo. Data annotations drive the
/// validation that <c>&lt;DataAnnotationsValidator /&gt;</c> picks up inside
/// an <c>EditForm</c>.
/// </summary>
public sealed class ContactForm
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2–50 chars.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "That doesn't look like an email address.")]
    public string Email { get; set; } = string.Empty;

    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int Age { get; set; } = 18;

    [Required]
    public string Topic { get; set; } = "General";

    [StringLength(500)]
    public string Message { get; set; } = string.Empty;

    public bool AcceptTerms { get; set; }
}
