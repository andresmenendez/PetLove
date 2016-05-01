using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistence;
using PetLoveWeb.Models;
using PetLoveWeb.Banco;


namespace PetLoveWeb.Gerenciadores
{
    /// <summary>
    /// Gerencia todas as regras de negócio da entidade Usuario
    /// </summary>
    
    public class GerenciadorRaca
    {
        private IUnitOfWork unitOfWork;
        private bool shared;

        private static GerenciadorRaca gRaca;

        public GerenciadorRaca()
        {
            this.unitOfWork = new UnitOfWork();
            shared = false;
        }

        public static GerenciadorRaca GetInstance()
        {
            if (gRaca == null)
            {
                gRaca = new GerenciadorRaca();
            }
            return gRaca;
        }

        /// <summary>
        /// Construtor acessado apenas dentro do componentes service e permite compartilhar
        /// contexto com outras classes de negócio
        /// </summary>
        /// <param name="unitOfWork"></param>
        internal GerenciadorRaca(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            shared = true;
        }


        /// <summary>
        /// Consulta padrão para retornar dados do autor como model
        /// </summary>
        /// <returns></returns>
        private IQueryable<RacaModel> GetQuery()
        {
            IQueryable<tb_racas> tb_racas = unitOfWork.RepositorioRaca.GetQueryable();
            var query = from raca in tb_racas 
                        select new RacaModel
                        {
                            Id_Raca = raca.IDRaca,
                            NomeRaca = raca.Nome,
                            Tipo = raca.Tipo
                        };
            return query;
        }

        /// <summary>
        /// Obter todos as entidades cadastradas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RacaModel> ObterTodos()
        {
            return GetQuery();
        }

        
        /// <summary>
        /// Obtém um autor
        /// </summary>
        /// <param name="idUsuario">Identificador do autor na base de dados</param>
        /// <returns>Usuario model</returns>
        public RacaModel Obter(int idRaca)
        {
            IEnumerable<RacaModel> users = GetQuery().Where(racaModel => racaModel.Id_Raca == idRaca);
            return users.ElementAtOrDefault(0);
        }

    }
}
