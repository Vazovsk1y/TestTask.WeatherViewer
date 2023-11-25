namespace TestTask.Application.Shared;

public record Response
{
	public string ErrorMessage { get; }

	public bool IsSuccess { get; }

	public bool IsFailure => !IsSuccess;

	protected Response(bool isSuccess, string? errorMessage) 
	{
		IsSuccess = isSuccess;
		ErrorMessage = errorMessage ?? string.Empty;
	}

	public static Response Success() => new(true, null);

	public static Response<T> Success<T>(T value) => new(value, true, null);

	public static Response Failure(string errorMessage) => new(false, errorMessage);

	public static Response<T> Failure<T>(string errorMessage) => new(default, false, errorMessage);
}

public record Response<T> : Response
{
	private readonly T? _value;

	protected internal Response(T? value, bool isSuccess, string? errorMessage) : base(isSuccess, errorMessage)
		=> _value = value;
		
	public T Value => IsFailure ?
		throw new InvalidOperationException("The value of failed result can't be accessed.")
		:
		_value!;

	public static implicit operator Response<T>(T value) => new(value, true, null);
}
