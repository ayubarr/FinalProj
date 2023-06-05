//using FinalProj.Api.Controllers;
//using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
//using FinalProj.ApiModels.Response.Interfaces;
//using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
//using FinalProj.Domain.Models.Enums;
//using FinalProj.Services.Interfaces;
//using FinalProj.Tests.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Moq;

//namespace FinalProj.Tests.Controllers
//{
//    [TestClass]
//    public class RequestControllerTests
//    {
//        private Mock<IBaseRequestService<Request, RequestDTO>> _mockBaseRequestService;
//        private Mock<IRequestService> _mockRequestService;
//        private RequestController _controller;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mockBaseRequestService = new Mock<IBaseRequestService<Request, RequestDTO>>();
//            _mockRequestService = new Mock<IRequestService>();
//            _controller = new RequestController(_mockBaseRequestService.Object, _mockRequestService.Object);
//        }

//        [TestMethod]
//        public void Get_ReturnsOkResult()
//        {
//            // Arrange
//            var requests = new List<RequestDTO> { /* создайте несколько объектов RequestDTO для теста */ };
//            var response = new ServiceResult<List<RequestDTO>>(requests);
//            _mockBaseRequestService.Setup(service => service.ReadAll()).Returns((IBaseResponse<IEnumerable<Request>>)response);

//            // Act
//            var result = _controller.Get();

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(requests, okResult.Value);
//        }

//        [TestMethod]
//        public void Get_WithId_ReturnsOkResult()
//        {
//            // Arrange
//            var id = Guid.NewGuid();
//            var requestDto = new RequestDTO
//            {
//                BoxQuantity = 0,
//                WorkType = WorkTypes.EcoBoxInstallation,
//                RequestType = Types.RequestExecution,
//                ClientId = "client1"
//            };
//            var response = new ServiceResult<RequestDTO>(requestDto);
//            _mockBaseRequestService.Setup(service => service.ReadById(id)).Returns((IBaseResponse<Request>)response);

//            // Act
//            var result = _controller.Get(id);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(requestDto, okResult.Value);
//        }

//        [TestMethod]
//        public async Task CreateRequest_ValidRequest_ReturnsOkResult()
//        {
//            // Arrange
//            var requestDto = new RequestDTO
//            {
//                BoxQuantity = 0,
//                WorkType = WorkTypes.EcoBoxInstallation,
//                RequestType = Types.RequestExecution,
//                ClientId = "client1"
//            };
//            var response = new ServiceResult<RequestDTO>(requestDto);
//            _mockRequestService.Setup(service => service.CreateRequest(requestDto)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.CreateRequest(requestDto);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response, okResult.Value);
//        }

//        [TestMethod]
//        public async Task Put_ValidRequest_ReturnsOkResult()
//        {
//            // Arrange
//            var requestDto = new RequestDTO { /* создайте объект RequestDTO для теста */ };

//            // Act
//            var result = await _controller.Put(requestDto);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkResult));
//        }

//        [TestMethod]
//        public async Task Delete_ExistingId_ReturnsOkResult()
//        {
//            // Arrange
//            var id = Guid.NewGuid();

//            // Act
//            var result = await _controller.Delete(id);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkResult));
//        }

//        [TestMethod]
//        public async Task GetUnassignedRequests_ReturnsOkResult()
//        {
//            // Arrange
//            var requests = new List<RequestDTO> { /* создайте несколько объектов RequestDTO для теста */ };
//            var response = new ServiceResult<List<RequestDTO>>(requests);
//            _mockRequestService.Setup(service => service.GetUnassignedRequests()).ReturnsAsync(response);

//            // Act
//            var result = await _controller.GetUnassignedRequests();

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(requests, okResult.Value);
//        }

//        [TestMethod]
//        public async Task GetClosedRequestsByOperatorId_WithValidOperatorId_ReturnsOkResult()
//        {
//            // Arrange
//            var operatorId = "operatorId";
//            var requests = new List<RequestDTO> { /* создайте несколько объектов RequestDTO для теста */ };
//            var response = new ServiceResult<List<RequestDTO>>(requests);
//            _mockRequestService.Setup(service => service.GetClosedRequestsByOperatorId(operatorId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.GetClosedRequestsByOperatorId(operatorId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(requests, okResult.Value);
//        }

//        [TestMethod]
//        public async Task GetActiveRequestsByOperatorId_WithValidOperatorId_ReturnsOkResult()
//        {
//            // Arrange
//            var operatorId = "operatorId";
//            var requests = new List<RequestDTO> { /* создайте несколько объектов RequestDTO для теста */ };
//            var response = new ServiceResult<List<RequestDTO>>(requests);
//            _mockRequestService.Setup(service => service.GetActiveRequestsByOperatorId(operatorId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.GetActiveRequestsByOperatorId(operatorId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(requests, okResult.Value);
//        }

//        [TestMethod]
//        public async Task AssignRequestToTeam_WithValidRequestIdAndTeamId_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var teamId = "teamId";
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.AssignRequestToTeam(requestId, teamId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.AssignRequestToTeam(requestId, teamId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//        [TestMethod]
//        public async Task AssignRequestToOperator_WithValidRequestIdAndOperatorId_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var operatorId = "operatorId";
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.AssignRequestToOperator(requestId, operatorId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.AssignRequestToOperator(requestId, operatorId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//        [TestMethod]
//        public async Task MarkRequestAsCompleted_WithValidRequestId_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.MarkRequestAsCompleted(requestId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.MarkRequestAsCompleted(requestId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//        [TestMethod]
//        public async Task AssignLocationToRequest_WithValidRequestIdAndLocationId_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var locationId = Guid.NewGuid();
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.AssignLocationToRequest(requestId, locationId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.AssignLocationToRequest(requestId, locationId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//        [TestMethod]
//        public async Task SetEcoBoxQuantityAndTemplate_WithValidRequestIdQuantityAndTemplateId_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var quantity = 5;
//            var templateId = Guid.NewGuid();
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//        [TestMethod]
//        public async Task ChangeRequestStatus_WithValidRequestIdAndNewStatus_ReturnsOkResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var newStatus = Status.Completed;
//            var response = new ServiceResult<bool>(true);
//            _mockRequestService.Setup(service => service.ChangeRequestStatus(requestId, newStatus)).ReturnsAsync(response);

//            // Act
//            var result = await _controller.ChangeRequestStatus(requestId, newStatus);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Data, okResult.Value);
//        }

//    }
//}
//}
