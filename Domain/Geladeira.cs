namespace Domain
{
    public class Geladeira
    {
        private Stack<Dictionary<int, string[]>> andares;

        public Geladeira()
        {
            andares = new Stack<Dictionary<int, string[]>>();

            // Floor 2 - Charcutaria, Carnes e Ovos
            andares.Push(new Dictionary<int, string[]> {
            { 0, new string[4] }, // Container 0
            { 1, new string[4] }  // Container 1
        });

            // Floor 1 - Laticínios e Enlatados
            andares.Push(new Dictionary<int, string[]> {
            { 0, new string[4] }, // Container 0
            { 1, new string[4] }  // Container 1
        });

            // Floor 0 - Hortifrutis
            andares.Push(new Dictionary<int, string[]> {
            { 0, new string[4] }, // Container 0
            { 1, new string[4] }  // Container 1
        });
        }

        public void InserirItem(int piso, int gaveta, int posicao, string item)
        {
            var arrPisos = andares.ToArray();

            if (piso < 0 || piso >= arrPisos.Length || !arrPisos[piso].ContainsKey(gaveta))
            {
                Console.WriteLine("Andar ou gaveta invalidos.");
                return;
            }

            if (posicao < 0 || posicao >= arrPisos[piso][gaveta].Length)
            {
                Console.WriteLine("posiçao invalida");
                return;
            }

            arrPisos[piso][gaveta][posicao] = item;
        }

        public void MostraItens()
        {
            var arrAndares = andares.ToArray();
            for (int piso = 0; piso < arrAndares.Length; piso++)
            {
                Console.WriteLine($"Andar {piso}:");
                foreach (var gaveta in arrAndares[piso])
                {
                    Console.WriteLine($"  Container {gaveta.Key}:");
                    for (int pos = 0; pos < gaveta.Value.Length; pos++)
                    {
                        string item = gaveta.Value[pos];
                        if (!string.IsNullOrEmpty(item))
                        {
                            Console.WriteLine($"    Posição {pos}: {item}");
                        }
                    }
                }
            }
        }
    }
}