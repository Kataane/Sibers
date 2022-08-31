namespace Sibers.Controllers;

public class ClientsController : BaseController<IService, Client>
{
    public ClientsController(IService service) : base(service) {}
}