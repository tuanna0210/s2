using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Utils
{
    public class Message
    {
        /// <summary>
        /// ID của bản ghi được thêm, sửa, xóa
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Có lỗi hay không có lỗi
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Đối tượng attach kèm theo thông báo
        /// </summary>
        public object Obj { get; set; }
    }
}
