using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadOnly
{
    public interface IClientPostReadOnlyRespository
    {
        Task<RequestResult<ClientPostDetailResponse>> GetClientPostDetailAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ClientPostListResponse>>> GetAllClientPostListAsync(ClientPostListRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ClientPostListResponse>>> GetClientPostBySearchAsync(ClientPostSearchRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ClientPostParentChildResponse>>> GetParentChildPostAsync(ClientPostRequestIdWithPagination request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ClientTagResponse>>> GetPostTagsAsync(ClientPostRequestIdWithPagination request, CancellationToken cancellationToken);

    }
}
