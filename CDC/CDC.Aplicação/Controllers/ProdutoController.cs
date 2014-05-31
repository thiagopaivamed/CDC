using CDC.DAL.Interface;
using CDC.DAL.Repositorio;
using CDC.Dominio.Models;
using CDC.Dominio.ViewModels;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CDC.Aplicação.Controllers
{

    public class ProdutoController : Controller
    {
        private CDCEntities db = new CDCEntities();
        private readonly IProdutoRepositorio _repositorio;
        public ProdutoController() : this(new ProdutoRepositorio()) { }

        public ProdutoController(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [Authorize]
        public ViewResult List(string categoria, int page = 1)
        {
            ProdutoListViewModel produtos = new ProdutoListViewModel
            {
                Produtos = _repositorio.PegarTodosPaginado(categoria, page),
                PagingInfo = new PagingInfo
                {
                    paginaAtual = page,
                    itensPagina = 4,
                    totalItens = categoria == null ? _repositorio.PegarTodos().Count() : _repositorio.PegarTodos().Where(e => e.TipodeProduto.Nome == categoria).Count()
                },
                Categoria = categoria
            };
            return View(produtos);
        }

        [Authorize(Users = "Thiago")]
        public ActionResult Index()
        {
            try
            {
                var produtos = _repositorio.PegarTodos();
                return View(produtos);
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }
        }

        [Authorize(Users = "Thiago")]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var produto = _repositorio.PegarPorId(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                return View(produto);
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }
        }

        [Authorize(Users = "Thiago")]
        public ActionResult Create()
        {
            ViewBag.Tipo = new SelectList(db.TipodeProduto, "IdTipoProduto", "Nome");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Users = "Thiago")]
        public ActionResult Create([Bind(Include = "IdProduto,Descricao,Imagem,Nome,Peso,Preco,Tipo")] Produto produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Tipo = new SelectList(db.TipodeProduto, "IdTipoProduto", "Nome", produto.Tipo);
                    return View(produto);
                }// fim if modelstate

                //Pegar arquivo com nome imagem
                HttpPostedFileBase photo = Request.Files["imagem"];
                //Onde salvar
                string dirPath = Server.MapPath("~/Images");
                // Pegar caminho e nome da imagem
                if (photo != null)
                {
                    string urlImagem = string.Format("{0}/{1}", "/Images", photo.FileName);
                    //Montar caminho da imagem
                    string filePath = string.Format("{0}/{1}", dirPath, photo.FileName);

                    photo.SaveAs(filePath);
                    produto.Imagem = urlImagem;
                }

                _repositorio.Criar(produto);

                TempData["message"] = string.Format("{0} foi salvo(a)", produto.Nome);

                return RedirectToAction("Index");
            }

            catch (EntitySqlException)
            {
                ModelState.AddModelError("", "Erro EntitySqlException");
                return View();
            }

            catch (EntityCommandExecutionException)
            {
                ModelState.AddModelError("", "Erro EntityCommandExecutionException");
                return View();
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }

        }

        [Authorize(Users = "Thiago")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var produto = _repositorio.PegarPorId(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Tipo = new SelectList(db.TipodeProduto, "IdTipoProduto", "Nome", produto.Tipo);
                return View(produto);
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "Thiago")]
        public ActionResult Edit([Bind(Include = "IdProduto,Descricao,Imagem,Nome,Peso,Preco,Tipo")] Produto produto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Tipo = new SelectList(db.TipodeProduto, "IdTipoProduto", "Nome", produto.Tipo);
                    return View(produto);
                }

                HttpPostedFileBase photo = Request.Files["imagem"];

                string dirPath = Server.MapPath("~/Images");

                if (photo != null)
                {
                    string urlImagem = string.Format("{0}/{1}", "/Images", photo.FileName);

                    string filePath = string.Format("{0}/{1}", dirPath, photo.FileName);

                    photo.SaveAs(filePath);

                    produto.Imagem = urlImagem;
                }

                _repositorio.Atualizar(produto);
                TempData["message"] = string.Format("{0} foi salvo(a)", produto.Nome);
                return RedirectToAction("Index");
            }

            catch (EntitySqlException)
            {
                ModelState.AddModelError("", "Erro EntitySqlException");
                return View();
            }

            catch (EntityCommandExecutionException)
            {
                ModelState.AddModelError("", "Erro EntityCommandExecutionException");
                return View();
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }

        }
        
        [HttpPost]
        [Authorize(Users = "Thiago")]
        public ActionResult Delete(int id)
        {
            try
            {
                Produto produto = _repositorio.PegarPorId(id);
                if (produto != null)
                {
                    _repositorio.Excluir(produto);
                    TempData["message"] = string.Format("{0} foi deletado(a)", produto.Nome);
                }
                else
                    return HttpNotFound();

                return RedirectToAction("Index");
            }

            finally
            {
                if (_repositorio != null)
                    _repositorio.Dispose();
            }

        }

        public JsonResult Disponibilidade(string nome, int id = 0)
        {
            bool nomeExiste = db.Produto.Any(item => item.Nome == nome && item.IdProduto != id);

            if (nomeExiste)
            {
                return Json("Nome em uso.", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
