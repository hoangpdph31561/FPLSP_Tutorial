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
    public interface IPostReadWriteRespository
    {
        Task<RequestResult<Guid>> CreateNewPost (PostEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdatePost (PostEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeletePost (PostDeleteRequest request, CancellationToken cancellationToken);
    }
}
