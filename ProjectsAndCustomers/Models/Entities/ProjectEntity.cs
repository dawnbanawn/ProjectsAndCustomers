﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAndCustomers.Models.Entities {
    public class ProjectEntity {
        [Key]
        public int Id { get; set; } // Guid is unique identifier
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public int? Budget {get; set; }

        // Connections to other entities

        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
    }
}
