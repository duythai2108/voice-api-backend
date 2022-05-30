using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities.Enums
{
    public enum AccountStatusEnum
    {
        INACTIVE,       // chưa xác thực
        ACTIVE,         // đã xác thực
        BLOCKED,        // bị admin chặn
        DELETED,        // đã bị xoá
    }
}
