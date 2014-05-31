using System;

namespace CDC.Dominio.ViewModels
{
    public class PagingInfo
    {
        public int totalItens { get; set; }

        public int itensPagina { get; set; }

        public int paginaAtual { get; set; }

        public int totalPaginas
        {
            get { return (int) Math.Ceiling((decimal) totalItens/itensPagina); }
        }
    }
}