using CDC.DAL.Interface;
using CDC.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CDC.DAL.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private CDCEntities db = new CDCEntities();
        private bool disposed = false;

        public IEnumerable<Produto> PegarTodos()
        {
            var produtos = db.Produto.Include(p => p.TipodeProduto).AsParallel().ToList();

            return produtos;
        }// Fim PegarTodos

        public IEnumerable<Produto> PegarTodosPaginado(string categoria, int pagina = 1)
        {
            const int tamPagina = 4;
            var produtos = db.Produto.Include(p => p.TipodeProduto).AsParallel().Where(p => categoria == null || p.TipodeProduto.Nome == categoria).OrderBy(p => p.IdProduto).Skip((pagina - 1) * tamPagina).Take(tamPagina);

            return produtos;
        }// Fim PegarTodos

        public IEnumerable<string> PegarTiposOrdenado()
        {
            IEnumerable<string> produtos = db.TipodeProduto.Select(x => x.Nome).Distinct().OrderBy(x => x);

            return produtos;
        }// Fim PegarTodos

        public Produto PegarPorId(int? id)
        {
            var produto = db.Produto.Include(p => p.TipodeProduto).FirstOrDefault(p => p.IdProduto == id);

            return produto;
        }// Fim PegarPorID

        public void Criar(Produto produto)
        {
            if (produto != null)
            {
                db.Produto.Add(produto);
                db.SaveChanges();
            }// fim if produto

        }//fim criar

        public void Atualizar(Produto produto)
        {
            if (produto != null)
            {
                db.Produto.Attach(produto);
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
            }// fim if (produto)

        }

        public void Excluir(Produto produto)
        {
            var id = PegarPorId(produto.IdProduto);

            if (id != null)
            {
                db.Produto.Remove(id);
                db.SaveChanges();
            }// fim if (id)
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }// fim dispose

        public IEnumerable<Produto> Procurar(string nome)
        {
            var produtos = db.Produto.Include(p => p.TipodeProduto).Where(p => p.Nome.Contains(nome));
            return produtos;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (db != null)
                    {
                        db.Dispose();
                        db = null;
                    }// fim if db

                }// fim if disposing

            }//fim if disposed
        }
    }
}
