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
    [Table("StoreInfo_tb")]
    public class StoreInfo : BaseEntity
    {
        #region  Public Properties    
        /// <summary>
        /// UserID
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// StoreName
        /// 门店名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "门店名称请小于50位")]
        public string StoreName { get; set; }
        /// <summary>
        /// RealName
        /// 联系人
        /// </summary>
        [MaxLength(30, ErrorMessage = "联系人请小于30位")]
        public string RealName { get; set; }
        /// <summary>
        /// FixedPhone
        /// 固定电话
        /// </summary>
        [MaxLength(30, ErrorMessage = "固定电话请小于30位")]
        public string FixedPhone
        {
            get;
            set;
        }
        /// <summary>
        /// MobilePhone
        /// 移动电话
        /// </summary>
        [MaxLength(30, ErrorMessage = "移动电话请小于30位")]
        public string MobilePhone
        {
            get;
            set;
        }
        /// <summary>
        /// Photo
        /// 门店照片 单图(Logo)
        /// </summary>
        [MaxLength(30, ErrorMessage = "照片路径长度请小于30位")]
        public string Photo
        {
            get;
            set;
        }
        /// <summary>
        /// Photos
        /// 门店照片 多图
        /// </summary>
        [MaxLength(3000, ErrorMessage = "照片路径长度请小于3000位")]
        public string Photos
        {
            get;
            set;
        }
        /// <summary>
        /// PriceList
        /// 价目表 图片地址
        /// </summary>
        [MaxLength(3000, ErrorMessage = "图片路径长度请小于3000位")]
        public string PriceList
        {
            get;
            set;
        }
        /// <summary>
        /// StoreType
        /// 经营类型
        /// </summary>
        [MaxLength(3000, ErrorMessage = "类型长度请小于3000位")]
        public string StoreType
        {
            get;
            set;
        }
        /// <summary>
        /// Service
        /// 经营内容
        /// </summary>
        [MaxLength(200, ErrorMessage = "经营内容长度请小于3000位")]
        public string Service
        {
            get;
            set;
        }
        /// <summary>
        /// LicenceImg
        /// 营业执照 照片
        /// </summary>
        [MaxLength(300, ErrorMessage = "照片路径长度请小于300位")]
        public string LicenceImg
        {
            get;
            set;
        }
        /// <summary>
        /// ContractImg
        /// 合同照片
        /// </summary>
        [MaxLength(300, ErrorMessage = "照片路径长度请小于300位")]
        public string ContractImg
        {
            get;
            set;
        }
        /// <summary>
        /// Province
        /// 省
        /// </summary>
        [MaxLength(100, ErrorMessage = "省名称长度请小于100位")]
        public string Province
        {
            get;
            set;
        }
        /// <summary>
        /// City
        /// 市
        /// </summary>
        [MaxLength(100, ErrorMessage = "市名称长度请小于100位")]
        public string City
        {
            get;
            set;
        }
        /// <summary>
        /// District
        /// 区
        /// </summary>
        [MaxLength(100, ErrorMessage = "区名称长度请小于100位")]
        public string District
        {
            get;
            set;
        }
        /// <summary>
        /// Lng
        /// 经度
        /// </summary>
        public double Lng
        {
            get;
            set;
        }
        /// <summary>
        /// Lat
        /// 纬度
        /// </summary>
        public double Lat
        {
            get;
            set;
        }
        /// <summary>
        /// AddrDetail
        /// 详细地址
        /// </summary>
        [MaxLength(100, ErrorMessage = "地址长度请小于100位")]
        public string AddrDetail
        {
            get;
            set;
        }
        /// <summary>
        /// Addr
        /// 地址简称
        /// </summary>
        [MaxLength(100, ErrorMessage = "地址简称长度请小于100位")]
        public string Addr
        {
            get;
            set;
        }
        /// <summary>
        /// GeoCode
        /// GeohashCode
        /// </summary>
        [MaxLength(100, ErrorMessage = "GeoCode长度请小于100位")]
        public string GeoCode
        {
            get;
            set;
        }
        /// <summary>
        /// Introduce
        /// 详细介绍
        /// </summary>
        [MaxLength(2000, ErrorMessage = "详细介绍长度请小于2000位")]
        public string Introduce
        {
            get;
            set;
        }
        /// <summary>
        /// IsCoupons
        /// 是否有优惠券
        /// </summary>
        public bool IsCoupons
        {
            get;
            set;
        }
        /// <summary>
        /// IsRelease
        /// 发布状态 1已发布 0未发布
        /// </summary>
        public bool IsRelease
        {
            get;
            set;
        }

        /// <summary>
        /// IsHomeRecommend
        /// 是否首页推荐
        /// </summary>
        public bool IsHomeRecommend
        {
            get;
            set;
        }
        /// <summary>
        /// BgImg
        /// 背景图片
        /// </summary>
        [MaxLength(300, ErrorMessage = "背景图片长度请小于300位")]
        public string BgImg
        {
            get;
            set;
        }
        /// <summary>
        /// SortIndex
        /// 排序
        /// </summary>
        public int SortIndex
        {
            get;
            set;
        }

        #endregion

    }
}
