using Domain.Models;
using Moq;
using Repository.Interface;
using Services;

namespace ServicesTests
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _itemService = new ItemService(_itemRepositoryMock.Object);
        }

        [Fact]
        public async Task InserirItemAsync_Sucesso()
        {
            var item = new Item { IdItem = 1, NumeroAndar = 1, NumeroContainer = 1, PosicaoDentroContainer = 1 };
            _itemRepositoryMock.Setup(repo => repo.ObterItemPorID(item.IdItem)).Returns((Item)null);
            _itemRepositoryMock.Setup(repo => repo.InserirItemAsync(item)).Returns(Task.CompletedTask);
            _itemRepositoryMock.Setup(repo => repo.RetornarItensAsync()).ReturnsAsync(new List<Item>());

            var result = await _itemService.InserirItemAsync(item);

            Assert.Equal("Item cadastrado com sucesso!", result);
        }

        [Fact]
        public async Task InserirItemAsync_Exception_PosicaoPreenchida()
        {
            var item = new Item{IdItem = 1,NumeroAndar = 1,NumeroContainer = 1,PosicaoDentroContainer = 1};

            var existingItem = new Item{IdItem = 2,NumeroAndar = 1,NumeroContainer = 1,PosicaoDentroContainer = 1};

               _itemRepositoryMock.Setup(repo => repo.RetornarItensAsync())
                               .ReturnsAsync(new List<Item> { existingItem });

              _itemRepositoryMock.Setup(repo => repo.ObterItemPorID(item.IdItem))
                               .Returns((Item)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _itemService.InserirItemAsync(item));

            Assert.Equal("Posição já preenchida!", exception.Message);
        }

        [Fact]
        public async Task AtualizarItemAsync_Sucesso()
        {
            var item = new Item { IdItem = 1, NumeroAndar = 1, NumeroContainer = 1, PosicaoDentroContainer = 1 };
            _itemRepositoryMock.Setup(repo => repo.AtualizarItemAsync(item)).Returns(Task.CompletedTask);


            var result = await _itemService.AtualizarItemAsync(item);

            Assert.Equal("Item alterado com sucesso!", result);
        }

        [Fact]
        public void ObterItemPorID_ID_ZERO()
        {
            var result = _itemService.ObterItemPorID(0);

            Assert.Null(result);
        }

        [Fact]
        public void ObterItemPorID_Item_Existente()
        {
            var item = new Item { IdItem = 1 };
            _itemRepositoryMock.Setup(repo => repo.ObterItemPorID(item.IdItem)).Returns(item);

            var result = _itemService.ObterItemPorID(item.IdItem);

            Assert.Equal(item, result);
        }

        [Fact]
        public async Task RetirarItemPorIDAsync_Sucesso()
        {
            var itemId = 1;
            _itemRepositoryMock.Setup(repo => repo.RetirarItemPorIDAsync(itemId)).Returns(Task.CompletedTask);

            var result = await _itemService.RetirarItemPorIDAsync(itemId);

            Assert.Equal("Item deletado com sucesso!", result);
        }

        [Fact]
        public async Task EsvaziarContainerAsync_Exception()
        {
            _itemRepositoryMock.Setup(repo => repo.RetornarItensAsync()).ReturnsAsync(new List<Item>());

            var exception = await Assert.ThrowsAsync<Exception>(() => _itemService.EsvaziarContainerAsync(3, 1));
            Assert.Equal("Container está vazio!", exception.Message);
        }
    }
}