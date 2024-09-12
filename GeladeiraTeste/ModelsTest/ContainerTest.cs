using Domain.Models;
using Xunit.Abstractions;

namespace GeladeiraTeste.ModelsTest
{
    public class ContainerTest
    {
        private Container container = new Container(1);

        [Fact]
        public void InstanciaContainer_Test()
        {
            Assert.Equal(container?.NumeroContainer, 1);
        }

        [Fact]
        public void RetornarContainer()
        {
            container._andar = new Andar(1, "Laticínio");

            Assert.NotNull(container.RetornarContainer(1));
        }

        [Fact]
        public void AdicionarItem_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Assert.NotNull(container.Items);
        }

        [Fact]
        public void AdicionarItem_Exception_Test()
        {
            container.Items[0] = new Item();

            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 1 };

            Assert.Throws<Exception>(() => container.AdicionarItem(0, item1));
        }

        [Fact]
        public void RetornarItem_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Assert.NotNull(container.RetornarItem(0));
        }

        [Fact]
        public void RemoverItem_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);
            container.RemoverItem(0);

            Assert.Equal(container?.Items[0].IdItem, 0);
        }

        [Fact]
        public void RemoverItem_Exception_Test()
        {
            Assert.Throws<Exception>(() => container.RemoverItem(0));
        }

        [Fact]
        public void EstaCheio_True_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            Item item2 = new Item() { Descricao = "queijo", IdItem = 3 };
            container.AdicionarItem(2, item2);

            Item item3 = new Item() { Descricao = "doce de leite", IdItem = 4 };
            container.AdicionarItem(3, item3);

            Assert.True(container.EstaCheio());
        }

        [Fact]
        public void EstaCheio_False_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            Assert.False(container.EstaCheio());
        }

        [Fact]
        public void EstaVazio_True_Test()
        {
            Assert.True(container.EstaVazio());
        }

        [Fact]
        public void EstaVazio_False_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            Assert.False(container.EstaVazio());
        }

        [Fact]
        public void LimparContainer_Test()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            container.LimparContainer();

            Assert.True(container.EstaVazio());
        }

        [Fact]
        public void LimparContainerParametro_Test()
        {
            container._andar = new Andar(1, "Laticínios");

            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            container._andar.Containers[1] = container;

            container.LimparContainer(1, 1);

            Assert.True(container.EstaVazio());
        }

        [Fact]
        public void LimparContainerParametro_Exception_Test()
        {
            container._andar = new Andar(1, "Laticínios");

            Assert.Throws<Exception>(() => container.LimparContainer(1, 1));
        }

        [Fact]
        public void ImprimirContainer()
        {
            Item item = new Item() { Descricao = "leite", IdItem = 1 };
            container.AdicionarItem(0, item);

            Item item1 = new Item() { Descricao = "iogurte", IdItem = 2 };
            container.AdicionarItem(1, item1);

            Assert.NotEqual(container.ImprimirContainer(), string.Empty);
        }
    }
}