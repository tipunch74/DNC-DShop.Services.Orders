using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Common.Types;
using DShop.Messages.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
            => await _dispatcher.DispatchAsync(command);
        
        protected async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
            => await _dispatcher.DispatchAsync<TQuery, TResult>(query);

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}