using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Domain.Enums;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite
{
    public class UserReadWriteRepository : IUserReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;
        public UserReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddUserAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                entity.Email = entity.Email;
                entity.Username = string.IsNullOrWhiteSpace(entity.Username) ? "N/A" : entity.Username;
                entity.RoleCodes = entity.RoleCodes == null ? new List<string>() : entity.RoleCodes;
                entity.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;

                entity.CreatedTime = DateTime.UtcNow;
                entity.CreatedBy = entity.CreatedBy;

                await _dbContext.UserEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create User"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "User"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateUserAsync(UserEntity entity, CancellationToken cToken)
        {
            try
            {
                var user = await _dbContext.UserEntities.FirstOrDefaultAsync(c => c.Id == entity.Id, cToken);

                user.Username = entity.Username;
                user.RoleCodes = entity.RoleCodes;
                user.Status = entity.Status;
                
                await _dbContext.SaveChangesAsync(cToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update User"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "User"
                    }
                });
            }
        }
    }
}
