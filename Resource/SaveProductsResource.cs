using System.ComponentModel.DataAnnotations;
using Dws.Note_one.Api.Domain.Models; 
using Dws.Note_one.Api.Domain.Helpers;
using Dws.Note_one.Api.Persistence.Repositories;
namespace Dws.Note_one.Api.Resource
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityInPackage { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; } 

        [Required]
        public string CategoryName { get; set; }

    }
}