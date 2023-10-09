using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Example;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorRequestReadOnlyRespository
    {
        Task<RequestResult<MajorRequestDto?>> GetMajorRequestByIdAsync(Guid idMajorRequest, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorRequestDto>>> GetMajorRequestWithPaginationByAdminAsync(
            ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
