using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

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
                entity.RoleCodes = entity.RoleCodes == null ? JsonConvert.DeserializeObject<List<string>>("[\"N/A\"]") : entity.RoleCodes;
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

        private async Task<UserEntity> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            var example = await _dbContext.UserEntities.FirstOrDefaultAsync(c => c.Id == idUser, cancellationToken);

            return example;
        }

        public async Task<RequestResult<int>> UpdateUserAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var user = await GetUserByIdAsync(entity.Id, cancellationToken);

                user.RoleCodes = entity.RoleCodes == null ? JsonConvert.DeserializeObject<List<string>>("[\"N/A\"]") : entity.RoleCodes;

                user.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;

                _dbContext.UserEntities.Update(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

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
