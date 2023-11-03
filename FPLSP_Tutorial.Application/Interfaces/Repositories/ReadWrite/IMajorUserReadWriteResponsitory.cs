using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorUserReadWriteResponsitory
    {
        Task<RequestResult<Guid>> AddMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteMajorUserAsync(DeleteMajorUserRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken);


    }
}
