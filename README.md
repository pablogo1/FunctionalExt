# FunctionalExt
Functional programming types Result and Option
## Option Type

The `Option` type represents a value that may or may not exist. It is useful for modeling optional values without resorting to null references. The `Option` type has two possible states:
- `Some(value)`: Represents an existing value.
- `None`: Represents the absence of a value.

### Example Usage
```csharp
Option<int> maybeNumber = Some(42);
Option<int> noNumber = None<int>();

maybeNumber.Match(
    some: value => Console.WriteLine($"Value: {value}"),
    none: () => Console.WriteLine("No value")
);
```

## Result Type

The `Result` type represents the outcome of an operation that can either succeed or fail. It is useful for error handling without using exceptions. The `Result` type has two possible states:
- `Success(value)`: Represents a successful result with a value.
- `Fail(error)`: Represents a failure with an error.

### Example Usage
```csharp
Result<int, GenericError> divide(int numerator, int denominator)
{
    if (denominator == 0)
        return Fail<int, GenericError>(new GenericError("Division by zero"));
    return Success<int, GenericError>(numerator / denominator);
}

var result = divide(10, 2);
result.Match(
    success: value => Console.WriteLine($"Result: {value}"),
    error: err => Console.WriteLine($"Error: {err}")
);
```

## Installation

To use this library, add it to your project via NuGet:
```bash
dotnet add package FunctionalExt
```

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.