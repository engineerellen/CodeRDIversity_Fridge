using Domain;
using Microsoft.Data.SqlClient;
using Repository;

namespace Services
{
    public class GeladeiraService
    {
        private GeladeiraContext _contexto;

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
                    var itemExistente = GetItemById(item.IdItem);

                    if (itemExistente == null)
                    {
                        _contexto.Add(item);
                        _contexto.SaveChanges();

                        return "Item cadastrado com sucesso!";
                    }
                    else
                    {
                        return "Item já existente na geladeira.";
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
                item = lstItems!= null ? lstItems.FirstOrDefault(): null;

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

        public List<Item>? GetAllItems()
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
            catch (Exception )
            {
                return null;
            }
        }

        public string RemoveItemById(int idItem)
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

    }
}

