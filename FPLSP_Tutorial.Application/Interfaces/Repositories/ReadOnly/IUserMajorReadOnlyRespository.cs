using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Infrastructure.ViewModels.UserMajors;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IUserMajorReadOnlyRespository
    {
        Task<RequestResult<PaginationResponse<MajorUserDto>>> GetMajorUserWithPaginationByAdminAsync(
          ViewMajorUserWithPaginationRequest request, CancellationToken cancellationToken); 
        Task<RequestResult<PaginationResponse<MajorUserDto>>> GetMajorUserWithPaginationBySearchAsync(
          ViewMajorUserBySearchRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorUserDto>>> GetMajorUserWithPaginationBySearchMajordAsync(
          ViewMajorUserBySearchRequest request, CancellationToken cancellationToken);
    }
}
