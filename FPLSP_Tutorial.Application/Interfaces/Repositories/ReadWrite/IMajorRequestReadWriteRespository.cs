using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorRequestReadWriteRespository
    {
        Task<RequestResult<Guid>> AddMajorRequestAsync(MajorRequestEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateMajorRequestAsync(MajorRequestEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteMajorRequestAsync(MajorRequestDeleteRequest request, CancellationToken cancellationToken);
    }
}
