using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]   // {0} - Nome do campo
        [StringLength(60,MinimumLength =3, ErrorMessage ="{0} should be between {2} and {1}")]  // {1} - 1º campo(tamanho máximo), {2} - 2º campo(tam mínimo)
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]   // {0} - Nome do campo
        [EmailAddress(ErrorMessage = "Enter a valid email")] 
        [DataType(DataType.EmailAddress)]   // Tag Helper - E-mail
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]   // {0} - Nome do campo
        [Display(Name = "Birth Date")]  // Tag Helper - Altera o nome do campo
        [DataType(DataType.Date)]       // Tag Helper - Deixar datetime apenas com data
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]   // {0} - Nome do campo
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]    // Tag Helper - Formatando em 2 casas decimais
        [Range(100.00, 900.00, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; } // Forçar que Department seja chave estrangeira

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoverSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(p => p.Date >= initial && p.Date <= final).Sum(p => p.Amount);
        }
    }

}
