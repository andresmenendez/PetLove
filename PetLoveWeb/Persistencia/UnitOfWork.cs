using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetLoveWeb;
using Persistence;
using PetLoveWeb.Banco;

namespace Persistence
{
    /// <summary>
    /// UnitWork é um padrão de projeto que permite trabalhar 
    /// com vários repositórios compartilhando um mesmo contexto
    /// transacional
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private petloveEntities _context;
        private IRepositorioGenerico<tb_animal> _repAnimal;
        private IRepositorioGenerico<tb_favoritos> _repFavorito;
        private IRepositorioGenerico<tb_fotos> _repFoto;
        private IRepositorioGenerico<tb_racas> _repRaca;
        private IRepositorioGenerico<tb_usuario> _repUsuario;
        
        /// <summary>
        /// Construtor cria contexto transacional
        /// </summary>
        public UnitOfWork()
        {
            _context = new PetLoveWeb.Banco.petloveEntities();
        }
        
        #region IUnitOfWork Members

        public IRepositorioGenerico<tb_animal> RepositorioAnimal
        {
            get
            {
                if (_repAnimal == null)
                {
                    _repAnimal = new RepositorioGenerico<tb_animal>(_context);
                }

                return _repAnimal;
            }
        }

        public IRepositorioGenerico<tb_favoritos> RepositorioFavorito
        {
            get
            {
                if (_repFavorito == null)
                {
                    _repFavorito = new RepositorioGenerico<tb_favoritos>(_context);
                }

                return _repFavorito;
            }
        }

        public IRepositorioGenerico<tb_fotos> RepositorioFoto
        {
            get
            {
                if (_repFoto == null)
                {
                    _repFoto = new RepositorioGenerico<tb_fotos>(_context);
                }

                return _repFoto;
            }
        }

        public IRepositorioGenerico<tb_usuario> RepositorioUsuario
        {
            get
            {
                if (_repUsuario == null)
                {
                    _repUsuario = new RepositorioGenerico<tb_usuario>(_context);
                }

                return _repUsuario;
            }
        }

        public IRepositorioGenerico<tb_racas> RepositorioRaca
        {
            get
            {
                if (_repRaca == null)
                {
                    _repRaca = new RepositorioGenerico<tb_racas>(_context);
                }

                return _repRaca;
            }
        }
        

        /// <summary>
        /// Salva todas as mudanças realizadas no contexto
        /// quando o contexto não for compartilhado
        /// </summary>
        public void Commit(bool shared)
        {
            if (!shared)
                _context.SaveChanges();
        }

        #endregion

        private bool disposed = false;
        /// <summary>
        /// Retira da memória um determinado contexto
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Limpa contexto da memória
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
