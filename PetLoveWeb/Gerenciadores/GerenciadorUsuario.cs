using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence;
using PetLoveWeb.Models;
using PetLoveWeb.Banco;


namespace SQLEscola.Gerenciadores
{
    /// <summary>
    /// Gerencia todas as regras de negócio da entidade Usuario
    /// </summary>
    
    public class GerenciadorUsuario
    {
        private IUnitOfWork unitOfWork;
        private bool shared;

        private static GerenciadorUsuario gUsuario;

        public GerenciadorUsuario()
        {
            this.unitOfWork = new UnitOfWork();
            shared = false;
        }

        public static GerenciadorUsuario GetInstance()
        {
            if (gUsuario == null)
            {
                gUsuario = new GerenciadorUsuario();
            }
            return gUsuario;
        }

        /// <summary>
        /// Construtor acessado apenas dentro do componentes service e permite compartilhar
        /// contexto com outras classes de negócio
        /// </summary>
        /// <param name="unitOfWork"></param>
        internal GerenciadorUsuario(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            shared = true;
        }

        /// <summary>
        /// Insere um novo na base de dados
        /// </summary>
        /// <param name="usuarioModel">Dados do modelo</param>
        /// <returns>Chave identificante na base</returns>
        public int Inserir(UsuarioModel usuarioModel)
        {
            tb_usuario usuarioE = new tb_usuario();
            Atribuir(usuarioModel, usuarioE);
            unitOfWork.RepositorioUsuario.Inserir(usuarioE);
            unitOfWork.Commit(shared);
            return usuarioE.IDUsuario;
        }

        /// <summary>
        /// Altera dados na base de dados
        /// </summary>
        /// <param name="usuarioModel"></param>
        public void Editar(UsuarioModel usuarioModel)
        {
            tb_usuario usuarioE = new tb_usuario(); 
            Atribuir(usuarioModel, usuarioE);
            unitOfWork.RepositorioUsuario.Editar(usuarioE);
            unitOfWork.Commit(shared);
        }

        /// <summary>
        /// Remove da base de dados
        /// </summary>
        /// <param name="usuarioModel"></param>
        public void Remover(int idUsuario)
        {
            unitOfWork.RepositorioUsuario.Remover(user => user.IDUsuario == idUsuario);
            unitOfWork.Commit(shared);
        }


        /// <summary>
        /// Consulta padrão para retornar dados do autor como model
        /// </summary>
        /// <returns></returns>
        private IQueryable<UsuarioModel> GetQuery()
        {
            IQueryable<tb_usuario> tb_usuario = unitOfWork.RepositorioUsuario.GetQueryable();
            var query = from user in tb_usuario 
                        select new UsuarioModel
                        {
                            Id_Usuario = user.IDUsuario,
                            Nome = user.Nome,
                            Email = user.Email,
                            Usuario = user.Usuario,
                            Telefone = user.Telefone
                        };
            return query;
        }

        /// <summary>
        /// Obter todos as entidades cadastradas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UsuarioModel> ObterTodos()
        {
            return GetQuery();
        }

        
        /// <summary>
        /// Obtém um autor
        /// </summary>
        /// <param name="idUsuario">Identificador do autor na base de dados</param>
        /// <returns>Usuario model</returns>
        public UsuarioModel Obter(int idUsuario)
        {
            IEnumerable<UsuarioModel> users = GetQuery().Where(usuarioModel => usuarioModel.Id_Usuario == idUsuario);

            return users.ElementAtOrDefault(0);
        }


        /// <summary>
        /// Obtém um user pelo nome
        /// </summary>
        /// <param name="nome">Nome do user que será buscado base de dados</param>
        /// <returns>Usuario model</returns>
        public UsuarioModel ObterPorEmail(string email)
        {
            IEnumerable<UsuarioModel> users = GetQuery().Where(usuarioModel => usuarioModel.Email.Equals(email));

            return users.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Atribui dados do Usuario Model para o Usuario Entity
        /// </summary>
        /// <param name="usuarioModel">Objeto do modelo</param>
        /// <param name="usuarioE">Entity mapeada da base de dados</param>
        private void Atribuir(UsuarioModel usuarioModel, tb_usuario usuarioE)
        {
            usuarioE.IDUsuario = usuarioModel.Id_Usuario;
            usuarioE.Nome = usuarioModel.Nome;
            usuarioE.Email = usuarioModel.Email;
            usuarioE.Usuario = usuarioModel.Usuario;
            usuarioE.Telefone = usuarioModel.Telefone;
        }
    }
}
