using labware_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    public interface IClienteRepository
    {
        List<Cliente> ListarTodos();
        Cliente Buscar(int idCliente);
        void Cadastrar(Cliente novoCliente);
        void Deletar(int idCliente);
        void AtualizarPeloId(int idCliente, Cliente clienteAtualizado);
    }
}
