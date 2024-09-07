using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validators;

internal class ClassificacaoValidator : ValidationAttribute
{
    //valida se a classificacao é relacionada ao andar
    protected override ValidationResult IsValid(object classificacao, ValidationContext validationContext)
    {
        if (classificacao is null)
            return new ValidationResult("" + validationContext.DisplayName + " é obrigatório");


        var strClassificacao = Convert.ToString(classificacao);

        var listaAndares = new Geladeira().RetornarAndares().OrderBy(p => p.NumeroAndar).ToList();

        var listaClassificacoes = listaAndares.Select(p => p.Descricao).ToList();

        //primeiro, converte todos os cases das palavras para lower, depois valida se a lista contem a palavra vinda do json
        if (listaClassificacoes.Where(p => p.ToLower().Contains(strClassificacao?.ToLower())).ToList().Count > 0)
            return ValidationResult.Success;

        else
            return new ValidationResult("Classificação do item está inválida!");
    }
}