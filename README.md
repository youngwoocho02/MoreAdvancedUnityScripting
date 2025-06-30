# MoreAdvancedUnityScripting

An enhanced Unity scripting package that leverages modern C# patterns to prevent null exceptions and help you write more robust code.

## What is MoreAdvancedUnityScripting?

`MoreAdvancedUnityScripting` is a utility package designed to integrate modern C# programming patterns into your Unity projects. It provides a suite of tools focused on functional-style error handling, helping you write safer, more predictable, and highly maintainable code.

## Why Use It?

The primary reason to use this package is to **significantly improve the safety and reliability of your codebase**. Traditional Unity development often struggles with common pitfalls that this package directly addresses:

*   **Risk of `NullReferenceException`**: This is one of the most frequent bugs in Unity, often caused by missed inspector assignments or objects destroyed at runtime.
*   **Inconsistent Error Handling**: Logic is often scattered between `if (obj == null)` checks and `try-catch` blocks, reducing code readability and maintainability.
*   **Repetitive Boilerplate Code**: Writing the same null checks and exception handling logic repeatedly slows down development and increases the chance of errors.
*   **Cumbersome `try-catch` Blocks**: The standard `try-catch` syntax can disrupt the natural flow of code, making the execution path difficult to follow, especially with asynchronous operations.

`MoreAdvancedUnityScripting` provides clear and effective solutions:

### 1. Explicit and Reliable Error Handling (Result Pattern)
By returning a `Result` type, operations that can fail make their outcomes explicit. This forces the developer to handle both success and failure cases, preventing unexpected runtime crashes and making the code's behavior predictable.

### 2. Solving Unity’s “Fake Null” Problem (Safe Extensions)
Unity's destroyed objects are not truly `null` and can bypass standard C# null checks like `is null`. The `.Safe()` extension method correctly handles this engine-specific behavior, allowing you to **reliably check for null on any `UnityEngine.Object` and prevent `NullReferenceException`**.

### 3. Concise and Consistent Error Handling
Utilities like `.ToResult()` wrap potentially failing operations (including async tasks) in a clean and consistent way. This eliminates verbose `try-catch` blocks, enforces a uniform error handling strategy, and makes your code far more readable and maintainable.

## Installation

Add the package using its Git URL in the Unity Package Manager:

```
https://github.com/youngwoocho02/MoreAdvancedUnityScripting.git
```

## Usage Examples

### Result Pattern
The `Result` class provides a type-safe way to handle operations that can succeed or fail.

```csharp
// An operation that can fail
public Result Divide(int a, int b)
{
    if (b == 0)
    {
        return Result.Failure(new Error("Division by zero."));
    }
    
    return Result.Success(a / b);
}

// Consuming the result
var result = Divide(10, 0);
if (result.IsSuccess)
{
    Debug.Log($"Result: {result.Value}");
}
else
{
    Debug.LogError($"Error: {result.Error.Message}");
}
```

### Simplified Exception Handling
The `.ToResult()` extension simplifies exception handling by converting exceptions into a `Result` type.

```csharp
// Before: Using a try-catch block
private async Awaitable SignInLegacy()
{
    try
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Sign-in successful.");
    }
    catch (Exception e)
    {
        Debug.LogError($"Error: {e.Message}");
    }
}

// After: Using ToResult() for cleaner code
private async Awaitable SignIn()
{
    var signInResult = await AuthenticationService.Instance.SignInAnonymouslyAsync().ToResult();
    
    if (signInResult.IsFailure)
    {
        Debug.LogError($"Error: {signInResult.Error.Message}");
        return;
    }

    Debug.Log("Sign-in successful.");
}
```

### Safe Extensions
The `SafeExtensions` class provides null-safe operations for `UnityEngine.Object` types, correctly handling "fake null" objects.

```csharp
[SerializeField] private GameObject serializedPrivateObject;
private GameObject privateObject;

private void NullTest()
{
    // The standard 'is null' check fails for unassigned serialized fields
    // because Unity treats them as "fake null" objects, not true null.
    if (serializedPrivateObject is null)
    {
        Debug.Log("This message will NOT appear for an unassigned serialized field.");
    }

    // The .Safe() extension correctly identifies both true null and "fake null".
    if (serializedPrivateObject.Safe() is null)
    {
        Debug.Log("serializedPrivateObject.Safe() is null"); // This works as expected.
    }
    if (privateObject.Safe() is null)
    {
        Debug.Log("privateObject.Safe() is null"); // This also works as expected.
    }
}

private void SetActiveIfNotNull()
{
    // This will throw a NullReferenceException if serializedPrivateObject is unassigned in the inspector.
    // serializedPrivateObject?.SetActive(true);

    // This is safe. The operation will only be attempted if the object is not null or destroyed.
    serializedPrivateObject.Safe()?.SetActive(true);
    privateObject.Safe()?.SetActive(true);
}
```

## License

This project is distributed under the MIT License. See the `LICENSE` file for more information.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.
