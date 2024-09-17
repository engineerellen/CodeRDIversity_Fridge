using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Context;

namespace GeladeiraTeste.RepositoryTest;

public class ItemRepositoryTests
{
    private GeladeiraContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<GeladeiraContext>()
            .UseInMemoryDatabase(databaseName: "GeladeiraTestDB")
            .Options;

        return new GeladeiraContext(options);
    }

    [Fact]
    public async Task InserirItemAsync_ItemIsAdded()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var item = new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 };

        // Act
        await repository.InserirItemAsync(item);

        // Assert
        var insertedItem = context.Items.FirstOrDefault(i => i.IdItem == item.IdItem);
        Assert.NotNull(insertedItem);
        Assert.Equal("Item1", insertedItem?.Descricao);
    }

    [Fact]
    public async Task AtualizarItemAsync_ItemIsUpdated()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var item = new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 };
        await repository.InserirItemAsync(item);

        // Act
        item.Descricao = "ItemUpdated";
        await repository.AtualizarItemAsync(item);

        // Assert
        var updatedItem = context.Items.FirstOrDefault(i => i.IdItem == item.IdItem);
        Assert.NotNull(updatedItem);
        Assert.Equal("ItemUpdated", updatedItem?.Descricao);
    }

    [Fact]
    public async Task RetirarItemPorIDAsync_ItemIsRemoved()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var item = new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 };
        await repository.InserirItemAsync(item);

        // Act
        await repository.RetirarItemPorIDAsync(item.IdItem);

        // Assert
        var deletedItem = context.Items.FirstOrDefault(i => i.IdItem == item.IdItem);
        Assert.Null(deletedItem);
    }

    [Fact]
    public async Task ObterItemPorID_ReturnsItem()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var item = new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 };
        await repository.InserirItemAsync(item);

        // Act
        var retrievedItem = repository.ObterItemPorID(item.IdItem);

        // Assert
        Assert.NotNull(retrievedItem);
        Assert.Equal("Item1", retrievedItem?.Descricao);
    }

    [Fact]
    public async Task RetornarItensAsync_ReturnsAllItems()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var items = new List<Item>
            {
                new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 },
                new Item { IdItem = 2, Descricao = "Item2", Quantidade = 20 }
            };

        context.Items.AddRange(items);
        await context.SaveChangesAsync();

        // Act
        var returnedItems = await repository.RetornarItensAsync();

        // Assert
        Assert.Equal(2, returnedItems.Count);
        Assert.Contains(returnedItems, i => i.Descricao == "Item1");
        Assert.Contains(returnedItems, i => i.Descricao == "Item2");
    }

    [Fact]
    public void ItemExistente_ReturnsTrueIfItemExists()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ItemRepository(context);
        var item = new Item { IdItem = 1, Descricao = "Item1", Quantidade = 10 };
        repository.InserirItemAsync(item).Wait();

        // Act
        var exists = repository.ItemExistente(item.IdItem);

        // Assert
        Assert.True(exists);
    }
}