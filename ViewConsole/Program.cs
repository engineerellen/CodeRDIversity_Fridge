using Domain;

Geladeira objGeladeira = new();

//Conceito de pilha- Stack

// andar 2 - Hortifrutis
var objItemMaca = new Item() { Descricao = "Maçã", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Fruta", ID = 1 };
var objItemBanana = new Item() { Descricao = "Banana", Quantidade = 1, UnidadeQtd = "Cacho", Classificacao = "Fruta", ID = 2 };
var objItemLaranja = new Item() { Descricao = "Laranja", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Fruta", ID = 3 };


objGeladeira.AdicionarItem(2, 0, 0, objItemMaca);
objGeladeira.AdicionarItem(2, 0, 1, objItemBanana);
objGeladeira.AdicionarItem(2, 1, 2, objItemLaranja);

// andar 1 - Laticínios e Enlatados
var objItemLeite = new Item() { Descricao = "Leite", Quantidade = 1, UnidadeQtd = "Litro", Classificacao = "Laticínio", ID = 4 };
var objItemQueijo = new Item() { Descricao = "Queijo", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Laticínio", ID = 5 };
var objItemMilho = new Item() { Descricao = "Milho Enlatado", Quantidade = 1, UnidadeQtd = "Lata", Classificacao = "Enlatado", ID = 6 };

objGeladeira.AdicionarItem(1, 0, 0, objItemLeite);
objGeladeira.AdicionarItem(1, 1, 1, objItemQueijo);
objGeladeira.AdicionarItem(1, 1, 2, objItemMilho);

// andar 0 - Charcutaria, Carnes e Ovos
var objItemPresunto = new Item() { Descricao = "Presunto", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Charcutaria", ID = 7 };
var objItemOvos = new Item() { Descricao = "Ovos", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Ovo", ID = 8 };
var objItemCarne = new Item() { Descricao = "Carne", Quantidade = 1, UnidadeQtd = "Kilo", Classificacao = "Carne", ID = 9 };

objGeladeira.AdicionarItem(0, 0, 0, objItemPresunto);
objGeladeira.AdicionarItem(0, 0, 1, objItemOvos);
objGeladeira.AdicionarItem(0, 1, 3, objItemCarne);

// mostrar itens no console
objGeladeira.ImprimeConteudo();

objGeladeira.RemoverItem(2, 0, 3);

// Remove um item
objGeladeira.RemoverItem(2, 0, 1);

// adicona um item numa posicao ocupada 
var objItemPera = new Item() { Descricao = "Pera", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Fruta", ID = 10 };
objGeladeira.AdicionarItem(2, 0, 1, objItemPera);

// limpa o container
objGeladeira.LimparContainer(1, 1);

// adiciona varios itens no container
var objItemIogurte = new Item() { Descricao = "Iogurte", Quantidade = 300, UnidadeQtd = "ML", Classificacao = "Laticínio", ID = 11 };
var objItemManteiga = new Item() { Descricao = "Manteiga", Quantidade = 300, UnidadeQtd = "Gramas", Classificacao = "Laticínio", ID = 12 };
var objItemCafe = new Item() { Descricao = "Café", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Enlatado", ID = 13 };

objGeladeira.AddItensAoContainer(1, 0, new List<Item> { objItemIogurte, objItemManteiga, objItemCafe });

// adiciona itens a mais que a o container pode suportar
var objItemCha = new Item() { Descricao = "Ervilha", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Enlatado", ID = 14 };
var objItemMel = new Item() { Descricao = "Mel", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Enlatados", ID = 15 };
var objItemPao = new Item() { Descricao = "Pão", Quantidade = 3, UnidadeQtd = "Unidades", Classificacao = "Padaria", ID = 16 };
var objItemCeral = new Item() { Descricao = "Cereal", Quantidade = 300, UnidadeQtd = "Gramas", Classificacao = "Elatado", ID = 17 };
var objItemGranola = new Item() { Descricao = "Granola", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Enlatado", ID = 18 };

objGeladeira.AddItensAoContainer(1, 0, new List<Item> { objItemCha, objItemMel, objItemPao, objItemCeral, objItemGranola });

// mostra itens no console
objGeladeira.ImprimeConteudo();