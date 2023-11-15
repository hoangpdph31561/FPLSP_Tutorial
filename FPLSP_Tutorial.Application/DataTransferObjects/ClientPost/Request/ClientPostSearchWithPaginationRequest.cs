using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostSearchWithPaginationRequest : PaginationRequest
    {
        public List<Guid> LstTags { get; set; } = new List<Guid>();
        public string StringSearch { get; set; } = string.Empty;

    }
}
