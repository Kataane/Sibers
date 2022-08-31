namespace Sibers.Entities.Base;

public interface IEntityWithTwoCollection<TProperty1, TProperty2> :
    IEntityWithCollection<TProperty1, First>,
    IEntityWithCollection<TProperty2, Second>
{
}