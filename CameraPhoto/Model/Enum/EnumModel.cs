using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraPhoto.Model
{
   public class EnumModel
    {
    }
    public enum OrderEnumphotoStatus
    {
        [EnumDescription("等待拍照")]
        Wait = 0,
        [EnumDescription("拍照一版")]
        PhotoOne = 1,
        [EnumDescription("拍照两版")]
        PhotoTwo = 2,
        [EnumDescription("打印一版")]
        PrintOne = 3,
        [EnumDescription("打印两版")]
        PrintTwo = 4,
        [EnumDescription("打印完成")]
        PrintOk = 5,
        [EnumDescription("订单完成")]
        Ok = 6
    }
    public enum OrderEnumPayStatus
    {
        [EnumDescription("已退款")]
        Refunds = -2,
        [EnumDescription("等待付款")]
        Wait = 1,
        [EnumDescription(" 已付款")]
        OK = 2,
        [EnumDescription(" 付款失败")]
        Error = 3,
        [EnumDescription(" 订单失效")]
        Overtime = 4,
        [EnumDescription(" 扣款成功")]
        DeOk = 5,
        [EnumDescription(" 扣款失败")]
        DeError = 6
    }
}
