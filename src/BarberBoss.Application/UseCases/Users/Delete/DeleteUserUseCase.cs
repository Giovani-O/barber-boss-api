using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Users.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Deletes a user by its Id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>bool</returns>
    /// <exception cref="NotFoundException">Exception thrown if the user is not found</exception>
    /// <exception cref="ErrorOnExecution">Exception thrown if the user can't be deleted for some reason</exception>
    public async Task Execute(long id)
    {
        var user = await _readOnlyRepository.GetById(id);
        if (user is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>
            {
                {nameof(id), [ResourceErrorMessages.USER_NOT_FOUND] }
            });
        }

        var response = await _writeOnlyRepository.Delete(user.Id);

        if (!response)
        {
            throw new ErrorOnExecution(new Dictionary<string, List<string>>
            {
                {nameof(id), [ResourceErrorMessages.UNKNOWN_ERROR] }
            });
        }
        
        await _unitOfWork.Commit();
    }
}