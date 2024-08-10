using Domain;

Geladeira objGeladeira = new Geladeira();

// Floor 0 - Hortifrutis
objGeladeira.InserirItem(0, 0, 0, "Maçã");
objGeladeira.InserirItem(0, 0, 1, "Banana");
objGeladeira.InserirItem(0, 1, 2, "Laranja");

// Floor 1 - Laticínios e Enlatados
objGeladeira.InserirItem(1, 0, 0, "Leite");
objGeladeira.InserirItem(1, 1, 1, "Queijo");
objGeladeira.InserirItem(1, 1, 2, "Milho Enlatado");

// Floor 2 - Charcutaria, Carnes e Ovos
objGeladeira.InserirItem(2, 0, 0, "Presunto");
objGeladeira.InserirItem(2, 0, 1, "Ovos");
objGeladeira.InserirItem(2, 1, 3, "Carne");

objGeladeira.MostraItens();
