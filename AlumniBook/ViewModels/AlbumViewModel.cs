using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 是否设置为封面
        /// </summary>
        public string IsCover { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}