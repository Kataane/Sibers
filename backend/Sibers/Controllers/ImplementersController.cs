namespace Sibers.Controllers;

public class ImplementersController : BaseController<IService, Implementer>
{
    public ImplementersController(IService service) : base(service) {}
}