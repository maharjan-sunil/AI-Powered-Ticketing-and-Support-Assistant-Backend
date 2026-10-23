//using DockerDemo.Data;
//using DockerDemo.Docker.Interface;
//using DockerDemo.Docker.Model;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Cryptography.X509Certificates;
//using UserService.API.Security;

//namespace DockerDemo.Docker.Service
//{
//    public class CommentService : ICategoryService
//    {

//        private readonly ApplicationDbContext _context;
//        private readonly ILogger<CommentService> _logger;
//      //  private readonly AzureKey _key;

//        public CommentService(ApplicationDbContext context, ILogger<CommentService> logger)
//        {
//            _context = context;
//        //    _key = key;
//            _logger = logger;
//        }

//        public async Task AddStore(Ticket store)
//        {
//                //   string encyrpted = await _key.Encrypt(store.Location);
//                //string decyrpted = await _key.Decrypt(encyrpted);
//                store.Location = store.Location;
//                _context.Stores.Add(store);
//                await _context.SaveChangesAsync();
//        }

//        public async Task DeleteStore(int id)
//        {
//            var store = await _context.Stores.FindAsync(id);
//            if (store != null)
//            {
//                _context.Stores.Remove(store);
//                await _context.SaveChangesAsync();
//            }
//        }

//        public async Task<ActionResult<IEnumerable<Ticket>>> GetStores()
//        {
//            return await _context.Stores.ToListAsync();
//        }

//        public async Task<ActionResult<IEnumerable<Ticket>>> GetStoreByName(string storeName)
//        {
//            IEnumerable<Ticket> filteredStores;
//            if (string.IsNullOrEmpty(storeName))
//            {
//                return await _context.Stores.ToListAsync();
//            }

//            // Use Where() with Contains() for 'like' functionality
//            // This translates to a SQL LIKE '%...%' query
//            return await _context.Stores
//                .Where(s => s.Name.Contains(storeName))
//                .ToListAsync();
//        }

//        public async Task<ActionResult<IEnumerable<Ticket>>> UpdateStore(Ticket store)
//        {


//            var storeDetail = await _context.Stores.FindAsync(store.Id);

//            // if (storeDetail == null)
//            // {
//            //   //  return NotFound($"Store with ID {store.Id} not found.");
//            // }


//            storeDetail.Name = store.Name;
//            storeDetail.Email = store.Email;


//            // // Mark the entity as modified and save
//            // _context.Entry(store).State = EntityState.Modified;

//            //// try
//            // //{
//            await _context.SaveChangesAsync();
//            return await _context.Stores
//                .Where(s => s.Id.Equals(store.Id))
//                .ToListAsync();
//            // return OK(storeDetail);
//            // //}
//            // //catch (DbUpdateConcurrencyException)
//            // // {
//            // //    return Conflict("The store was updated by another user.");
//            // //}

//        }

//        Task ICategoryService.UpdateStore(Ticket store)
//        {
//            return UpdateStore(store);
//        }
//    }
//}
