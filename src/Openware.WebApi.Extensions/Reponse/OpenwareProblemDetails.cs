using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Openware.WebApi.Localization;

namespace Openware.WebApi.Reponse;
public class OpenwareProblemDetails : ProblemDetails
{
    [JsonPropertyName("errors")]
    public List<FieldError> Errors { get; set; } = new List<FieldError>();

    public OpenwareProblemDetails()
    {
        Title = Resources.ProblemResponse;
    }

    public OpenwareProblemDetails(IDictionary<string, string[]> errorsDictionary) : this()
    {
        CreateFieldErrorsList(errorsDictionary);
    }

    public OpenwareProblemDetails(string key, string message) : this()
    {
        Errors.Add(new FieldError()
        {
            Name = key,
            Reasons = new[] { message }
        });
    }

    private void CreateFieldErrorsList(IDictionary<string, string[]> errorsDictionary)
    {
        if (errorsDictionary == null || errorsDictionary.Count == 0)
        {
            return;
        }

        Errors = errorsDictionary.Select(x => new FieldError()
        {
            Name = JsonNamingPolicy.CamelCase.ConvertName(x.Key),
            Reasons = x.Value
        }).ToList();
    }
}
public class FieldError
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("reasons")]
    public string[] Reasons { get; set; }
}