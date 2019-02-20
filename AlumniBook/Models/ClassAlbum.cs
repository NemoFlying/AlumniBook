using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public class ClassAlbum
    {
        public ClassAlbum()
        {
            this.IsCover = "N";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 是否设置为封面
        /// </summary>
        [MaxLength(1)]
        [Required]
        public string IsCover { get; set; }

        /// <summary>
        /// 创建人信息
        /// </summary>
        public virtual RegisteredUser CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改人信息
        /// </summary>
        public virtual RegisteredUser UpdateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}