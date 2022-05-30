// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

string opcao = "";
string nome1 = "";
string nome2 = "";
bool validOpcao;

Console.WriteLine("====== BATALHA NAVAL ======");
Console.WriteLine("Quantidade de jogadores:");

do
{
    Console.WriteLine("Digite 1 para jogar contra o computador ou 2 para dois jogadores");
    opcao = Console.ReadLine();
    if (opcao == "1" || opcao == "2")
    {
        validOpcao = true;
    }
    else
    {
        Console.WriteLine("Este não é um valor válido.");
        validOpcao = false;
    }
}
while (!validOpcao);

Console.WriteLine("\nDigite o nome do primeiro jogador:");
nome1 = Console.ReadLine();

if (opcao == "2")
{
    Console.WriteLine("\nDigite o nome do segundo jogador:");
    nome2 = Console.ReadLine();
}

char[,] jogador1 = new char[10, 10];
char[,] jogador2 = new char[10, 10];

for (int l = 0; l < 10; l++)
{
    for (int c = 0; c < 10; c++)
    {
        jogador1[l, c] = ' ';
        jogador2[l, c] = ' ';
    }
}

Console.Clear();

ConfiguraJogador(nome1, 1, jogador1);
if (opcao == "1")
{
    var naviosComp = new List<int>() { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
    foreach (var tamanhoNavio in naviosComp)
    {
        AdicionaNavioComp(tamanhoNavio, jogador2);
    }
}
else
{
    ConfiguraJogador(nome2, 2, jogador2);
}

//=====INÍCIO DOS TIROS======

Console.Clear();
char[,] tirosJogador1 = new char[10, 10];
char[,] tirosJogador2 = new char[10, 10];

for (int l = 0; l < 10; l++)
{
    for (int c = 0; c < 10; c++)
    {
        tirosJogador1[l, c] = ' ';
        tirosJogador2[l, c] = ' ';
    }
}

int acertosJog1 = 0;
int acertosJog2 = 0;
int errosJog1 = 0;
int errosJog2 = 0;

do
{
    Console.Clear();
    Console.WriteLine($"{nome1} efetua disparo:");
    DisparoJogador(tirosJogador1, jogador2);
    acertosJog1 = MatrizCounter(tirosJogador1, 'X');
    Console.WriteLine("Acertos: " + acertosJog1);
    errosJog1 = MatrizCounter(tirosJogador1, 'A');
    Console.WriteLine("Erros: " + errosJog1);
    Console.WriteLine("Pressione qualquer tecla para continuar"!);
    Console.ReadLine();

    if (acertosJog1 < 30)
    {
        if (opcao == "2")
        {
            Console.Clear();
            Console.WriteLine($"{nome2} efetua disparo:");
            DisparoJogador(tirosJogador2, jogador1);
            acertosJog2 = MatrizCounter(tirosJogador2, 'X');
            Console.WriteLine("Acertos: " + acertosJog2);
            errosJog2 = MatrizCounter(tirosJogador2, 'A');
            Console.WriteLine("Erros: " + errosJog2);
            Console.WriteLine("Pressione qualquer tecla para continuar"!);
            Console.ReadLine();
        }
        else
        {
            bool disparoValidoComp;
            do
            {
                Console.Clear();
                Random randNum = new Random();
                int colComp = randNum.Next(10);
                int linComp = randNum.Next(10);
                disparoValidoComp = ValidaDisparoComputador(colComp, linComp, tirosJogador2);

                if (disparoValidoComp)
                {
                    if (jogador1[linComp, colComp] == 'O')
                    {
                        tirosJogador2[linComp, colComp] = 'X';
                        Console.Clear();
                        Console.WriteLine($"Computador efetuou disparo:");
                        Console.WriteLine("O seu oponente acertou um navio.\n");
                        Console.WriteLine(ImprimeTabela(tirosJogador2));
                    }
                    else if (jogador1[linComp, colComp] == ' ')
                    {
                        tirosJogador2[linComp, colComp] = 'A';
                        Console.Clear();
                        Console.WriteLine($"Computador efetuou disparo:");
                        Console.WriteLine("Tiro n'água!\n");
                        Console.WriteLine(ImprimeTabela(tirosJogador2));
                    }
                }
            }
            while (!disparoValidoComp);

            acertosJog2 = MatrizCounter(tirosJogador2, 'X');
            Console.WriteLine("Acertos: " + acertosJog2);
            errosJog2 = MatrizCounter(tirosJogador2, 'A');
            Console.WriteLine("Erros: " + errosJog2);
            Console.WriteLine("Pressione qualquer tecla para continuar"!);
            Console.ReadLine();
        }
    }
}
while (!(acertosJog1 == 30 || acertosJog2 == 30));

Console.Clear();

if (opcao == "2")
{
    if (acertosJog1 == 30)
        Console.WriteLine($"JOGADOR 1 É O VENCEDOR! PARABÉNS, {nome1}!");
    else
        Console.WriteLine($"JOGADOR 2 É O VENCEDOR! PARABÉNS, {nome2}!");
}
else
{
    if (acertosJog1 == 30)
        Console.WriteLine($"VOCÊ VENCEU! PARABÉNS, {nome1}!");
    else
        Console.WriteLine($"VOCÊ PERDEU. TENTE NOVAMENTE.");
}



static string ImprimeTabela(char[,] s)
{
    string cabecalho = "|   | A | B | C | D | E | F | G | H | I | J |";
    string tabela = "";
    string preench = "";
    for (int i = 0; i < 10; i++)
    {
        if (i != 9)
        {
            preench = "\n| " + (i + 1) + " | ";
        }
        else
        {
            preench = "\n|10 | ";
        }
        tabela = tabela + preench;

        for (int j = 0; j < 10; j++)
        {
            preench = s[i, j] + " | ";
            tabela = tabela + preench;
        }
    }
    return cabecalho + tabela;
}

static int MatrizCounter(char[,] matriz, char valor)
{
    int counter = 0;
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (matriz[i, j] == valor)
            {
                counter++;
            }
        }
    }
    return counter;
}

static int ContarItens(List<string> lista, string item)
{
    int counter = 0;
    foreach (string s in lista)
    {
        if (s == item)
            counter++;
    }
    return counter;
}

static int CoordColuna(string col)
{
    int coluna = 0;
    var letraColuna = new Dictionary<string, int> { { "A", 0 },{ "B", 1 },{ "C", 2 },{ "D", 3 },{ "E", 4 },
                                                    { "F", 5 },{ "G", 6 },{ "H", 7 },{ "I", 8 },{ "J", 9 } };

    coluna = letraColuna[col];
    return coluna;
}

static int CoordLinha(string lin)
{
    int linha = int.Parse(lin);
    return linha - 1;
}

static void AdicionaNavio(char[,] jogador, int cIn, int lIn, int cFim, int lFim)
{
    for (int l = lIn; l <= lFim; l++)
    {
        for (int c = cIn; c <= cFim; c++)
        {
            jogador[l, c] = 'O';
        }
    }
}

static bool ValidaEspaco(char[,] jogador, int cIn, int lIn, int cFim, int lFim)
{
    bool vazio = true;
    for (int l = lIn; l <= lFim; l++)
    {
        for (int c = cIn; c <= cFim; c++)
        {
            if (jogador[l, c] == 'O')
            {
                vazio = false;
                break;
            }
        }
    }
    return vazio;
}

static void ConfiguraJogador(string nome, int numero, char[,] tabuleiro)
{
    string str_c1 = "";
    string str_l1 = "";
    string str_c2 = "";
    string str_l2 = "";
    int c1 = 99;
    int l1 = 99;
    int c2 = 99;
    int l2 = 99;
    bool espacoValido;
    bool validaTamanho = true;
    bool validaNavio = true;
    var embarcacao = "";
    var validaEmbarcacao = new Dictionary<string, int> {{ "PS", 4},
                                                        { "NT", 3},
                                                        { "DS", 2},
                                                        { "SB", 1}};
    var naviosDisp = new List<string>() { "PS", "NT", "NT", "DS", "DS", "DS", "SB", "SB", "SB", "SB" };

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine("Jogador " + numero + ": " + nome + ". Este é seu tabuleiro:\n");
        string tab = ImprimeTabela(tabuleiro);
        Console.WriteLine(tab);
        Console.WriteLine();

        Console.WriteLine(@$"Escolha seu navio:" + "\n" +
        "PS - Porta-Aviões (Tamanho: 5 quadrantes. Quantidade: " + ContarItens(naviosDisp, "PS") + ")\n" +
        "NT - Navio-Tanque (Tamanho: 4 quadrantes. Quantidade: " + ContarItens(naviosDisp, "NT") + ")\n" +
        "DS - Destroyer (Tamanho: 3 quadrantes. Quantidade: " + ContarItens(naviosDisp, "DS") + ")\n" +
        "SB - Submarino (Tamanho: 2 quadrantes. Quantidade: " + ContarItens(naviosDisp, "SB") + ")\n");
        embarcacao = Console.ReadLine().ToUpper();

        do
        {
            if (!validaEmbarcacao.ContainsKey(embarcacao))
            {
                validaNavio = false;
                Console.WriteLine("Embarcação inválida. Escolha o tipo de navio entre as opções dadas.");
                embarcacao = Console.ReadLine().ToUpper();
            }
            else if (!naviosDisp.Contains(embarcacao))
            {
                validaNavio = false;
                Console.WriteLine("Embarcação não disponível. Escolha outro tipo de navio entre as opções dadas.");
                embarcacao = Console.ReadLine().ToUpper();
            }
            else
            {
                validaNavio = true;
                naviosDisp.Remove(embarcacao);
            }
        }
        while (validaNavio == false);

        Console.WriteLine("Qual a sua posição?\n");

        var posicao = Console.ReadLine().ToUpper();
        Match validCoord = null;

        do
        {
            do
            {
                do
                {
                    Regex regex = new Regex(@"\b([A-J])([1-9]|10)([A-J])([1-9]|10)\b");
                    validCoord = regex.Match(posicao);
                    if (validCoord.Success)
                    {
                        str_c1 = validCoord.Groups[1].Value;
                        str_l1 = validCoord.Groups[2].Value;
                        str_c2 = validCoord.Groups[3].Value;
                        str_l2 = validCoord.Groups[4].Value;
                        c1 = CoordColuna(str_c1);
                        l1 = CoordLinha(str_l1);
                        c2 = CoordColuna(str_c2);
                        l2 = CoordLinha(str_l2);
                    }
                    else
                    {
                        Console.WriteLine("Esta não é uma coordenada válida!Insira uma nova posição:");
                        posicao = Console.ReadLine().ToUpper();
                    }
                } while (!validCoord.Success);

                int tam = 0;
                if (!((c1 == c2 && l1 < l2) || (c1 < c2 && l1 == l2)))
                {
                    Console.WriteLine("Esta não é uma coordenada válida para um navio!");
                    validaTamanho = false;
                }
                else
                {
                    if (c1 == c2 && l1 < l2)
                    {
                        tam = l2 - l1;
                    }
                    if (c1 < c2 && l1 == l2)
                    {
                        tam = c2 - c1;
                    }
                    if (tam != validaEmbarcacao[embarcacao])
                    {
                        Console.WriteLine("Tamanho da embarcação não confere com o tipo escolhido!");
                        validaTamanho = false;
                    }
                    else
                    {
                        validaTamanho = true;
                    }
                }
                if (validaTamanho == false)
                {
                    Console.WriteLine("Insira uma nova posição:");
                    posicao = Console.ReadLine().ToUpper();
                }
            }
            while (validaTamanho == false);


            espacoValido = ValidaEspaco(tabuleiro, c1, l1, c2, l2);

            if (espacoValido)
            {
                AdicionaNavio(tabuleiro, c1, l1, c2, l2);
            }
            else
            {
                Console.WriteLine("Um ou mais espaços já estão ocupados. Insira uma nova posição: ");
                posicao = Console.ReadLine().ToUpper();
            }
        }
        while (!espacoValido);

        Console.Clear();
    }
}

static void DisparoJogador(char[,] tabuleiroJogo, char[,] tabuleiroOponente)
{
    string disparo = "";
    string str_c = "";
    string str_l = "";
    int col = 99;
    int lin = 99;
    bool tiroValido;
    Match validCoord = null;

    Console.WriteLine("Este é tabuleiro de seu oponente:\n");
    Console.WriteLine(ImprimeTabela(tabuleiroJogo));
    Console.WriteLine();

    Console.WriteLine("Digite uma coordenada para seu disparo:");
    disparo = Console.ReadLine().ToUpper();

    do
    {
        do
        {
            Regex regex = new Regex(@"\b([A-J])([1-9]|10)\b");
            validCoord = regex.Match(disparo);
            if (validCoord.Success)
            {
                str_c = validCoord.Groups[1].Value;
                str_l = validCoord.Groups[2].Value;
                col = CoordColuna(str_c);
                lin = CoordLinha(str_l);
            }
            else
            {
                Console.WriteLine("Esta não é uma posição válida! Insira uma nova posição:");
                disparo = Console.ReadLine().ToUpper();
            }
        } while (!validCoord.Success);
        if (tabuleiroJogo[lin, col] == 'X' || tabuleiroJogo[lin, col] == 'A')
        {
            tiroValido = false;
            Console.WriteLine("Esta posição já foi selecionada! Insira uma nova posição:");
            disparo = Console.ReadLine().ToUpper();
        }
        else
        {
            tiroValido = true;
            if (tabuleiroOponente[lin, col] == 'O')
            {
                tabuleiroJogo[lin, col] = 'X';
                Console.Clear();
                Console.WriteLine("\nVocê acertou um navio. Parabéns!\n");
                Console.WriteLine(ImprimeTabela(tabuleiroJogo));
            }
            else if (tabuleiroOponente[lin, col] == ' ')
            {
                tabuleiroJogo[lin, col] = 'A';
                Console.Clear();
                Console.WriteLine("\nTiro n'água!\n");
                Console.WriteLine(ImprimeTabela(tabuleiroJogo));
            }
        }
    } while (!tiroValido);
}

static bool ValidaDisparoComputador(int col, int lin, char[,] tabuleiroJogo)
{
    bool tiroValido;

    if (tabuleiroJogo[lin, col] == 'X' || tabuleiroJogo[lin, col] == 'A')
    {
        tiroValido = false;
    }
    else
    {
        tiroValido = true;
    }

    return tiroValido;
}

static void AdicionaNavioComp(int tamanho, char[,] tabuleiro)
{
    int linFim = 0;
    int colFim = 0;
    Random randNum = new Random();
    int sentido = 0;
    int colIn = 0;
    int linIn = 0;
    bool espacoValido = true;

    do
    {
        do
        {
            sentido = randNum.Next(2);
            colIn = randNum.Next(10);
            linIn = randNum.Next(10);

            if (sentido == 0)
            {
                linFim = linIn;
                colFim = colIn + tamanho;
            }
            if (sentido == 1)
            {
                linFim = linIn + tamanho;
                colFim = colIn;
            }
        }
        while (colFim > 9 || linFim > 9);

        espacoValido = ValidaEspaco(tabuleiro, colIn, linIn, colFim, linFim);
        if (espacoValido == true)
        {
            AdicionaNavio(tabuleiro, colIn, linIn, colFim, linFim);
        }
    }
    while (!espacoValido);
}


