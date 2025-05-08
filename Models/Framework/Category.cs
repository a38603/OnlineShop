namespace Models.Framework
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
       
        public int ID { get; set; }

        [StringLength(50,ErrorMessage ="Số ký tự tối đa là 50!")]
        [DisplayName("Tên danh Mục: ")]
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục!")]
        public string Name { get; set; }
        
        [StringLength(50,ErrorMessage = "Số ký tự tối đa là 50!")]
        [DisplayName("Tiêu đề SEO: ")]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề SEO!")]
        public string Alias { get; set; }
        [DisplayName("Danh mục cha: ")]
        [Required(ErrorMessage = "Bạn chưa nhập danh mục cha!")]
        public int? ParentID { get; set; }
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Thứ tự: ")]
        [Range(0,Int32.MaxValue,ErrorMessage ="Bạn phải nhập số!")]
        [Required(ErrorMessage = "Bạn chưa nhập thứ tự!")]
        public int? Order { get; set; }

        [DisplayName("Trạng thái: ")]
        [Required(ErrorMessage = "Bạn chưa nhập trạng thái")]
        
        public bool? Status { get; set; }
    }
}
