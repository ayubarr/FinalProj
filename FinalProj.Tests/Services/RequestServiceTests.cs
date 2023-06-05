//using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
//using FinalProj.ApiModels.Response.Interfaces;
//using FinalProj.DAL.Repository.Interfaces;
//using FinalProj.Domain.Models.Entities.Persons.Users;
//using FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo;
//using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
//using FinalProj.Domain.Models.Enums;
//using FinalProj.Services.Implemintations.RequestServices;
//using FinalProj.Services.Interfaces;
//using FinalProj.Services.Mapping.Helpers;
//using Microsoft.AspNetCore.Identity;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FinalProj.Tests.Services
//{
//    [TestClass]
//    public class RequestServiceTests
//    {
//        private Mock<IBaseAsyncRepository<Request>> _mockRequestRepository;
//        private Mock<IBaseAsyncRepository<Location>> _mockLocationRepository;
//        private Mock<IBaseAsyncRepository<EcoBoxTemplate>> _mockTemplateRepository;
//        private Mock<UserManager<Client>> _mockUserManager;
//        private IRequestService _service;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mockRequestRepository = new Mock<IBaseAsyncRepository<Request>>();
//            _mockLocationRepository = new Mock<IBaseAsyncRepository<Location>>();
//            _mockTemplateRepository = new Mock<IBaseAsyncRepository<EcoBoxTemplate>>();
//            _mockUserManager = new Mock<UserManager<Client>>();

//            _service = new RequestService(
//                _mockRequestRepository.Object,
//                _mockLocationRepository.Object,
//                _mockTemplateRepository.Object,
//                _mockUserManager.Object
//            );
//        }

//        [TestMethod]
//        public async Task GetUnassignedRequests_ShouldReturnSuccessResponseWithUnassignedRequests()
//        {
//            // Arrange
//            var requests = new List<Request>
//            {
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Active, OperatorId = null, TechTeamId = null },
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Active, OperatorId = null, TechTeamId = null }
//            };
//            var unassignedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(requests.AsQueryable());

//            // Act
//            var result = await _service.GetUnassignedRequests();

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<IEnumerable<RequestDTO>>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            CollectionAssert.AreEqual((List<RequestDTO>)unassignedRequestsDTO, result.Data.ToList());
//        }

//        [TestMethod]
//        public async Task GetClosedRequestsByOperatorId_ShouldReturnSuccessResponseWithClosedRequests()
//        {
//            // Arrange
//            var operatorId = "operator1";
//            var requests = new List<Request>
//            {
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Closed, OperatorId = operatorId },
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Closed, OperatorId = operatorId }
//            };
//            var closedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(requests.AsQueryable());

//            // Act
//            var result = await _service.GetClosedRequestsByOperatorId(operatorId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<IEnumerable<RequestDTO>>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            CollectionAssert.AreEqual((List<RequestDTO>)closedRequestsDTO, result.Data.ToList());
//        }

//        [TestMethod]
//        public async Task GetActiveRequestsByOperatorId_ShouldReturnSuccessResponseWithActiveRequests()
//        {
//            // Arrange
//            var operatorId = "operator1";
//            var requests = new List<Request>
//            {
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Active, OperatorId = operatorId },
//                new Request { Id = Guid.NewGuid(), RequestStatus = Status.Active, OperatorId = operatorId }
//            };
//            var activeRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(requests.AsQueryable());

//            // Act
//            var result = await _service.GetActiveRequestsByOperatorId(operatorId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<IEnumerable<RequestDTO>>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            CollectionAssert.AreEqual((List<RequestDTO>)activeRequestsDTO, result.Data.ToList());
//        }

//        [TestMethod]
//        public async Task AssignRequestToTeam_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var teamId = "team1";
//            var request = new Request { Id = requestId };

//            _mockRequestRepository.Setup(x => x.ReadByIdAsync(requestId)).ReturnsAsync(request);

//            // Act
//            var result = await _service.AssignRequestToTeam(requestId, teamId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsTrue(result.Data);
//            Assert.AreEqual(teamId, request.TechTeamId);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Once);
//        }

//        [TestMethod]
//        public async Task AssignRequestToOperator_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var operatorId = "operator1";
//            var request = new Request { Id = requestId };

//            _mockRequestRepository.Setup(x => x.ReadByIdAsync(requestId)).ReturnsAsync(request);

//            // Act
//            var result = await _service.AssignRequestToOperator(requestId, operatorId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsTrue(result.Data);
//            Assert.AreEqual(operatorId, request.OperatorId);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Once);
//        }

//        [TestMethod]
//        public async Task MarkRequestAsCompleted_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var request = new Request { Id = requestId };

//            _mockRequestRepository.Setup(x => x.ReadByIdAsync(requestId)).ReturnsAsync(request);

//            // Act
//            var result = await _service.MarkRequestAsCompleted(requestId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsTrue(result.Data);
//            Assert.AreEqual(Status.Completed, request.RequestStatus);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Once);
//        }

//        [TestMethod]
//        public async Task AssignLocationToRequest_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var locationId = Guid.NewGuid();
//            var request = new Request { Id = requestId };
//            var location = new Location { Id = locationId };

//            _mockRequestRepository.Setup(x => x.ReadByIdAsync(requestId)).ReturnsAsync(request);
//            _mockLocationRepository.Setup(x => x.ReadByIdAsync(locationId)).ReturnsAsync(location);

//            // Act
//            var result = await _service.AssignLocationToRequest(requestId, locationId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsTrue(result.Data);
//            Assert.AreEqual(location, request.Location);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Once);
//        }

//        [TestMethod]
//        public async Task SetEcoBoxQuantityAndTemplate_WithAvailableLocationAndEcoBoxes_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var quantity = 3;
//            var templateId = Guid.NewGuid();
//            var request = new Request { Id = requestId };
//            var location = new Location { Id = Guid.NewGuid() };
//            var ecoBox1 = new EcoBox();
//            var ecoBox2 = new EcoBox();
//            var ecoBox3 = new EcoBox();
//            location.EcoBoxes = new List<EcoBox> { ecoBox1, ecoBox2, ecoBox3 };
//            var template = new EcoBoxTemplate { Id = templateId };

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(new List<Request> { request }.AsQueryable());
//            _mockRequestRepository.Setup(x => x.UpdateAsync(request)).Returns(Task.CompletedTask);
//            _mockLocationRepository.Setup(x => x.ReadByIdAsync(location.Id)).ReturnsAsync(location);
//            _mockTemplateRepository.Setup(x => x.ReadByIdAsync(templateId)).ReturnsAsync(template);

//            // Act
//            var result = await _service.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsTrue(result.Data);
//            Assert.AreEqual(template, ecoBox1.Template);
//            Assert.AreEqual(template, ecoBox2.Template);
//            Assert.AreEqual(template, ecoBox3.Template);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Once);
//        }

//        [TestMethod]
//        public async Task SetEcoBoxQuantityAndTemplate_WithUnavailableLocation_ShouldReturnNotFoundResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var quantity = 3;
//            var templateId = Guid.NewGuid();
//            var request = new Request { Id = requestId };
//            var template = new EcoBoxTemplate { Id = templateId };

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(new List<Request> { request }.AsQueryable());
//            _mockRequestRepository.Setup(x => x.UpdateAsync(request)).Returns(Task.CompletedTask);

//            // Act
//            var result = await _service.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsFalse(result.IsSuccess);
//            Assert.IsNull(result.Data);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Never);
//        }

//        [TestMethod]
//        public async Task SetEcoBoxQuantityAndTemplate_WithUnavailableEcoBoxes_ShouldReturnNotFoundResponse()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var quantity = 3;
//            var templateId = Guid.NewGuid();
//            var request = new Request { Id = requestId };
//            var location = new Location { Id = Guid.NewGuid() };
//            var template = new EcoBoxTemplate { Id = templateId };

//            _mockRequestRepository.Setup(x => x.ReadAllAsync()).ReturnsAsync(new List<Request> { request }.AsQueryable());
//            _mockRequestRepository.Setup(x => x.UpdateAsync(request)).Returns(Task.CompletedTask);
//            _mockLocationRepository.Setup(x => x.ReadByIdAsync(location.Id)).ReturnsAsync(location);

//            // Act
//            var result = await _service.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<bool>>(result);
//            Assert.IsFalse(result.IsSuccess);
//            Assert.IsNull(result.Data);
//            _mockRequestRepository.Verify(x => x.UpdateAsync(request), Times.Never);
//        }

//        [TestMethod]
//        public async Task CreateRequest_ShouldReturnSuccessResponse()
//        {
//            // Arrange
//            var requestDTO = new RequestDTO
//            {
//                BoxQuantity = 0,
//                WorkType = WorkTypes.EcoBoxInstallation,
//                RequestType = Types.RequestExecution,
//                ClientId = "client1"
//            };
//            var client = new Client { Id = requestDTO.ClientId };

//            _mockUserManager.Setup(x => x.FindByIdAsync(requestDTO.ClientId)).ReturnsAsync(client);
//            _mockRequestRepository.Setup(x => x.Create(It.IsAny<Request>())).Returns(Task.CompletedTask);

//            // Act
//            var result = await _service.CreateRequest(requestDTO);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<Guid>>(result);
//            Assert.IsTrue(result.IsSuccess);
//            Assert.IsNotNull(result.Data);
//            _mockRequestRepository.Verify(x => x.Create(It.IsAny<Request>()), Times.Once);
//        }

//        [TestMethod]
//        public async Task CreateRequest_WithInvalidClient_ShouldReturnErrorResponse()
//        {
//            // Arrange
//            var requestDTO = new RequestDTO
//            {
//                BoxQuantity = 0,
//                WorkType = WorkTypes.EcoBoxInstallation,
//                RequestType = Types.RequestExecution,
//                ClientId = "client1"
//            };

//            _mockUserManager.Setup(x => x.FindByIdAsync(requestDTO.ClientId)).ReturnsAsync((Client)null);

//            // Act
//            var result = await _service.CreateRequest(requestDTO);

//            // Assert
//            Assert.IsInstanceOfType<IBaseResponse<Guid>>(result);
//            Assert.IsFalse(result.IsSuccess);
//            Assert.IsNull(result.Data);
//            _mockRequestRepository.Verify(x => x.Create(It.IsAny<Request>()), Times.Never);
//        }
//    }
//}
