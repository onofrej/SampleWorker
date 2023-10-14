namespace SampleWorker.Application.Shared.ErrorHandling;

[ExcludeFromCodeCoverage]
public readonly struct Error : IEquatable<Error>
{
    public Error(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; }

    public string ErrorMessage { get; }

    public static bool operator !=(Error left, Error right)
    {
        return !(left == right);
    }

    public static bool operator ==(Error left, Error right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(Error other)
    {
        return ErrorCode == other.ErrorCode &&
            ErrorMessage == other.ErrorMessage;
    }

    public override bool Equals(object? obj)
    {
        return obj is Error error && Equals(error);
    }

    public override readonly int GetHashCode()
    {
        return ErrorCode.GetHashCode();
    }
}