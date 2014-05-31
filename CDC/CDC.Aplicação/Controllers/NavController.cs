using CDC.DAL.Interface;
using CDC.DAL.Repositorio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CDC.Aplicação.Controllers
{
    public class NavController : Controller
    {
        private readonly IProdutoRepositorio _repositorio;
        public NavController() : this(new ProdutoRepositorio()) { }

        public NavController(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public PartialViewResult Menu(string categoria = null)
        {
            ViewBag.CategoriaSelecionada = categoria;
            IEnumerable<string> produtos = _repositorio.PegarTiposOrdenado();

            return PartialView(produtos);
        }
    }
}