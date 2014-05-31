using System.Collections.Generic;
using CDC.Dominio.Models;
using System.ServiceModel;

namespace CDC.ProdutosService
{
    [ServiceContract]
    [ServiceKnownType(typeof(Produto))]
    public interface IConsultarProduto
    {
        [OperationContract]
        IEnumerable<Produto> Consultar(string nome);
    }
}
