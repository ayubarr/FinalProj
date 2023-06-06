using FinalProj.ApiModels.Response.Interfaces;
using FinalProj.DAL.Repository.Interfaces;
using FinalProj.Services.Implemintations.RequestServices;
using FinalProj.Tests.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj.Tests.Services
{
    [TestClass]
    public class BaseRequestServiceTests
    {
        private Mock<IBaseAsyncRepository<TestEntity>> _mockRepository;
        private BaseRequestService<TestEntity, TestEntityDTO> _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IBaseAsyncRepository<TestEntity>>();
            _service = new BaseRequestService<TestEntity, TestEntityDTO>(_mockRepository.Object);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnSuccessResponseWithId()
        {
            // Arrange
            var entityDTO = new TestEntityDTO { Name = "Test" };
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(entityDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<TestEntity>));
            Assert.IsTrue(result.IsSuccess);
            // Assert.AreEqual(entity.Id, result.Data.Id);
        }

        [TestMethod]
        public void ReadAll_ShouldReturnSuccessResponseWithEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Id = Guid.NewGuid(), Name = "Test1" },
                new TestEntity { Id = Guid.NewGuid(), Name = "Test2" }
            };

            _mockRepository.Setup(x => x.ReadAll()).Returns(entities.AsQueryable());

            // Act
            var result = _service.ReadAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<IEnumerable<TestEntity>>));
            Assert.IsTrue(result.IsSuccess);
            CollectionAssert.AreEqual(entities, result.Data.ToList());
        }

        [TestMethod]
        public async Task ReadAllAsync_ShouldReturnSuccessResponseWithEntities()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new TestEntity { Id = Guid.NewGuid(), Name = "Test1" },
                new TestEntity { Id = Guid.NewGuid(), Name = "Test2" }
            };

            _mockRepository.Setup(x => x.ReadAllAsync().Result).Returns(entities.AsQueryable());

            // Act
            var result = await _service.ReadAllAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<IEnumerable<TestEntity>>));
            Assert.IsTrue(result.IsSuccess);
            CollectionAssert.AreEqual(entities, result.Data.ToList());
        }

        [TestMethod]
        public void ReadById_ShouldReturnSuccessResponseWithEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new TestEntity { Id = entityId, Name = "Test" };

            _mockRepository.Setup(x => x.ReadById(entityId)).Returns(entity);

            // Act
            var result = _service.ReadById(entityId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<TestEntity>));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(entity, result.Data);
        }

        [TestMethod]
        public async Task ReadByIdAsync_ShouldReturnSuccessResponseWithEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new TestEntity { Id = entityId, Name = "Test" };

            _mockRepository.Setup(x => x.ReadByIdAsync(entityId)).Returns(Task.FromResult(entity));

            // Act
            var result = await _service.ReadByIdAsync(entityId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<TestEntity>));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(entity, result.Data);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnSuccessResponseWithEntity()
        {
            // Arrange
            var entityDTO = new TestEntityDTO { Id = Guid.NewGuid(), Name = "Test" };
            var entity = new TestEntity { Id = (Guid)entityDTO.Id, Name = entityDTO.Name };

            _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(entityDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<TestEntity>));
            Assert.IsTrue(result.IsSuccess);
            // Assert.AreEqual(entity, result.Data);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnSuccessResponseWithTrue()
        {
            // Arrange
            var entityDTO = new TestEntityDTO { Id = Guid.NewGuid(), Name = "Test" };

            _mockRepository.Setup(x => x.DeleteAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(entityDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<bool>));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Data);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldReturnSuccessResponseWithTrue()
        {
            // Arrange
            var entityId = Guid.NewGuid();

            _mockRepository.Setup(x => x.DeleteByIdAsync(entityId)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteByIdAsync(entityId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IBaseResponse<bool>));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Data);
        }


    }
}
