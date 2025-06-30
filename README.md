# [MoreAdvancedUnityScripting](https://github.com/youngwoocho02/MoreAdvancedUnityScripting)

An enhanced Unity scripting package that leverages modern C# patterns to prevent null exceptions and help you write more robust code.

# What is MoreAdvancedUnityScripting? And Why Should You Use It?

The main reason to use this package is to **significantly improve the safety and reliability of your code**. Traditional Unity development often suffers from the following common issues:

* **Risk of NullReferenceException**: Missing inspector assignments or destroyed objects at runtime frequently cause `NullReferenceException`, one of the most common and frustrating bugs.
* **Inconsistent error handling**: Error handling scattered inconsistently across the codebase—sometimes using `if (obj == null)`, other times `try-catch`—reduces code readability and maintainability.
* **Repetitive boilerplate code**: Writing repetitive error handling code increases the chance of mistakes and slows down development.

`MoreAdvancedUnityScripting` addresses these problems clearly and effectively:

## 1. Explicit and Reliable Error Handling (Result Pattern)  
By expressing the possibility of failure in the return type (`Result`), it forces developers to **handle both success and failure cases explicitly**. This prevents runtime crashes caused by unhandled exceptions and makes the code flow more predictable.

## 2. Solving Unity’s “Fake Null” Problem (Safe Extensions)  
In Unity, destroyed objects are not actual `null` but “fake null,” which breaks pure C# null checks like `is null`. The `.Safe()` extension method perfectly handles this Unity-specific behavior, allowing you to **safely check for null in any situation and avoid NullReferenceExceptions**.

## 3. Obstructive error syntax:
The try-catch syntax can break the natural flow of code and scatter logic, making the execution path difficult to follow

## 4. Concise and Consistent Code (Try-Catch Utilities)  
Wrapping all potentially failing operations—including async ones—with methods like `.ToResult()` removes complex `try-catch` blocks and enforces a consistent error handling style. This **improves code readability and makes maintenance easier**.

In summary, this package is a powerful tool that **reduces bugs, improves code quality, and makes long-term project management easier**. Its benefits become especially clear in team environments or large, complex Unity projects.
**MoreAdvancedUnityScripting** is a utility package designed to enhance Unity development with modern C# programming patterns and robust error handling mechanisms. It provides a set of tools that help developers write more reliable, maintainable, and expressive code by implementing functional programming concepts that are commonly used in enterprise-level applications.

The primary reason to use this package is **safety and reliability**. Traditional Unity development often relies on null checks, try-catch blocks scattered throughout the codebase, and inconsistent error handling patterns. This package addresses these issues by providing:

# Installation

**Install this Package**: Add this package using its Git URL in the Package Manager. You will need to create a `package.json` file in the root of your package folder.
    ```
    https://github.com/youngwoocho02/MoreAdvancedUnityScripting.git
    ```
# Usage Examples

## Result Pattern

The `Result<T>` class provides a type-safe way to handle operations that can succeed or fail:

```csharp
// Basic usage
public Result<int> DivideNumbers(int a, int b)
{
    if (b == 0)
    {
        return Result<int>.Failure(ErrorDetails.ExecutionError);
    }
    
    return Result<int>.Success(a / b);
}


// Using the result
var result = DivideNumbers(10, 2);
if (result.IsSuccess)
{
    Debug.Log($"Result: {result.Value}");
}
else
{
    Debug.LogError($"Error: {result.Error.Message}");
}

```



## Try-Catch Result

The `TryCatchResult` class simplifies exception handling:

```csharp
// Using try-catch
private async Awaitable SignIn()
{
    try
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    catch (Exception e)
    {
        Debug.LogError($"Error: {e.Message}");
        return;
    }
}

// Using ToResult()
private async Awaitable SignIn()
{
    var signInResult = await AuthenticationService.Instance.SignInAnonymouslyAsync().ToResult();
    if (signInResult.IsFailure)
    {
        Debug.LogError($"Error: {signInResult.Error.Message}");
        return;
    }    
}


## Safe Extensions

The `SafeExtensions` class provides null-safe operations for Unity Objects:

```csharp
[SerializeField] private GameObject serializedPrivateObject;
private Gameobject privateObject;

private void NullTest()
{
    if (serializedPrivateObject == null)
    {
        Debug.Log("serializedPrivateObject == null");
    }
    if (privateObject == null)
    {
        Debug.Log("privateObject == null");
    }
    // Result:
    // serializedPrivateObject == null
    // privateObject == null

    if (serializedPrivateObject is null)
    {
        Debug.Log("serializedPrivateObject is null");
    }
    if (privateObject is null)
    {
        Debug.Log("privateObject is null");
    }
    // Result:
    // privateObject is null

    // Not what you want

    if (serializedPrivateObject.Safe() is null)
    {
        Debug.Log("serializedPrivateObject.Safe() is null");
    }
    if (privateObject.Safe() is null)
    {
        Debug.Log("privateObject.Safe() is null");
    }

    // Result:
    // privateObject.Safe() is null
    // privateObject.Safe() is null

    // This is what you want
}

private void SetActiveIfNotNull()
{
    serializedPrivateObject?.SetActive(true);
    privateObject?.SetActive(true);
    // This will throw NullReferenceException if serializedPrivateObject is null in inspector

    serializedPrivateObject.Safe()?.SetActive(true);
    privateObject.Safe()?.SetActive(true);
    // This will not throw NullReferenceException even if serializedPrivateObject is null in inspector
}
```

# License

This project is distributed under the MIT License. See the `LICENSE` file for more information.

# Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.
