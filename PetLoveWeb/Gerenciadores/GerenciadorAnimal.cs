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
    
    public class GerenciadorAnimal
    {
        private IUnitOfWork unitOfWork;
        private bool shared;

        private static GerenciadorAnimal gAnimal;

        public GerenciadorAnimal()
        {
            this.unitOfWork = new UnitOfWork();
            shared = false;
        }

        public static GerenciadorAnimal GetInstance()
        {
            if (gAnimal == null)
            {
                gAnimal = new GerenciadorAnimal();
            }
            return gAnimal;
        }

        /// <summary>
        /// Construtor acessado apenas dentro do componentes service e permite compartilhar
        /// contexto com outras classes de negócio
        /// </summary>
        /// <param name="unitOfWork"></param>
        internal GerenciadorAnimal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            shared = true;
        }

        /// <summary>
        /// Insere um novo na base de dados
        /// </summary>
        /// <param name="animalModel">Dados do modelo</param>
        /// <returns>Chave identificante na base</returns>
        public int Inserir(AnimalModel animalModel)
        {
            tb_animal animalE = new tb_animal();
            Atribuir(animalModel, animalE);
            unitOfWork.RepositorioAnimal.Inserir(animalE);
            unitOfWork.Commit(shared);
            return animalE.IDAnimal;
        }

        /// <summary>
        /// Altera dados na base de dados
        /// </summary>
        /// <param name="animalModel"></param>
        public void Editar(AnimalModel animalModel)
        {
            tb_animal animalE = new tb_animal(); 
            Atribuir(animalModel, animalE);
            unitOfWork.RepositorioAnimal.Editar(animalE);
            unitOfWork.Commit(shared);
        }

        /// <summary>
        /// Remove da base de dados
        /// </summary>
        /// <param name="animalModel"></param>
        public void Remover(int idAnimal)
        {
            unitOfWork.RepositorioAnimal.Remover(user => user.IDAnimal == idAnimal);
            unitOfWork.Commit(shared);
        }


        /// <summary>
        /// Consulta padrão para retornar dados do autor como model
        /// </summary>
        /// <returns></returns>
        private IQueryable<AnimalModel> GetQuery()
        {
            IQueryable<tb_animal> tb_animal = unitOfWork.RepositorioAnimal.GetQueryable();
            var query = from animal in tb_animal 
                        select new AnimalModel
                        {
                            Id_Usuario = animal.IDUsuario,
                            Id_Animal = animal.IDAnimal,
                            Id_Raca = animal.IDRaca,
                            Categoria = animal.Categoria,
                            Latitude = animal.Latitude,
                            Longitude = animal.Longitude,
                            Nascimento = (DateTime)animal.Nascimento,
                            NomeAnimal = animal.Nome,
                            Sexo = animal.Sexo
                        };
            return query;
        }

        /// <summary>
        /// Obter todos as entidades cadastradas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AnimalModel> ObterTodos()
        {
            return GetQuery();
        }

        
        /// <summary>
        /// Obtém um autor
        /// </summary>
        /// <param name="idUsuario">Identificador do autor na base de dados</param>
        /// <returns>Usuario model</returns>
        public AnimalModel Obter(int idAnimal)
        {
            IEnumerable<AnimalModel> users = GetQuery().Where(animalModel => animalModel.Id_Animal == idAnimal);

            return users.ElementAtOrDefault(0);
        }

        public AnimalModel ObterPorUsuario(int idUsuario)
        {
            IEnumerable<AnimalModel> users = GetQuery().Where(animalModel => animalModel.Id_Usuario == idUsuario);

            return users.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Atribui dados do Usuario Model para o Usuario Entity
        /// </summary>
        /// <param name="animalModel">Objeto do modelo</param>
        /// <param name="animalE">Entity mapeada da base de dados</param>
        private void Atribuir(AnimalModel animalModel, tb_animal animalE)
        {
            animalE.IDUsuario = animalModel.Id_Usuario;
            animalE.IDAnimal = animalModel.Id_Animal;
            animalE.IDRaca = animalModel.Id_Raca;
            animalE.Categoria = animalModel.Categoria;
            animalE.Nascimento = animalModel.Nascimento;
            animalE.Sexo = animalModel.Sexo;
            animalE.Latitude = animalModel.Latitude;
            animalE.Longitude = animalModel.Longitude;
        }
    }
}
