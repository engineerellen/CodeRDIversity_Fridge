using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Diagnostics.CodeAnalysis;

namespace Services
{
    public class GeladeiraService
    {
        private GeladeiraContext _contexto;
        private Geladeira objGeladeira = new();

        public GeladeiraService(GeladeiraContext contexto)
        {
            _contexto = contexto;
        }

        public string InserirItem(Item item)
        {
            try
            {
                if (item != null)
                {
                    if (PosicaoPreenchida(item))
                        throw new Exception("Posição já preenchida!");

                    var itemExistente = GetItemById(item.IdItem);

                    if (itemExistente == null)
                    {
                        _contexto.Add(item);
                        _contexto.SaveChanges();

                        return "Item cadastrado com sucesso!";
                    }
                    else
                    {
                        throw new Exception("Item já existente na geladeira.");
                    }
                }
                else
                {
                    return "Item inválido!";
                }
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AtualizarItem(Item item)
        {
            try
            {
                _contexto.Entry(item).State = EntityState.Modified;

                if (item != null)
                {
                    _contexto.Update(item);
                    _contexto.SaveChanges();

                    return "Item alterado com sucesso!";
                }
                else
                {
                    return "Item inválido!";
                }
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Item? GetItemById(int idItem)
        {
            Item? item = new Item();

            try
            {
                if (idItem == 0)
                {
                    return null;
                }

                var lstItems = _contexto.Items.Where(x => x.IdItem == idItem).ToList();
                item = lstItems != null ? lstItems.FirstOrDefault() : null;

                if (item != null)
                {
                    return item;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool PosicaoPreenchida(Item item)
        {
            var lstItems = _contexto.Items.Where(i => i.NumeroAndar == item.NumeroAndar
                                                            && i.NumeroContainer == item.NumeroContainer
                                                            && i.PosicaoDentroContainer == item.PosicaoDentroContainer).ToList();
            var itemRetornado = lstItems.FirstOrDefault();

            return itemRetornado != null;
        }

        public List<Item>? RetornarItens()
        {
            List<Item> listOfItems = new List<Item>();
            try
            {

                listOfItems = _contexto.Items.ToList();

                if (listOfItems != null)
                {
                    return listOfItems;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string RetirarItemPorID(int idItem)
        {
            try
            {
                if (idItem == 0)
                {
                    return "Item inválido! Por favor tente novamente.";
                }
                else
                {
                    var item = GetItemById(idItem);

                    if (item != null)
                    {
                        _contexto.Items.Remove(item);
                        _contexto.SaveChanges();

                        return "Item " + item.Descricao + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Item não cadastrado!";
                    }
                }
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EsvaziarContainer(int numAndar, int numContainer)
        {
            var itensContainer = _contexto.Items.Where(c => c.NumeroAndar == numAndar && c.NumeroContainer == numContainer).ToList();

            if (itensContainer is not null)
            {
                foreach (var item in itensContainer)
                    RetirarItemPorID(item.IdItem);
            }

            else
                throw new Exception("Container está vazio!");

            return "Container esvaziado com sucesso!";
        }

        public string AdicionarItensNaGeladeira(List<Item> itens)
        {
            foreach (Item item in itens)
            {
                var itensContainer = _contexto.Items.Where(c => c.NumeroAndar == item.NumeroAndar && c.NumeroContainer == item.NumeroContainer).ToList();

                if (itensContainer is not null)
                    InserirItem(item);

                else
                    throw new Exception("Container já está cheio!");
            }

            return "Itens adicionados com sucesso!";
        }
    }
}