using CDC.DAL.Interface;
using CDC.DAL.Repositorio;
using CDC.Dominio.Models;
using CDC.Dominio.ViewModels;
using System.Web.Mvc;

namespace CDC.Aplicação.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly IProdutoRepositorio _repositorio;
        public CarrinhoController() : this(new ProdutoRepositorio()) { }

        public CarrinhoController(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CarrinhoIndexViewModel
            {
                op = PegarCarrinho(),
                ReturnUrl = returnUrl
            });
        }

        [Authorize]
        public RedirectToRouteResult AdicionaraoCarrinho(int IdProduto, string returnUrl)
        {
            Produto produto = _repositorio.PegarPorId(IdProduto);

            if (produto != null)
            {
                PegarCarrinho().AdicionarItem(produto, 1);
            }

            return RedirectToAction("Index", new {returnUrl});

        }

         [Authorize]
        public RedirectToRouteResult RemoverdoCarrinho(int IdProduto, string returnUrl)
        {
            Produto produto = _repositorio.PegarPorId(IdProduto);

            if (produto != null)
            {
                PegarCarrinho().RemoverItem(produto);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        private OperacoesCarrinho PegarCarrinho()
        {
            var carrinho = (OperacoesCarrinho) Session["Carrinho"];

            if (carrinho == null)
            {
                carrinho = new OperacoesCarrinho();
                Session["Carrinho"] = carrinho;
            }// fim if

            return carrinho;
        }
	}
}