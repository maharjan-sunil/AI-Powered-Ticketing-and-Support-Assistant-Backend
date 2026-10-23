//using DockerDemo.Docker.Interface;
//using DockerDemo.Docker.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;
//using UserService.API.RabbitMQ;

//namespace DockerDemo.Docker.Controller
//{
//   //[Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {

//        private readonly ICategoryService _service;
//        public UserController(ICategoryService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public  async Task<ActionResult<IEnumerable<Ticket>>> GetStores()
//        {
//            return await _service.GetStores();
//        }
       
//        [HttpPost]
//        //[Authorize]
//        public async Task<IActionResult> AddStore(Ticket store)
//        {
//            // 1. Save as Pending (simulate DB)
//            //store.StoreStatus = Status.Pending;
//            Console.WriteLine($"Initial set status to {Status.Pending}");

//            // 2. Send message to RabbitMQ
//            var factory = new ConnectionFactory() { HostName = "localhost" };

//            using var connection = factory.CreateConnection();
//            using var channel = connection.CreateModel();

//            channel.QueueDeclare("store-queue", true, false, false);

//            var message = JsonSerializer.Serialize(store);
//            var body = Encoding.UTF8.GetBytes(message);

//            channel.BasicPublish("", "store-queue", null, body);

//            await _service.AddStore(store);
//            return Ok();

//        }

//        [HttpPatch]
//        [Authorize]
//        public async Task<IActionResult> UpdateStore(Ticket store)
//        {
//            await _service.UpdateStore(store);
//            return Ok();

//        }

//        [HttpDelete]
//        [Authorize(Roles = "ADMIN")]
//        public async Task<IActionResult> DeleteStore(int id)
//        {
//            await _service.DeleteStore(id);
//            return Ok();

//        }

       
//        [HttpGet("search")]
//        [Authorize]
//        public async Task<ActionResult<IEnumerable<Ticket>>> GetStoreByName(String storeName)
//        {
//            return await _service.GetStoreByName(storeName);

//        }
//    }
//}

