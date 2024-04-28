namespace ControleFacil.Api.Domain.Services.Interfaces
{

    /// <summary>
    /// Interface genérica para criação de serviços do tipo CRUD.
    /// </summary>
    /// <typeparam name="RQ">Contrato de resquest</typeparam>
    /// <typeparam name="RS">Contarto de response</typeparam>
    /// <typeparam name="I">Tipo de Id</typeparam>
    public interface IServise<RQ, RS, I> where RQ : class
    {
        Task<IEnumerable<RS>> Obter(I idUsuario);
        Task<RS> Obter(I id, I idUsuario);
        Task<RS> Adicionar(RQ entidade, I idUsuario);
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);
        Task<RS> Inativar(I id, I idUsuario);

    }
}