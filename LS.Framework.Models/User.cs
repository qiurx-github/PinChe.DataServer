using LS.Framework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Framework.Models
{
    [Table("User_tb")]
    public class User : BaseEntity
    {
        [MaxLength(100, ErrorMessage = "WxOpenID请小于100位")]
        public string WxOpenId { get; set; }

        [MaxLength(100, ErrorMessage = "WxUnionId请小于100位")]
        public string WxUnionId { get; set; }

        [MaxLength(30, ErrorMessage = "Mobile请小于30位")]
        public string Mobile { get; set; }

        [MaxLength(100, ErrorMessage = "Password请小于100位")]
        public string Password { get; set; }

        [MaxLength(300, ErrorMessage = "Avatar请小于300位")]
        public string Avatar { get; set; }

        [MaxLength(50, ErrorMessage = "NickName请小于50位")]
        public string NickName { get; set; }

        [MaxLength(30, ErrorMessage = "RealName请小于30位")]
        public string RealName { get; set; }

        public bool Sex { get; set; }

        [MaxLength(100, ErrorMessage = "Province请小于100位")]
        public string Province { get; set; }

        [MaxLength(100, ErrorMessage = "City请小于100位")]
        public string City { get; set; }

        public int State { get; set; }

        /// <summary>
        /// 个人认证
        /// </summary>
        public bool Grrz { get; set; }

        /// <summary>
        /// 车主认证
        /// </summary>
        public bool Czrz { get; set; }

        /// <summary>
        /// 邀请人ID
        /// </summary>
        public int InviteUserId { get; set; }

        [MaxLength(20, ErrorMessage = "IP请小于20位")]
        public string IP { get; set; }

        /// <summary>
        /// 伪造用户
        /// </summary>
        public bool IsForge { get; set; }

        /// <summary>
        /// 是否是线上合伙人
        /// </summary>
        public bool IsPartner { get; set; }
    }
}
