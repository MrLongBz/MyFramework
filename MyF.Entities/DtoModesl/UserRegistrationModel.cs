using MyF.Infrastructure.Enum;

namespace MyF.Entities.DtoModesl;
public class UserRegistrationModel
{
    // 账号
    public string Account { get; set; } // 新增的账号属性

    // 用户名
    public string UserName { get; set; } // 用户名

    // 邮箱
    public string Email { get; set; } // 邮箱

    // 密码
    public string Password { get; set; } // 密码

    // 电话号码
    public string PhoneNumber { get; set; } // 电话号码

    // 公司名称
    public string Company { get; set; } // 新增的公司属性

    // 部门名称
    public string Department { get; set; } // 新增的部门属性

    // 性别
    public Gender? Gender { get; set; } // 新增的性别属性
}