using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rest_cap.Server.Models
{
    [Table("ApiAccess")]
    public class ApiAccess
    {
        [Key]
        public int SysId { get; set; }
        public string ApiKey { get; set; }
        public string User {  get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
