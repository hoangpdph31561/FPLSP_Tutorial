using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors
{
    public class ViewMajorUserBySearchRequest : PaginationRequest
    {
        public string? Email { get; set; }
        public Guid? MajorId { get; set; }
    }
}
