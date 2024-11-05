using Core.Persistence.Paging;

namespace Core.Application.Response;

public class GetListResponse<T>: BasePageableModel
{
    private IList<T> _items;

    public IList<T> Items
    {
        get => _items ??= Enumerable.Empty<T>().ToList();
        set => _items = value;
    }
}
