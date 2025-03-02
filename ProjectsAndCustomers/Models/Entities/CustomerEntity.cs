using System.ComponentModel.DataAnnotations;

namespace ProjectsAndCustomers.Models.Entities {
    public class CustomerEntity {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
    }
}