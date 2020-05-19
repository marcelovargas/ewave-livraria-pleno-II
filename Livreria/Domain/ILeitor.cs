namespace Domain
{
    using Entities;
    public interface ILeitor : IGeneric<Leitor>
    {
        Leitor GetEntityById(string id);
    }
}
