using Domain.Models;
using System.ComponentModel;

namespace GeladeiraTeste.ModelsTest
{
    public class ItemTest
    {
        private Item _item = new Item();

        [Fact]
        public void AdicionarItens_Test()
        {
            var andar = new Andar(1, "Laticínios");

            _item.Items = andar.Containers[0].Items;

            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            Item item2 = new Item() { Descricao = "queijo", IdItem = 3 };
            Item item3 = new Item() { Descricao = "doce de leite", IdItem = 4 };

            List<Item> novosItens = new List<Item>() { item, item1, item2, item3 };

            _item.AdicionarItens(novosItens);

            Assert.NotNull(_item.Items.Where(i => i.IdItem > 0).First());
        }

        [Fact]
        public void AdicionarItens_QtdItens_Ultrapasssado_Test()
        {
            var andar = new Andar(1, "Laticínios");

            _item.Items = andar.Containers[0].Items;

            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            Item item2 = new Item() { Descricao = "queijo", IdItem = 3 };
            Item item3 = new Item() { Descricao = "doce de leite", IdItem = 4 };
            Item item4 = new Item() { Descricao = "muçarela", IdItem = 5 };

            List<Item> novosItens = new List<Item>() { item, item1, item2, item3, item4 };

            Assert.Throws<Exception>(() => _item.AdicionarItens(novosItens));
        }

        [Fact]
        public void AdicionarItens_QtdItens_ContainerCheio_Test()
        {
            var andar = new Andar(1, "Laticínios");

            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            Item item2 = new Item() { Descricao = "queijo", IdItem = 3 };
            Item item3 = new Item() { Descricao = "doce de leite", IdItem = 4 };

            _item.Items = new List<Item>() { item, item1, item2, item3 };

            Item item4 = new Item() { Descricao = "muçarela", IdItem = 5 };
            Item item5 = new Item() { Descricao = "queijo prato", IdItem = 6 };
            Item item6 = new Item() { Descricao = "iogurte de morango", IdItem = 7 };

            List<Item> novosItens = new List<Item>() { item4, item5, item6 };

            Assert.Throws<Exception>(() => _item.AdicionarItens(novosItens));
        }

        [Fact]
        public void AdicionarItem_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };

            _item.AdicionarItem(1, 0, 0, item);

            Assert.True(true);
        }

        [Fact]
        public void AdicionarItem_Exception_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };

            Assert.Throws<Exception>(() => _item.AdicionarItem(1, 5, 0, item));
        }

        [Fact]
        public void AdicionarItens_Parametros_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            Item item2 = new Item() { Descricao = "queijo", IdItem = 3 };
            Item item3 = new Item() { Descricao = "doce de leite", IdItem = 4 };

            List<Item> novosItens = new List<Item>() { item, item1, item2, item3 };


            _item.AddItens(1, 0, novosItens);

            Assert.True(true);
        }

        [Fact]
        public void RemoverItem_Test()
        {
            _item.RemoverItem(1, 0, 0);
            Assert.True(true);
        }
    }
}