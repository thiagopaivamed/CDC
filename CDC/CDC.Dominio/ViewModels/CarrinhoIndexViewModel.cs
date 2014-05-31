using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDC.Dominio.Models;

namespace CDC.Dominio.ViewModels
{
    public class CarrinhoIndexViewModel
    {
        public OperacoesCarrinho op { get; set; }
        public string ReturnUrl { get; set; }
    }
}
