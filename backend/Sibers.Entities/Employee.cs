namespace Sibers.Entities;

public class Employee : Entity, IEntityWithTwoCollection<Project, Goal>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }


    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    
    public ICollection<Goal>? Goals { get; set; }
    public ICollection<Project>? Projects { get; set; }


    public ICollection<Project>? Get(First? diff) => Projects;
    public ICollection<Goal>? Get(Second? diff) => Goals;


    public string FullName
    {
        get
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }


#if DEBUG
    public string? Password { get; set; }
#endif

}