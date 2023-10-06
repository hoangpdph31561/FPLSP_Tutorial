
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IPostReadOnlyRespository
    {
        Task<RequestResult<ViewPostByIdResponse?>> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ViewPostWithPaginationResponse>>> GetExampleWithPaginationByAdminAsync(
            ViewPostWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
