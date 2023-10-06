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
    public interface ITagReadWriteRepository
    {
        Task<RequestResult<int>> AddTagAsync(dynamic entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateTagAsync(TagEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteTagAsync(TagDeleteRequest request, CancellationToken cancellationToken);
    }
}
