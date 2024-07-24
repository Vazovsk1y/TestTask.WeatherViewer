namespace TestTask.Application.Contracts;

public record Page<TItem>
{
	private const int StartCountingFrom = 1;

	public IReadOnlyCollection<TItem> Items { get; }

	public int PageIndex { get; }

	public int TotalItemsCount { get; }

	public int TotalPages { get; }

	public bool HasNextPage => PageIndex < TotalPages;

	public bool HasPreviousPage => PageIndex > StartCountingFrom;

	public Page(
		IReadOnlyCollection<TItem> items,
		int totalItemsItemsCount,
		PagingOptions? pagingOptions = null)
	{
		Validate(items, totalItemsItemsCount, pagingOptions);

		Items = items;
		TotalItemsCount = totalItemsItemsCount;
		PageIndex = pagingOptions?.PageIndex ?? StartCountingFrom;
		TotalPages = CalculateTotalPages(pagingOptions, totalItemsItemsCount);
	}

	private static int CalculateTotalPages(PagingOptions? pagingOptions, int totalItemsCount)
	{
		if (pagingOptions is null)
		{
			return StartCountingFrom;
		}

		return totalItemsCount < pagingOptions.PageSize ? StartCountingFrom : (int)Math.Ceiling(totalItemsCount / (double)pagingOptions.PageSize);
	}

	private static void Validate(
		IReadOnlyCollection<TItem> items,
		int totalItemsCount,
		PagingOptions? pagingOptions)
	{
		switch (pagingOptions)
		{
			case { PageIndex: <= 0 }:
				throw new ArgumentException("Page index must be greater than zero.");
			case { PageSize: <= 0 }:
				throw new ArgumentException("Page size must be greater than zero.");
		}

		if (items.Count > totalItemsCount)
		{
			throw new ArgumentException("Total items count must be greater than or equal page items count.");
		}

		if (pagingOptions is null && totalItemsCount != items.Count)
		{
			throw new ArgumentException("If paging options is null total items count must be equal to page items count.");
		}
	}
}