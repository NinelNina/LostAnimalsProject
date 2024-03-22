using AutoMapper;
using LostAnimals.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAnimals.Services.Breeds.Models;

public class UpdateBreedModel
{
    public string BreedName { get; set; }
    public Guid AnimalKindID { get; set; }
}

public class UpdateBreedModelProfile : Profile
{
    public UpdateBreedModelProfile()
    {
        CreateMap<UpdateBreedModel, Breed>();
    }
}