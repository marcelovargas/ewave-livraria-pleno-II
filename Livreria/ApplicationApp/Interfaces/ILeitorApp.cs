namespace ApplicationApp.Interfaces
{
    using Entities;
    public interface ILeitorApp : IGenericApp<Leitor>
    {
        Leitor GetEntityById(string idLeitor);
    }
}
