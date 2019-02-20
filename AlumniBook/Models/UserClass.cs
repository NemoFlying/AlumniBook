using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniBook.Models
{
    public class UserClass
    {
        public UserClass()
        {
            this.JoinDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //设置自动增长
        public int Id { get; set; }

        public virtual ClassInfo ClassInfo { get; set; }

        public virtual RegisteredUser UserInfo { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime JoinDate { get; set; }


    }
}