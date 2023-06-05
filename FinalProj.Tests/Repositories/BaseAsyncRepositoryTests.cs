using FinalProj.DAL.Repository.Implemintations;
using FinalProj.DAL.SqlServer;
using FinalProj.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace FinalProj.Tests.Repositories
{
    [TestClass]
    public class BaseAsyncRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private Mock<DbSet<TestEntity>> _mockDbSet;
        private BaseAsyncRepository<TestEntity> _repository;


        [TestInitialize]
        public void Setup()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockDbSet = new Mock<DbSet<TestEntity>>();

            // Настройка макета для _dbSet и передача его в _mockDbContext
            _mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(_mockDbSet.Object);

            _repository = new BaseAsyncRepository<TestEntity>(_mockDbContext.Object);
        }

        [TestMethod]
        public async Task Create_ShouldAddEntityAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            _mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Act
            await _repository.Create(entity);

            // Assert
            _mockDbSet.Verify(x => x.AddAsync(entity, default), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public void ReadAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Id = Guid.NewGuid(), Name = "Entity 1" },
                new TestEntity { Id = Guid.NewGuid(), Name = "Entity 2" }
            };
            var mockDbSet = new Mock<DbSet<TestEntity>>();
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.Provider).Returns(entities.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.Expression).Returns(entities.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.ElementType).Returns(entities.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.GetEnumerator()).Returns(entities.GetEnumerator());

            var mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(mockDbSet.Object);

            var repository = new BaseAsyncRepository<TestEntity>(mockDbContext.Object);

            // Act
            var result = repository.ReadAll();

            // Assert
            Assert.IsTrue(result.SequenceEqual(entities));
        }

        [TestMethod]
        public  void ReadById_ShouldReturnEntityWithMatchingId()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new TestEntity { Id = entityId, Name = "Test" };

            var mockDbSet = new Mock<DbSet<TestEntity>>();
            mockDbSet.Setup(x => x.FindAsync(entityId)).ReturnsAsync(entity);

            var mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(mockDbSet.Object);

            var repository = new BaseAsyncRepository<TestEntity>(mockDbContext.Object);

            // Act
            var result =  repository.ReadById(entityId);

            // Assert
            Assert.AreEqual(entity, result);
            mockDbSet.Verify(x => x.FindAsync(entityId), Times.Once);
        }


        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateEntityAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            _mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Act
            await _repository.UpdateAsync(entity);

            // Assert
            _mockDbSet.Verify(x => x.Update(entity), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteEntityAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            _mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Act
            await _repository.DeleteAsync(entity);

            // Assert
            _mockDbSet.Verify(x => x.Remove(entity), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldDeleteEntityByIdAndSaveChanges()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new TestEntity { Id = entityId, Name = "Test" };
            var entities = new List<TestEntity> { entity };

            var mockDbSet = new Mock<DbSet<TestEntity>>();
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.Provider).Returns(entities.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.Expression).Returns(entities.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.ElementType).Returns(entities.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(x => x.GetEnumerator()).Returns(entities.GetEnumerator());
            mockDbSet.Setup(x => x.Remove(entity)).Verifiable();

            var mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(x => x.Set<TestEntity>()).Returns(mockDbSet.Object);
            mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var repository = new BaseAsyncRepository<TestEntity>(mockDbContext.Object);

            // Act
            await repository.DeleteByIdAsync(entityId);

            // Assert
            mockDbSet.Verify(x => x.Remove(entity), Times.Once);
            mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

    }
}
