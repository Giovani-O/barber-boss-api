using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.BarberShopTests.RegisterTests;

public class RegisterTests
{
    private readonly Mapper _mapper;
    private readonly RegisterBarberShopUseCase _registerBarberShopUseCase;

    public RegisterTests()
    {
        // Builds data for the context
        var baseBarberShop = BarberShopBuilder.Build();
        
        // Create in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "BarberShopRegisterDatabase")
            .Options;

        // Creates new db context and adds data
        var dbContext = new BarberBossDbContext(options);
        dbContext.BarberShops.Add(baseBarberShop);
        dbContext.SaveChanges();

        // Creates instances of everything that's needed for the use case
        var barberShopRepository = new BarberShopRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        _mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<BarberShopMappingProfile>();
        }));

        // Sets _unitOfWork to use the in memory context
        unitOfWork.Setup(uow => uow.Commit()).Callback(() =>
        {
            dbContext.SaveChangesAsync();
        });

        // Creates a new instance of the use case
        _registerBarberShopUseCase = new RegisterBarberShopUseCase(
            barberShopRepository,
            barberShopRepository,
            unitOfWork.Object,
            _mapper);
    }
}