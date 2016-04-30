using System;
using Persistence;
using PetLoveWeb.Banco;
namespace Persistence
{
    public interface IUnitOfWork
    {
        void Commit(bool shared);
        IRepositorioGenerico<tb_animal> RepositorioAnimal { get; }
        IRepositorioGenerico<tb_favoritos> RepositorioFavorito { get; }
        IRepositorioGenerico<tb_fotos> RepositorioFoto { get; }
        IRepositorioGenerico<tb_racas> RepositorioRaca { get; }
        IRepositorioGenerico<tb_usuario> RepositorioUsuario { get; }
    }
}
