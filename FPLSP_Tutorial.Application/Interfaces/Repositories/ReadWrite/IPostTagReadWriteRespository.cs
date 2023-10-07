using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IPostTagReadWriteRespository
    {
        Task<RequestResult<int>> AddPostTagAsync(dynamic entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdatePostTagAsync(PostTagEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeletePostTagAsync(PostTagDeleteRequest request, CancellationToken cancellationToken);
    }
}
