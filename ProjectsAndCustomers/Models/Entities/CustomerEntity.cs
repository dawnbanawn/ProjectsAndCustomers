using System.ComponentModel.DataAnnotations;
// from get started videon
namespace ProjectsAndCustomers.Models.Entities {
    public class CustomerEntity {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
    }
}