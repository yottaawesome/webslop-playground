using Microsoft.AspNetCore.Components;

namespace BlazorLearningSample.Components.Pages;

/// <summary>
/// Code-behind for <c>CodeBehindDemo.razor</c>. The Razor compiler generates
/// a partial class from the .razor file; declaring a matching partial class
/// here lets us put fields/methods/logic in a pure C# file that's easier to
/// read, navigate and unit-test.
///
/// Note: use <see cref="InjectAttribute"/> on a property (not the
/// <c>@inject</c> directive) to inject services from a code-behind.
/// </summary>
public partial class CodeBehindDemo : ComponentBase
{
    private int lucky = Random.Shared.Next(1, 100);

    // You could also do: [Inject] private ILogger<CodeBehindDemo> Logger { get; set; } = default!;

    private void Reroll() => lucky = Random.Shared.Next(1, 100);
}
