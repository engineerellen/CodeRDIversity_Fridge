namespace Domain.Resources
{
    //tratar dados imutaveis, que não serão mudados
    //é uma classe com tipo que não serão mudados
    public sealed record LoginResource(string Nome, string Senha);
}