using System.Collections.Generic;

namespace BattlEyeManager.Spa.Model
{
    public class PagedResult<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
