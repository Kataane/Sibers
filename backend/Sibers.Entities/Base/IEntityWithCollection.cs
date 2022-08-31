namespace Sibers.Entities.Base;

//TDiff нужен чтобы при касте понимать к какому именно
// интерфейсу нужно каститься, в частности
// при вызове метода Get

// Данная условность нужна, чтобы была возможность работать
// с любым количеством массиов у сущности

// Как пример инкапсулированный интерфейс IEntityWithTwoCollection
// который позволяет работать с 2 массивами, который
// просто реализует два интерфейса IEntityWithCollection
public interface IEntityWithCollection<TProperty, in TDiff>
{
    public ICollection<TProperty>? Get(TDiff? diff);
}

