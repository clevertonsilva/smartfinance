namespace smartfinance.Domain.Models.Shared
{
    public class PagedListViewModel<TEntity>
    {
        public PagedListViewModel()
        { 
       
        }
        public PagedListViewModel(IEnumerable<TEntity> items, int page, int pageSize, int totalCount)
        {
            Items = Items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IEnumerable<TEntity> Items { get;  }
        public int Page { get;  }
        public int PageSize { get;  }
        public int TotalCount { get;  }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => PageSize > 1;
    }
    
}
