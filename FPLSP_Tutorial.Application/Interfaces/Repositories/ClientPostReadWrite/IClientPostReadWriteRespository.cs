
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ClientPostReadWrite
{
    public interface IClientPostReadWriteRespository
    {
        Task<RequestResult<Guid>> AddMajorRequest(MajorRequestEntity entity, CancellationToken cancellationToken);
    }
}
