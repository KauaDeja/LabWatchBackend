using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        LabWatchContext ctx = new LabWatchContext();
        public void AtualizarPeloId(int idCliente, Cliente clienteAtualizado)
        {
            Cliente cliente = ctx.Clientes.Find(idCliente);

            if (clienteAtualizado.NomeCliente != null)
            {
                cliente.NomeCliente = clienteAtualizado.NomeCliente;
                cliente.Descricao = clienteAtualizado.Descricao;
                cliente.DataCadastro = clienteAtualizado.DataCadastro;
                ctx.Clientes.Update(cliente);
                ctx.SaveChanges();
            }
        }

        public Cliente Buscar(int idCliente)
        {
            return ctx.Clientes.FirstOrDefault(t => t.IdCliente == idCliente);
        }

        public void Cadastrar(Cliente novoCliente)
        {
            ctx.Clientes.Add(novoCliente);
            ctx.SaveChanges();
        }

        public void Deletar(int idCliente)
        {
            ctx.Clientes.Remove(Buscar(idCliente));
            ctx.SaveChanges();
        }

        public List<Cliente> ListarTodos()
        {
            return ctx.Clientes.ToList();
        }
    }
}
