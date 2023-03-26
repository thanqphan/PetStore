namespace PetStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHACUNGCAP")]
    public partial class NHACUNGCAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGCAP()
        {
            SANPHAM = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(50)]
        [Display(Name = "Mã Nhà Cung")]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên Nhà Cung")]
        public string TenNCC { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Liên hệ")]
        public string SĐT { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAM { get; set; }
    }
}
