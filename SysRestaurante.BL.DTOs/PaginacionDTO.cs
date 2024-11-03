using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class PaginacionDTO
    {
        public class PaginacionInputDTO
        {
            public int Take { get; set; }
            public int Skip { get; set; }
            public bool IsCount { get; set; }
        }
        public class PaginacionOutputDTO<T>
        {
            public T Data { get; set; }
            public int Count { get; set; }
        }
    }
}
