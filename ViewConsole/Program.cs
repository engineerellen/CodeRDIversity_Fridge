using Domain;

Geladeira objGeladeira = new();


// andar 0 - Hortifrutis
var objItemMaca = new Item() { Descricao = "Maçã", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Hotfruti", ID = 1 };
var objItemBanana = new Item() { Descricao = "Banana", Quantidade = 1, UnidadeQtd = "Cacho", Classificacao = "Hotfruti", ID = 2 };
var objItemLaranja = new Item() { Descricao = "Laranja", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Hotfruti", ID = 3 };


objGeladeira.AdicionarItem(0, 0, 0, objItemMaca);
objGeladeira.AdicionarItem(0, 0, 1, objItemBanana);
objGeladeira.AdicionarItem(0, 1, 2, objItemLaranja);

// andar 1 - Laticínios e Enlatados
var objItemLeite = new Item() { Descricao = "Leite", Quantidade = 1, UnidadeQtd = "Litro", Classificacao = "Laticínios e Enlatados", ID = 4 };
var objItemQueijo = new Item() { Descricao = "Queijo", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Laticínios e Enlatados", ID = 5 };
var objItemMilho = new Item() { Descricao = "Milho Enlatado", Quantidade = 1, UnidadeQtd = "Lata", Classificacao = "Laticínios e Enlatados", ID = 6 };

objGeladeira.AdicionarItem(1, 0, 0, objItemLeite);
objGeladeira.AdicionarItem(1, 1, 1, objItemQueijo);
objGeladeira.AdicionarItem(1, 1, 2, objItemMilho);

// andar 2 - Charcutaria, Carnes e Ovos
var objItemPresunto = new Item() { Descricao = "Presunto", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Charcutaria, Carnes e Ovos", ID = 7 };
var objItemOvos = new Item() { Descricao = "Ovos", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Charcutaria, Carnes e Ovos", ID = 8 };
var objItemCarne = new Item() { Descricao = "Carne", Quantidade = 1, UnidadeQtd = "Kilo", Classificacao = "Charcutaria, Carnes e Ovos", ID = 9 };

objGeladeira.AdicionarItem(2, 0, 0, objItemPresunto);
objGeladeira.AdicionarItem(2, 0, 1, objItemOvos);
objGeladeira.AdicionarItem(2, 1, 3, objItemCarne);

// mostrar itens no console
objGeladeira.ImprimeConteudo();

objGeladeira.RemoverItem(0, 0, 3);

// Remove um item
objGeladeira.RemoverItem(0, 0, 1);

// adicona um item numa posicao ocupada 
var objItemPera = new Item() { Descricao = "Pera", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Hotfruti", ID = 10 };
objGeladeira.AdicionarItem(0, 0, 1, objItemPera);

// limpa o container
objGeladeira.LimparContainer(1, 1);

// adiciona varios itens no container
var objItemIogurte = new Item() { Descricao = "Iogurte", Quantidade = 300, UnidadeQtd = "ML", Classificacao = "Laticínios e Enlatados", ID = 11 };
var objItemManteiga = new Item() { Descricao = "Manteiga", Quantidade = 300, UnidadeQtd = "Gramas", Classificacao = "Laticínios e Enlatados", ID = 12 };
var objItemCafe = new Item() { Descricao = "Café", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Laticínios e Enlatados", ID = 13 };

objGeladeira.AddItensAoContainer(1, 0, new List<Item> { objItemIogurte, objItemManteiga, objItemCafe });

// adiciona itens a mais que a o container pode suportar
var objItemCha = new Item() { Descricao = "Chá", Quantidade = 200, UnidadeQtd = "ML", Classificacao = "Laticínios e Enlatados", ID = 14 };
var objItemMel = new Item() { Descricao = "Mel", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Laticínios e Enlatados", ID = 15 };
var objItemPao = new Item() { Descricao = "Pão", Quantidade = 3, UnidadeQtd = "Unidades", Classificacao = "Laticínios e Enlatados", ID = 16 };
var objItemCeral = new Item() { Descricao = "Cereal", Quantidade = 300, UnidadeQtd = "Gramas", Classificacao = "Laticínios e Enlatados", ID = 17 };
var objItemGranola = new Item() { Descricao = "Granola", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Laticínios e Enlatados", ID = 18 };

objGeladeira.AddItensAoContainer(1, 0, new List<Item> { objItemCha, objItemMel, objItemPao, objItemCeral, objItemGranola });

// mostra itens no console
objGeladeira.ImprimeConteudo();