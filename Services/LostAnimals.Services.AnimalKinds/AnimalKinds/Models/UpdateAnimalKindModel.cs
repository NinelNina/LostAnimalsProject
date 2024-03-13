using AutoMapper;
using LostAnimals.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAnimals.Services.AnimalKinds;

public class UpdateAnimalKindModel
{
    public string AnimalKindName { get; set; }
}

public class UpdateAnimalKindProfile : Profile
{
    public UpdateAnimalKindProfile()
    {
        CreateMap<UpdateAnimalKindModel, AnimalKind>();
    }
}
