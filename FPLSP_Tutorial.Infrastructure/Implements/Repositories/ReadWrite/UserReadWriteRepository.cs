using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Application.ValueObjects.Common;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities;
using FPLSP_Tutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
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
                entity.CreatedTime = DateTime.UtcNow;     
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

        public async Task<RequestResult<int>> DeleteUserAsync(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await GetUserByIdAsync(request.Id, cancellationToken);

                user.Status = Convert.ToInt32(EntityStatus.Deleted);

                _dbContext.UserEntities.Remove(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete User"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "User"
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

                user.Username = entity.Username == null ? "N/A" : entity.Username;

                user.RoleCodes = entity.RoleCodes == null ? (JsonArray)JsonArray.Parse("[\"N/A\"]") : entity.RoleCodes;

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
